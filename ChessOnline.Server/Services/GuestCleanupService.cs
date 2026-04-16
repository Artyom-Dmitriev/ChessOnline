using ChessOnline.Server.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ChessOnline.Server.Services
{
    public class GuestCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public GuestCleanupService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope(); // Создаем новый scope для получения AppDbContext = новый контейнер.
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>(); // Распаковываем из контейнера AppDbContext = получаем новую порцию данных.

                var expiredGuests = db.Users.Where(u => (u.IsGuest == true) && (u.GuestExpiresAt < DateTime.UtcNow)).ToList();

                db.Users.RemoveRange(expiredGuests);
                await db.SaveChangesAsync();

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
