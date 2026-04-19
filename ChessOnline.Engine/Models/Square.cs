namespace ChessOnline.Engine.Models
{
    public readonly struct Square
    {
        public int Row { get; init; }
        public int Col { get; init; }

        public bool IsValid()
        {
            if (0 <= this.Row && this.Row <= 7 && 0 <= this.Col && this.Col <= 7)
                return true;

            return false;
        }
        public override string ToString()
        {
            char col = (char)('a' + this.Col);
            int row = Row + 1;

            return $"{col}{row}";
        }
    }
}
