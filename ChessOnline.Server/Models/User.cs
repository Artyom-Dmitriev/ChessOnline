namespace ChessOnline.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsGuest { get; set; }
        public DateTime? GuestExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int EloRating { get; set; } = 1200; // Default starting Elo rating
    }
}
