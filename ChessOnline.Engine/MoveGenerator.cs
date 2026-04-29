using ChessOnline.Engine.Models;

namespace ChessOnline.Engine
{
    public class MoveGenerator
    {
        private static readonly (int dRow, int dCol)[] KnightOffsets =
        {
          (+2, +1), (+2, -1), (-2, +1), (-2, -1),
          (+1, +2), (+1, -2), (-1, +2), (-1, -2)
        };
        public List<Move> GetMoves(Board board, Square from)
        {
            var piece = board.GetPiece(from);

            if (piece.IsEmpty)
                return new List<Move>();

            return piece.Type switch
            {
                PieceType.Pawn => GetPawnMoves(board, from, piece.Color),
                PieceType.Knight => GetKnightMoves(board, from, piece.Color),
                _ => new List<Move>()
            };
        }

        public static bool IsOnBoard(int row, int col)
        {
            return row >= 0 && col >= 0 && row < 8 && col < 8;
        }

        public List<Move> GetKnightMoves(Board board, Square from, PieceColor color)
        {
            var moves = new List<Move>();

            foreach (var move in KnightOffsets)
            {
                var to = new Square { Col = from.Col + move.dCol, Row = from.Row + move.dRow };
                if (!IsOnBoard(from.Row + move.dRow, from.Col + move.dCol)) continue;
                if (!board.IsEmptyOrEnemy(to, color)) continue;
                moves.Add(new Move { From = from, To = to });
            }

            return moves;
        }

        public List<Move> GetPawnMoves(Board board, Square from, PieceColor color)
        {
            var moves = new List<Move>();

            // White Pawns moves

            if (color == PieceColor.White)
            {
                if (IsOnBoard(from.Row + 1, from.Col) &&
                    board.IsEmpty(new Square { Col = from.Col, Row = from.Row + 1 }))
                    moves.Add(new Move
                    {
                        From = from,
                        To = new Square { Col = from.Col, Row = from.Row + 1 }
                    });

                if (from.Row == 1 &&
                    board.IsEmpty(new Square { Col = from.Col, Row = from.Row + 2 }) &&
                    board.IsEmpty(new Square { Col = from.Col, Row = from.Row + 1 }))
                    moves.Add(new Move
                    {
                        From = from,
                        To = new Square { Col = from.Col, Row = from.Row + 2 }
                    });

                if (IsOnBoard(from.Row + 1, from.Col + 1) &&
                    board.IsOccupiedByColor(new Square
                    { Col = from.Col + 1, Row = from.Row + 1 }, PieceColor.Black))
                {
                    moves.Add(new Move
                    {
                        From = from,
                        To = new Square { Col = from.Col + 1, Row = from.Row + 1 }
                    });
                }

                if (IsOnBoard(from.Row + 1, from.Col - 1) &&
                    board.IsOccupiedByColor(new Square
                    { Col = from.Col - 1, Row = from.Row + 1 }, PieceColor.Black))
                {
                    moves.Add(new Move
                    {
                        From = from,
                        To = new Square { Col = from.Col - 1, Row = from.Row + 1 }
                    });
                }
            }

            //Black Pawns moves

            if (color == PieceColor.Black)
            {
                if (IsOnBoard(from.Row - 1, from.Col) &&
                    board.IsEmpty(new Square { Col = from.Col, Row = from.Row - 1 }))
                    moves.Add(new Move
                    {
                        From = from,
                        To = new Square { Col = from.Col, Row = from.Row - 1 }
                    });


                if (from.Row == 6 &&
                   board.IsEmpty(new Square { Col = from.Col, Row = from.Row - 2 }) &&
                   board.IsEmpty(new Square { Col = from.Col, Row = from.Row - 1 }))
                    moves.Add(new Move
                    {
                        From = from,
                        To = new Square { Col = from.Col, Row = from.Row - 2 }
                    });

                if (IsOnBoard(from.Row - 1, from.Col - 1) &&
                    board.IsOccupiedByColor(new Square
                    { Col = from.Col - 1, Row = from.Row - 1 }, PieceColor.White))
                {
                    moves.Add(new Move
                    {
                        From = from,
                        To = new Square { Col = from.Col - 1, Row = from.Row - 1 }
                    });
                }

                if (IsOnBoard(from.Row - 1, from.Col + 1) &&
                        board.IsOccupiedByColor(new Square
                        { Col = from.Col + 1, Row = from.Row - 1 }, PieceColor.White))
                {
                    moves.Add(new Move
                    {
                        From = from,
                        To = new Square { Col = from.Col + 1, Row = from.Row - 1 }
                    });
                }
            }

            return moves;
        }
    }
}
