namespace ChessOnline.Engine.Models
{
    public readonly struct Piece
    {
        public PieceColor Color { get; init; }
        public PieceType Type { get; init; }
        public bool IsEmpty
        {
            get
            {
                if (PieceType.None == Type)
                    return true;

                return false;
            }
        }
        public static Piece Empty = new Piece { Type = PieceType.None, Color = PieceColor.None };
    }
}
