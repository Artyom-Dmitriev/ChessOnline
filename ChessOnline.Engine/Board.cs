using ChessOnline.Engine.Models;

namespace ChessOnline.Engine
{
    public class Board
    {
        private Piece[,] _board = new Piece[8, 8];
        public PieceColor CurrentTurn { get; private set; } = PieceColor.White;

        public Board(Piece[,] board, PieceColor currentTurn)
        {
            _board = board;
            CurrentTurn = currentTurn;
        }

        public Piece GetPiece(Square square)
        {
            return _board[square.Row, square.Col];
        }

        public void SetPiece(Square square, Piece piece)
        {
            _board[square.Row, square.Col] = piece;
        }

        public bool IsEmpty(Square square)
        {
            return GetPiece(square).IsEmpty;
        }

        public bool IsOccupiedByColor(Square square, PieceColor color)
        {
            var piece = GetPiece(square);

            if (piece.IsEmpty == false && piece.Color == color)
                return true;

            return false;
        }

        public bool IsEmptyOrEnemy(Square sq, PieceColor myColor)
        {
            var piece = GetPiece(sq);

            return piece.IsEmpty || piece.Color != myColor;
        }

        public void SetupInitialPosition()
        {
            // Setup Pawns for white
            for (int col = 0; col < 8; col++)
            {
                SetPiece(new Square { Row = 1, Col = col },
                    new Piece { Type = PieceType.Pawn, Color = PieceColor.White });
            }

            // Setup Rooks for white
            SetPiece(new Square { Row = 0, Col = 0 },
                new Piece { Type = PieceType.Rook, Color = PieceColor.White });

            SetPiece(new Square { Row = 0, Col = 7 },
                new Piece { Type = PieceType.Rook, Color = PieceColor.White });

            //Setup Knights for white
            SetPiece(new Square { Row = 0, Col = 1 },
                new Piece { Type = PieceType.Knight, Color = PieceColor.White });

            SetPiece(new Square { Row = 0, Col = 6 },
                new Piece { Type = PieceType.Knight, Color = PieceColor.White });

            //Setup Bishops for white
            SetPiece(new Square { Row = 0, Col = 2 },
                new Piece { Type = PieceType.Bishop, Color = PieceColor.White });

            SetPiece(new Square { Row = 0, Col = 5 },
                new Piece { Type = PieceType.Bishop, Color = PieceColor.White });

            //Setup King and Queen for white
            SetPiece(new Square { Row = 0, Col = 3 },
                new Piece { Type = PieceType.Queen, Color = PieceColor.White });

            SetPiece(new Square { Row = 0, Col = 4 },
                new Piece { Type = PieceType.King, Color = PieceColor.White });


            // Setup Pawns for black
            for (int col = 0; col < 8; col++)
            {
                SetPiece(new Square { Row = 6, Col = col },
                    new Piece { Type = PieceType.Pawn, Color = PieceColor.Black });
            }

            // Setup Rooks for black
            SetPiece(new Square { Row = 7, Col = 0 },
                new Piece { Type = PieceType.Rook, Color = PieceColor.Black });

            SetPiece(new Square { Row = 7, Col = 7 },
                new Piece { Type = PieceType.Rook, Color = PieceColor.Black });

            //Setup Knights for black
            SetPiece(new Square { Row = 7, Col = 1 },
                new Piece { Type = PieceType.Knight, Color = PieceColor.Black });

            SetPiece(new Square { Row = 7, Col = 6 },
                new Piece { Type = PieceType.Knight, Color = PieceColor.Black });

            //Setup Bishops for black
            SetPiece(new Square { Row = 7, Col = 2 },
                new Piece { Type = PieceType.Bishop, Color = PieceColor.Black });

            SetPiece(new Square { Row = 7, Col = 5 },
                new Piece {Type = PieceType.Bishop,Color = PieceColor.Black });

            //Setup King and Queen for black
            SetPiece(new Square { Row = 7, Col = 3 },
                new Piece { Type = PieceType.Queen, Color = PieceColor.Black });

            SetPiece(new Square { Row = 7, Col = 4 },
                new Piece { Type = PieceType.King, Color = PieceColor.Black });

        }

        public Square FindKing(PieceColor color)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    var piece = GetPiece(new Square { Row = row, Col = col });

                    if (piece.Type == PieceType.King && piece.Color == color)
                    {
                        return new Square { Row = row, Col = col };
                    }
                }
            }
            throw new InvalidOperationException("Король не найден на доске.");
        }

        public Board Clone()
        {
            var cloneBoard = new Piece[8, 8];

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    cloneBoard[row, col] = this._board[row, col];
                }
            }
            return new Board(cloneBoard, this.CurrentTurn);
        }
    }
}
