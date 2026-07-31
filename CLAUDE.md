# Chess Online - Project Context

## About
Online chess web app (pet project for learning web development).

## Developer
Third-year applied data analysis / AI student. C# is the primary language:
confident with it, actively studies how it works under the hood (IL, dispatch,
value vs reference semantics). Comfortable with OOP and design principles.
New to web development — this is the first web project, so ASP.NET Core,
Blazor, SignalR and EF Core are all being learned here.
Goal: learn web dev deeply, not just get working code.

## Tech Stack
- ASP.NET Core (.NET 10) (Web API + SignalR)
- Blazor WebAssembly (frontend)
- Entity Framework Core + SQLite
- xUnit for tests
- JWT authentication

## Solution Structure
- ChessOnline.Server - backend API, SignalR hubs, EF Core
- ChessOnline.Client - Blazor WASM frontend
- ChessOnline.Shared - DTOs shared between server and client
- ChessOnline.Engine - chess rules, move validation, bot AI
- ChessOnline.Tests - unit tests

## Key Features
Core scope (targeted for MVP):
- Online matchmaking (blitz only: 3+2 or 5+0)
- Guest mode (24h temporary accounts, no registration needed, upgradeable to permanent)

Later scope (after MVP works end to end):
- Bot play (minimax AI, multiple difficulty levels)
- Friends system (add by nickname/ID, invite to games)
- Game history and Elo rating stats

## Work Format
- Give small tech-tasks for key logic (models, services, LINQ queries, validators)
- Developer writes code, Claude reviews and explains
- Claude handles boilerplate/config (Program.cs, .csproj, migrations, CSS)
- Always explain WHY something is done, not just WHAT
- Reference PDF learning materials (Parts 1-4) when relevant concepts appear
- C# language mechanics can be discussed at depth; web concepts need
  to be introduced from the ground up

## Current Stage
Stage 2 - Chess Engine (ChessOnline.Engine).
Working on pure chess logic as a standalone library.
Stage 1 (Foundation) is complete: Solution structure,
DB models, auth with JWT, guest mode, all tested
via Swagger.

Blocks:
2.1 Basic types (enums, structs) - done
2.2 Board representation (8x8 array) - done
2.3 Move generation (all piece types) - done
2.4 Special rules (castling, en passant, promotion) - in progress
2.5 Check detection + move filtering - next
2.6 Checkmate, stalemate, draw conditions
2.7 GameState (full game management)
2.8 Algebraic notation

Key principle for this stage: Engine has ZERO
dependencies on web/DB. Pure chess logic only.
All public methods must have xUnit tests,
written in Arrange-Act-Assert form.

## Architecture Decisions
- Server validates ALL moves (anti-cheat)
- SignalR for real-time (moves, matchmaking), REST for everything else
- Guest users: IsGuest=true, GuestExpiresAt, nullable Email/PasswordHash
- Background service cleans expired guests every hour
