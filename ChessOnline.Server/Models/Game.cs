namespace ChessOnline.Server.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int WhitePlayerId { get; set; }
        public int BlackPlayerId { get; set; }
        public int TimeControlSeconds { get; set; } 
        public int IncrementSeconds { get; set; } 
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? WinnerId { get; set; }
        public GameStatus Status { get; set; } = GameStatus.Waiting;

        // Навигационные свойства

        public User WhitePlayer { get; set; }
        public User BlackPlayer { get; set; }
        public User? Winner { get; set;}
    }
}
