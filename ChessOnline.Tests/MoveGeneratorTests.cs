using ChessOnline.Engine;
using ChessOnline.Engine.Models;

namespace ChessOnline.Tests
{
    public class MoveGeneratorTests
    {
        /// <summary>
        /// White Pawns Tests.
        /// </summary>

        [Fact]
        public void Generate_Move_From_Random_Position_For_White_Pawn()
        {
            // Arrange 
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 3, Col = 3 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place the white pawn on the boardcl

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Contains(moves, m => m.To.Row == 4 && m.To.Col == 3); // Move forward
        }

        [Fact]
        public void Generate_Move_From_Starting_Position_For_White_Pawn()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 1, Col = 1 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place the white pawn on the board

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Contains(moves, m => m.To.Row == 2 && m.To.Col == 1); // Move forward
            Assert.Contains(moves, m => m.To.Row == 3 && m.To.Col == 1); // Move two squares forward
        }

        [Fact]
        public void Move_From_Starting_Position_Is_Impossible_For_White_Pawn()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 1, Col = 1 };
            var squareInFront = new Square { Row = 2, Col = 1 };
            board.SetPiece(squareInFront, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn });

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 2 && m.To.Col == 1); // Move forward should be blocked
        }

        [Fact]
        public void Taking_Diagonal_To_Left()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 3, Col = 3 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place the white pawn on the board
            board.SetPiece(new Square { Row = 4, Col = 2 }, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn });

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Contains(moves, m => m.To.Row == 4 && m.To.Col == 2); // Taking diagonal to the left
        }

        [Fact]
        public void Does_Not_Allow_Taking_Diagonal_Own_Piece()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 5, Col = 2 };
            board.SetPiece(new Square { Row = 6, Col = 1 },
                new Piece { Color = PieceColor.White, Type = PieceType.Pawn });
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place the white pawn on the board

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 6 && m.To.Col == 1); // Should not allow taking own piece
        }

        [Fact]
        public void Does_Not_Allow_Moving_Backwards()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 3, Col = 3 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place the white pawn on the board
            // Act
            var moves = moveGenerator.GetMoves(board, from);
            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 2 && m.To.Col == 3); // Should not allow moving backwards
        }

        /// <summary>
        /// Black Pawns Tests.
        /// </summary>

        [Fact]
        public void Generate_Move_From_Random_Position_For_Black_Pawn()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.Black);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 3 };
            board.SetPiece(from, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn }); // Place the black pawn on the board

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Contains(moves, m => m.To.Row == 3 && m.To.Col == 3); // Move forward
        }

        [Fact]
        public void Generate_Move_From_Starting_Position_For_Black_Pawn()
        {
            //Arrange
            var board = new Board(new Piece[8, 8], PieceColor.Black);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 6, Col = 1 };
            board.SetPiece(from, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn }); // Place the black pawn on the board

            //Act
            var moves = moveGenerator.GetMoves(board, from);

            //Assert
            Assert.Contains(moves, m => m.To.Row == 5 && m.To.Col == 1); // Move forward
            Assert.Contains(moves, m => m.To.Row == 4 && m.To.Col == 1); // Move two squares forward
        }

        [Fact]
        public void Move_From_Starting_Position_Is_Impossible_For_Black_Pawn()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.Black);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 6, Col = 1 };
            var squareInFront = new Square { Row = 5, Col = 1 };
            board.SetPiece(squareInFront, new Piece { Color = PieceColor.White, Type = PieceType.Pawn });
            board.SetPiece(from, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn }); // Place the black pawn on the board

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 5 && m.To.Col == 1); // Move forward should be blocked
            Assert.DoesNotContain(moves, m => m.To.Row == 4 && m.To.Col == 1); // Move two squares forward should also be blocked
        }

        [Fact]
        public void Taking_Diagonal_To_Left_For_Black_Pawn()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.Black);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 3 };
            var target = new Square { Row = 3, Col = 2 };
            board.SetPiece(from, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn }); // Place the black pawn on the board
            board.SetPiece(target, new Piece { Color = PieceColor.White, Type = PieceType.Pawn });

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Contains(moves, m => m.To.Row == 3 && m.To.Col == 2); // Taking diagonal to the left
        }

        [Fact]
        public void Taking_Diagonal_To_Right_For_Black_Pawn()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.Black);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 3 };
            var target = new Square { Row = 3, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn }); // Place the black pawn on the board
            board.SetPiece(target, new Piece { Color = PieceColor.White, Type = PieceType.Pawn });
            // Act
            var moves = moveGenerator.GetMoves(board, from);
            // Assert
            Assert.Contains(moves, m => m.To.Row == 3 && m.To.Col == 4); // Taking diagonal to the right
        }

        [Fact]
        public void Does_Not_Allow_Taking_Diagonal_Own_Piece_For_Black_Pawn()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.Black);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 3 };
            var target = new Square { Row = 3, Col = 2 };
            board.SetPiece(from, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn }); // Place the black pawn on the board
            board.SetPiece(target, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn });

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 3 && m.To.Col == 2);
        }

        [Fact]
        public void Does_Not_Allow_Moving_Backwards_For_Black_Pawn()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.Black);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 3 };
            board.SetPiece(from, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn }); // Place the black pawn on the board
            // Act
            var moves = moveGenerator.GetMoves(board, from);
            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 5 && m.To.Col == 3); // Should not allow moving backwards
        }
    }
}