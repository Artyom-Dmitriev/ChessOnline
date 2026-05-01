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

        private static readonly (int dRow, int dCol)[] RookDirections =
        {
          (+1, 0), (-1, 0), (0, +1), (0, -1)
        };

        private static readonly (int dRow, int dCol)[] BishopDirections =
        {
          (+1, +1), (+1, -1), (-1, +1), (-1, -1)
        };

        private static readonly (int dRow, int dCol)[] QueenDirections =
        {
          (+1, 0), (-1, 0), (0, +1), (0, -1),
          (+1, +1), (+1, -1), (-1, +1), (-1, -1)
        };

        private static readonly (int dRow, int dCol)[] KingOffsets =
        {
          (+1, 0), (-1, 0), (0, +1), (0, -1),
          (+1, +1), (+1, -1), (-1, +1), (-1, -1)
        };

        private static readonly PieceType[] PromotionTypes =
        {
          PieceType.Queen, PieceType.Rook, PieceType.Bishop, PieceType.Knight
        };

        private void AddPawnMove(List<Move> moves, Square from, Square to, PieceColor color)
        {
            bool isPromotion =
                (color == PieceColor.White && to.Row == 7) ||
                (color == PieceColor.Black && to.Row == 0);

            if (isPromotion)
            {
                foreach (var promo in PromotionTypes)
                    moves.Add(new Move { From = from, To = to, Promotion = promo });
            }
            else
            {
                moves.Add(new Move { From = from, To = to });
            }
        }

        public List<Move> GetMoves(Board board, Square from)
        {
            var piece = board.GetPiece(from);

            if (piece.IsEmpty)
                return new List<Move>();

            return piece.Type switch
            {
                PieceType.Pawn => GetPawnMoves(board, from, piece.Color),
                PieceType.Knight => GetKnightMoves(board, from, piece.Color),
                PieceType.Bishop => GetBishopMoves(board, from, piece.Color),
                PieceType.Rook => GetRookMoves(board, from, piece.Color),
                PieceType.Queen => GetQueenMoves(board, from, piece.Color),
                PieceType.King => GetKingMoves(board, from, piece.Color),
                _ => new List<Move>()
            };
        }

        public List<Move> GetAllMoves(Board board, PieceColor color)
        {
            var allMoves = new List<Move>();

            for (var row = 0; row < 8; row++)
            {
                for (var col = 0; col < 8; col++)
                {
                    var from = new Square { Row = row, Col = col };
                    var piece = board.GetPiece(from);

                    if (piece.IsEmpty) continue; // None piece on this square
                    if (piece.Color != color) continue; // Opponent's piece, skip

                    allMoves.AddRange(GetMoves(board, from)); // Get moves for this piece and add to the list
                }
            }

            return allMoves;
        }

        public static bool IsOnBoard(int row, int col)
        {
            return row >= 0 && col >= 0 && row < 8 && col < 8;
        }

        private List<Move> GetSlidingMoves(Board board, Square from, PieceColor color, (int dRow, int dCol)[] directions)
        {
            var moves = new List<Move>();

            foreach (var (dRow, dCol) in directions)
            {
                for (int step = 1; step < 8; step++)
                {
                    var to = new Square
                    {
                        Row = from.Row + dRow * step,
                        Col = from.Col + dCol * step
                    };

                    if (!IsOnBoard(to.Row, to.Col)) break;
                    if (!board.IsEmptyOrEnemy(to, color)) break;

                    moves.Add(new Move { From = from, To = to });

                    if (!board.IsEmpty(to)) break;
                }
            }


            return moves;
        }


        public List<Move> GetRookMoves(Board board, Square from, PieceColor color)
            => GetSlidingMoves(board, from, color, RookDirections);

        public List<Move> GetBishopMoves(Board board, Square from, PieceColor color)
            => GetSlidingMoves(board, from, color, BishopDirections);

        public List<Move> GetQueenMoves(Board board, Square from, PieceColor color)
            => GetSlidingMoves(board, from, color, QueenDirections);

        public List<Move> GetKingMoves(Board board, Square from, PieceColor color)
        {
            var moves = new List<Move>();

            foreach (var move in KingOffsets)
            {
                var to = new Square { Row = from.Row + move.dRow, Col = from.Col + move.dCol };
                if (!IsOnBoard(to.Row, to.Col)) continue;
                if (!board.IsEmptyOrEnemy(to, color)) continue;
                moves.Add(new Move { From = from, To = to });
            }

            return moves;
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
                    AddPawnMove(moves, from, new Square { Col = from.Col, Row = from.Row + 1 }, color);

                if (from.Row == 1 &&
                    board.IsEmpty(new Square { Col = from.Col, Row = from.Row + 2 }) &&
                    board.IsEmpty(new Square { Col = from.Col, Row = from.Row + 1 }))
                    AddPawnMove(moves, from, new Square { Col = from.Col, Row = from.Row + 2 }, color);

                if (IsOnBoard(from.Row + 1, from.Col + 1) &&
                    board.IsOccupiedByColor(new Square
                    { Col = from.Col + 1, Row = from.Row + 1 }, PieceColor.Black))               
                    AddPawnMove(moves, from, new Square { Col = from.Col + 1, Row = from.Row + 1 }, color);
                

                if (IsOnBoard(from.Row + 1, from.Col - 1) &&
                    board.IsOccupiedByColor(new Square
                    { Col = from.Col - 1, Row = from.Row + 1 }, PieceColor.Black))              
                    AddPawnMove(moves, from, new Square { Col = from.Col - 1, Row = from.Row + 1 }, color);
            }

            //Black Pawns moves

            if (color == PieceColor.Black)
            {
                if (IsOnBoard(from.Row - 1, from.Col) &&
                    board.IsEmpty(new Square { Col = from.Col, Row = from.Row - 1 }))
                    AddPawnMove(moves, from, new Square { Col = from.Col, Row = from.Row - 1 }, color);

                if (from.Row == 6 &&
                   board.IsEmpty(new Square { Col = from.Col, Row = from.Row - 2 }) &&
                   board.IsEmpty(new Square { Col = from.Col, Row = from.Row - 1 }))
                    AddPawnMove(moves, from, new Square { Col = from.Col, Row = from.Row - 2 }, color); 

                if (IsOnBoard(from.Row - 1, from.Col - 1) &&
                    board.IsOccupiedByColor(new Square
                    { Col = from.Col - 1, Row = from.Row - 1 }, PieceColor.White))             
                    AddPawnMove(moves, from, new Square { Col = from.Col - 1, Row = from.Row - 1 }, color); 
          

                if (IsOnBoard(from.Row - 1, from.Col + 1) &&
                        board.IsOccupiedByColor(new Square
                        { Col = from.Col + 1, Row = from.Row - 1 }, PieceColor.White))
                    AddPawnMove(moves, from, new Square { Col = from.Col + 1, Row = from.Row - 1 }, color);
            }

            return moves;
        }
    }
}
