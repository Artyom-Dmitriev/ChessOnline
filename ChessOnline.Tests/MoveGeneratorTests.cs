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

        /// <summary>
        /// Knight Tests.
        /// </summary>
        [Fact]
        public void Generate_All_Moves_For_Knight()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Knight }); // Place the knight on the board

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Equal(8, moves.Count); // A knight in the center of the board should have 8 possible moves
        }

        [Fact]
        public void Knight_In_The_Corner_Should_Have_2_Moves()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 0, Col = 0 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Knight }); // Place the knight in the corner

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Equal(2, moves.Count); // A knight in the corner should have 2 possible moves
            Assert.Contains(moves, m => m.To.Row == 2 && m.To.Col == 1); // Move to (2, 1)
            Assert.Contains(moves, m => m.To.Row == 1 && m.To.Col == 2); // Move to (1, 2)
        }

        [Fact]
        public void Knight_At_The_Edge_Should_Have_4_Moves()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.Black);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 0, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.Black, Type = PieceType.Knight }); // Place the knight at the edge

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Equal(4, moves.Count); // A knight at the edge should have 4 possible moves
            Assert.Contains(moves, m => m.To.Row == 2 && m.To.Col == 3); // Move to (2, 3)
            Assert.Contains(moves, m => m.To.Row == 2 && m.To.Col == 5); // Move to (2, 5)
            Assert.Contains(moves, m => m.To.Row == 1 && m.To.Col == 2); // Move to (1, 2)
            Assert.Contains(moves, m => m.To.Row == 1 && m.To.Col == 6); // Move to (1, 6)
        }

        [Fact]
        public void Knight_Can_Jump_Over_Own_Pieces()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Knight }); // Place the Knight on the board
            board.SetPiece(new Square { Row = 5, Col = 4 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece in front of the Knight
            board.SetPiece(new Square { Row = 4, Col = 5 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece to the right of the Knight
            board.SetPiece(new Square { Row = 3, Col = 4 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece behind the Knight
            board.SetPiece(new Square { Row = 4, Col = 3 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece to the left of the Knight
            board.SetPiece(new Square { Row = 5, Col = 5 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece diagonally in front-right of the Knight
            board.SetPiece(new Square { Row = 5, Col = 3 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece diagonally in front-left of the Knight
            board.SetPiece(new Square { Row = 3, Col = 5 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece diagonally behind-right of the Knight
            board.SetPiece(new Square { Row = 3, Col = 3 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece diagonally behind-left of the Knight

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Equal(8, moves.Count); // A knight should still have 8 possible moves even if all surrounding squares are occupied by its own pieces
        }

        [Fact]
        public void Knight_Can_Not_Move_To_Square_Occupied_By_Own_Piece()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.Black);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.Black, Type = PieceType.Knight }); // Place the Knight on the board
            board.SetPiece(new Square { Row = 6, Col = 5 }, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn }); // Place a piece on one of the Knight's possible move squares

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 6 && m.To.Col == 5); // The Knight should not be able to move to a square occupied by its own piece
            Assert.Equal(7, moves.Count); // The Knight should have 7 possible moves instead of 8
        }

        [Fact]
        public void Knight_Can_Take_Enemy_Piece()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.Black);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.Black, Type = PieceType.Knight }); // Place the Knight on the board
            board.SetPiece(new Square { Row = 6, Col = 5 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place an enemy piece on one of the Knight's possible move squares

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Contains(moves, m => m.To.Row == 6 && m.To.Col == 5); // The Knight should be able to take an enemy piece
        }

        /// <summary>
        /// Rook Tests.
        /// </summary>

        [Fact]
        public void Generate_Rook_Moves_From_Center()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Rook }); // Place the Rook on the board

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Equal(14, moves.Count); // A rook in the center of the board should have 14 possible moves (7 in each direction)
        }

        [Fact]
        public void Rook_In_The_Corner_Should_Have_15_Moves()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 0, Col = 0 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Rook }); // Place the Rook in the corner

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Equal(14, moves.Count); // A rook in the corner should have 14 possible moves (7 along the row and 7 along the column)
        }

        [Fact]
        public void Rook_Blocked_By_Own_Piece_Should_Have_Limited_Moves()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Rook }); // Place the Rook on the board
            board.SetPiece(new Square { Row = 4, Col = 7 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the rook's movement to the right

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 4 && m.To.Col == 7); // The rook should not be able to move to a square occupied by its own piece
            Assert.DoesNotContain(moves, m => m.To.Row == 4 && m.To.Col == 8); // The rook should not be able to move past its own piece
            Assert.Equal(13, moves.Count); // The rook should have 13с possible moves instead of 14
        }

        [Fact]
        public void Rook_Can_Take_Enemy_Piece_And_Stop()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Rook }); // Place the Rook on the board
            board.SetPiece(new Square { Row = 4, Col = 6 }, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn }); // Place an enemy piece blocking the rook's movement to the right

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Contains(moves, m => m.To.Row == 4 && m.To.Col == 6); // The rook should be able to take an enemy piece
            Assert.DoesNotContain(moves, m => m.To.Row == 4 && m.To.Col == 7); // The rook should not be able to move past the enemy piece
        }

        [Fact]
        public void Rook_Can_Not_Move_Diagonally()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Rook }); // Place the Rook on the board

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 5 && m.To.Col == 5); // The rook should not be able to move diagonally
            Assert.DoesNotContain(moves, m => m.To.Row == 3 && m.To.Col == 3); // The rook should not be able to move diagonally
        }

        [Fact]
        public void Blocked_Rook_Should_Not_Have_Moves_In_That_Direction()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Rook }); // Place the Rook on the board
            board.SetPiece(new Square { Row = 2, Col = 4 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the rook's movement upwards

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 2 && m.To.Col == 4); // The rook should not be able to move to a square occupied by its own piece
            Assert.DoesNotContain(moves, m => m.To.Row == 1 && m.To.Col == 4); // The rook should not be able to move past its own piece
        }

        [Fact]
        public void Rook_Can_Not_Moving_Beeing_Blocked_By_Own_Piece()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Rook }); // Place the Rook on the board
            board.SetPiece(new Square { Row = 4, Col = 5 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the rook's movement to the right
            board.SetPiece(new Square { Row = 5, Col = 4 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the rook's movement downwards
            board.SetPiece(new Square { Row = 4, Col = 3 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the rook's movement to the left
            board.SetPiece(new Square { Row = 3, Col = 4 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the rook's movement upwards

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Equal(0, moves?.Count); // The rook should not have any moves if it is completely blocked by its own pieces
        }

        /// <summary>
        /// Bishop Tests.
        /// </summary>

        [Fact]
        public void Bishop_Has_13_Moves_From_Center()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 3, Col = 3 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Bishop }); // Place the Bishop on the board

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Equal(13, moves.Count);
        }

        [Fact]
        public void Bishop_In_The_Corner_Should_Have_7_Moves()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 0, Col = 0 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Bishop }); // Place the Bishop in the corner

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.Equal(7, moves.Count); // A bishop in the corner should have 7 possible moves (along the diagonal)
        }

        [Fact]
        public void Bishop_Can_Not_Move_In_Horizontal_Or_Vertical_Direction()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 3, Col = 3 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Bishop }); // Place the Bishop on the board

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 3 && m.To.Col == 4); // The bishop should not be able to move horizontally
            Assert.DoesNotContain(moves, m => m.To.Row == 4 && m.To.Col == 3); // The bishop should not be able to move vertically
        }

        [Fact]
        public void Bishop_Stopped_By_Own_Piece()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 3, Col = 3 };
            var blockingSquare = new Square { Row = 5, Col = 5 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Bishop }); // Place the Bishop on the board
            board.SetPiece(blockingSquare, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the bishop's movement along the diagonal

            // Act
            var moves = moveGenerator.GetMoves(board, from);

            // Assert
            Assert.DoesNotContain(moves, m => m.To.Row == 5 && m.To.Col == 5); // The bishop should not be able to move to a square occupied by its own piece
            Assert.DoesNotContain(moves, m => m.To.Row == 6 && m.To.Col == 6); // The bishop should not be able to move past its own piece
        }

        [Fact]
        public void Bishop_Can_Take_Enemy_Piece_And_Stop()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 3, Col = 3 };
            var blockingSquare = new Square { Row = 5, Col = 5 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Bishop }); // Place the Bishop on the board
            board.SetPiece(blockingSquare, new Piece { Color = PieceColor.Black, Type = PieceType.Pawn }); // Place an enemy piece blocking the bishop's movement along the diagonal
            // Act
            var moves = moveGenerator.GetMoves(board, from);
            // Assert
            Assert.Contains(moves, m => m.To.Row == 5 && m.To.Col == 5); // The bishop should be able to take an enemy piece
            Assert.DoesNotContain(moves, m => m.To.Row == 6 && m.To.Col == 6); // The bishop should not be able to move past the enemy piece
        }

        /// <summary>
        /// Queen Tests.
        /// </summary>

        [Fact]
        public void Queen_In_Center_Should_Have_27_Moves()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 3, Col = 3 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Queen }); // Place the Queen on the board
            // Act
            var moves = moveGenerator.GetMoves(board, from);
            // Assert
            Assert.Equal(27, moves.Count); // A queen in the center of the board should have 27 possible moves (combination of rook and bishop moves)
        }

        [Fact]
        public void Queen_In_The_Corner_Should_Have_21_Moves()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 0, Col = 0 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Queen }); // Place the Queen in the corner
            // Act
            var moves = moveGenerator.GetMoves(board, from);
            // Assert
            Assert.Equal(21, moves.Count); // A queen in the corner should have 21 possible moves (combination of rook and bishop moves along the edges)
        }

        [Fact]
        public void Queen_Blocked_By_Own_Piece_Should_Have_Zero_Moves()
        {
            // Arrange
            var board = new Board(new Piece[8, 8], PieceColor.White);
            var moveGenerator = new MoveGenerator();
            var from = new Square { Row = 4, Col = 4 };
            board.SetPiece(from, new Piece { Color = PieceColor.White, Type = PieceType.Queen }); // Place the Queen on the board
            board.SetPiece(new Square { Row = 4, Col = 5 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the queen's movement to the right
            board.SetPiece(new Square { Row = 5, Col = 4 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the queen's movement downwards
            board.SetPiece(new Square { Row = 4, Col = 3 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the queen's movement to the left
            board.SetPiece(new Square { Row = 3, Col = 4 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the queen's movement upwards
            board.SetPiece(new Square { Row = 5, Col = 5 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the queen's movement diagonally down-right
            board.SetPiece(new Square { Row = 5, Col = 3 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the queen's movement diagonally down-left
            board.SetPiece(new Square { Row = 3, Col = 5 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the queen's movement diagonally up-right
            board.SetPiece(new Square { Row = 3, Col = 3 }, new Piece { Color = PieceColor.White, Type = PieceType.Pawn }); // Place a piece blocking the queen's movement diagonally up-left
            // Act
            var moves = moveGenerator.GetMoves(board, from);
            // Assert
            Assert.Empty(moves); // The queen should have no available moves when blocked by its own pieces
        }
    }
}