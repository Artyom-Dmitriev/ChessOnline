namespace ChessOnline.Server.Models
{
    public class Friendship
    {
        public int Id { get; set; }
        public int RequesterId { get; set; }
        public int AddresseeId { get; set; }
        public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; 

        // Навигационные свойства
        public User Requester { get; set; }
        public User Addressee { get; set; }
    }
}
