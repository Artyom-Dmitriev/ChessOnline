namespace ChessOnline.Server.Models
{
    public class Move
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int MoveNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string? Promotion { get; set; }
        public string MoveNotation { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства

        public Game Game { get; set; }
        public User Player { get; set; }
    }
}
