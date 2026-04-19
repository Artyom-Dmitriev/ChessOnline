namespace ChessOnline.Engine.Models
{
    public readonly struct Move
    {
        public Square From {  get; init; }
        public Square To { get; init; }
        public PieceType? Promotion { get; init; }
    }
}
