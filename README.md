# ChessOnline

Онлайн-шахматы на ASP.NET Core и Blazor WebAssembly.

Учебный пет-проект: цель — не просто получить работающее приложение, а разобраться,
как оно устроено изнутри. Ключевая логика пишется вручную и покрывается тестами,
рутина (конфигурация, boilerplate) генерируется. Проект развивается поэтапно,
каждый этап разбит на блоки, каждый блок — отдельный коммит.

## Стек

| Технология | Версия / назначение |
|---|---|
| .NET | `net10.0` (все проекты) |
| ASP.NET Core Web API | HTTP-эндпоинты, DI, конфигурация |
| Blazor WebAssembly | клиент (пока шаблон по умолчанию) |
| Entity Framework Core + SQLite | доступ к данным, миграции |
| JWT (`Microsoft.AspNetCore.Authentication.JwtBearer`) | аутентификация |
| BCrypt.Net-Next | хеширование паролей |
| Swashbuckle (Swagger UI) | ручная проверка API |
| xUnit | юнит-тесты движка |

Решение хранится в новом XML-формате — `ChessOnline.slnx`.

## Структура решения

```
ChessOnline/
├── ChessOnline.Server/    # Web API: контроллеры, сервисы, EF Core, миграции
├── ChessOnline.Client/    # Blazor WebAssembly (шаблон, ещё не наполнен)
├── ChessOnline.Shared/    # DTO и enum'ы, общие для сервера и клиента
├── ChessOnline.Engine/    # шахматная логика — чистая доменная библиотека
├── ChessOnline.Tests/     # xUnit-тесты движка
├── CLAUDE.md              # контекст проекта для Claude Code
└── ChessOnline.slnx
```

Зависимости: `Server → Shared, Engine`; `Client → Shared`; `Tests → Engine, Shared`.
`Engine` не зависит ни от чего — ни от веба, ни от БД.

## Что реализовано

### Сервер и аутентификация

* **Модели данных** — `User`, `Game`, `Move`, `Friendship`.
* **`AppDbContext`** — связи через Fluent API (`HasOne/WithMany`, `DeleteBehavior.Restrict`
  для игроков, победителя, ходов), уникальный индекс на `User.Nickname`.
* **Миграция `InitialCreate`** — создаёт схему в SQLite (`chessonline.db`).
* **Регистрация и логин** — пароли хешируются BCrypt, в ответ выдаётся JWT.
* **Гостевой режим** — временный аккаунт без email и пароля, ник вида `Guest_1a2b3c4d`,
  срок жизни 24 часа (`IsGuest`, `GuestExpiresAt`).
* **Апгрейд гостя до постоянного аккаунта** — по JWT текущего пользователя;
  email, пароль и ник заполняются, рейтинг и `Id` сохраняются.
* **`GuestCleanupService`** — фоновый `BackgroundService`, раз в час удаляет
  просроченные гостевые аккаунты.
* **`TokenService`** — генерация JWT с клеймами `NameIdentifier`, `Name`, `IsGuest`.
* **Swagger UI** с поддержкой Bearer-авторизации — все эндпоинты проверены вручную.

### Шахматный движок (`ChessOnline.Engine`)

Библиотека без единой зависимости от HTTP, БД и UI — только правила шахмат.

**Базовые типы** (`Models/`): `PieceType`, `PieceColor`, `GameStatus` — enum'ы;
`Piece`, `Square`, `Move` — `readonly struct`.
`Square.ToString()` возвращает шахматную нотацию клетки (`e4`, `a1`).

**`Board`** — доска `Piece[8, 8]`:

| Метод | Что делает |
|---|---|
| `GetPiece` / `SetPiece` | чтение и запись фигуры на клетке |
| `IsEmpty` | пуста ли клетка |
| `IsOccupiedByColor` | стоит ли на клетке фигура заданного цвета |
| `IsEmptyOrEnemy` | можно ли встать на клетку (пусто или вражеская фигура) |
| `SetupInitialPosition` | начальная расстановка всех 32 фигур |
| `FindKing` | поиск короля заданного цвета |
| `Clone` | глубокая копия доски |

**`MoveGenerator`** — генерация ходов:

* `GetPawnMoves` — ход на одну клетку, рывок на две с начальной горизонтали,
  взятия по диагонали; отдельно для белых и чёрных.
* `GetKnightMoves` — 8 смещений, прыжок через фигуры.
* `GetSlidingMoves` — общий метод для скользящих фигур через массивы направлений;
  на нём построены `GetRookMoves`, `GetBishopMoves`, `GetQueenMoves`.
* `GetKingMoves` — 8 соседних клеток.
* `GetMoves` — диспетчер по типу фигуры (`switch`-выражение).
* `GetAllMoves` — все ходы всех фигур заданного цвета.
* **Превращение пешки** — при выходе на последнюю горизонталь генерируются
  четыре хода с заполненным `Move.Promotion`: ферзь, ладья, слон, конь.

Ходы на этом этапе — **псевдо-легальные**: фильтрация по шаху ещё не реализована.

### Конвенция индексации

`Row 0` — первая горизонталь (белые), `Row 7` — восьмая (чёрные).
`Col 0` — вертикаль `a`. То есть `Square(0, 4)` = `e1` = стартовая клетка белого короля.
Конвенция единая для всего проекта.

## API

Все эндпоинты возвращают `{ "token": "<JWT>" }`.

| Метод | Путь | Доступ | Описание |
|---|---|---|---|
| `POST` | `/api/users/register` | анонимно | регистрация по email, нику и паролю |
| `POST` | `/api/users/login` | анонимно | вход по email и паролю |
| `POST` | `/api/users/guest` | анонимно | создание гостевого аккаунта на 24 часа |
| `POST` | `/api/users/upgrade` | `Bearer` | превращение гостя в постоянный аккаунт |

## Модель данных

| Сущность | Ключевые поля |
|---|---|
| `User` | `Nickname` (уникальный), `Email?`, `PasswordHash?`, `IsGuest`, `GuestExpiresAt?`, `EloRating` (1200 по умолчанию), `CreatedAt` |
| `Game` | `WhitePlayerId`, `BlackPlayerId`, `WinnerId?`, `Status`, `TimeControlSeconds`, `IncrementSeconds`, `StartedAt?`, `FinishedAt?` |
| `Move` | `GameId`, `PlayerId`, `MoveNumber`, `From`, `To`, `Promotion?`, `MoveNotation` |
| `Friendship` | `RequesterId`, `AddresseeId`, `Status` (`Pending` / `Accepted` / `Declined`) |

## Запуск

Нужен .NET SDK 10.

```bash
git clone https://github.com/Artyom-Dmitriev/ChessOnline.git
cd ChessOnline
dotnet restore
```

Создать или обновить базу данных (нужен инструмент `dotnet-ef`):

```bash
dotnet tool install --global dotnet-ef
```

```bash
dotnet ef database update --project ChessOnline.Server
```

Запустить сервер:

```bash
dotnet run --project ChessOnline.Server
```

Swagger UI: `http://localhost:5237/swagger` (профиль `http`)
или `https://localhost:7047/swagger` (профиль `https`).

Настройки подключения и JWT — в `ChessOnline.Server/appsettings.json`.
Ключ подписи там лежит как заглушка для локальной разработки; для любого
публичного развёртывания его нужно вынести в переменные окружения или user secrets.

## Тесты

```bash
dotnet test
```

52 теста, все зелёные — 51 по движку плюс пустая заглушка из шаблона:

* `BoardTests` — начальная расстановка, `GetPiece`/`SetPiece`, независимость `Clone`.
* `MoveGeneratorTests` — пешки обоих цветов (ход, рывок, взятия, блокировка,
  запрет хода назад), конь (центр, край, угол, прыжок через свои, взятие),
  ладья, слон и ферзь (количество ходов из центра и угла, остановка перед своей
  фигурой, взятие вражеской, запрет хода сквозь неё), король, `GetAllMoves`
  (20 ходов в стартовой позиции), превращение пешки во все четыре фигуры —
  и прямым ходом, и со взятием.

## Текущий статус

Этап 1 (фундамент: решение, БД, аутентификация, гостевой режим) — завершён.
Этап 2 (шахматный движок) — в работе: базовые типы, доска, генерация ходов
всех фигур и превращение пешки готовы.
