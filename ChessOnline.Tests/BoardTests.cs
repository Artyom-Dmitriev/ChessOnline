using ChessOnline.Engine;
using ChessOnline.Engine.Models;

namespace ChessOnline.Tests;

public class BoardTests
{
    [Fact]
    public void Start_Position_Is_Correct()
    {
        // Arrange
        var board = new Board(new Piece[8, 8], PieceColor.White);

        // Act
        board.SetupInitialPosition();

        // Assert — Check a few pieces to ensure the setup is correct for whites
        Assert.Equal(PieceType.Rook, board.GetPiece(new Square { Row = 0, Col = 0 }).Type);
        Assert.Equal(PieceType.Rook, board.GetPiece(new Square { Row = 0, Col = 7 }).Type);
        Assert.Equal(PieceType.King, board.GetPiece(new Square { Row = 0, Col = 4 }).Type);
        Assert.Equal(PieceType.Pawn, board.GetPiece(new Square { Row = 1, Col = 0 }).Type);
        Assert.Equal(PieceType.None, board.GetPiece(new Square { Row = 3, Col = 3 }).Type);

        // Check a few pieces to ensure the setup is correct for blacks
        Assert.Equal(PieceType.Rook, board.GetPiece(new Square { Row = 7, Col = 0 }).Type);
        Assert.Equal(PieceType.Rook, board.GetPiece(new Square { Row = 7, Col = 7 }).Type);
        Assert.Equal(PieceType.King, board.GetPiece(new Square { Row = 7, Col = 4 }).Type);
        Assert.Equal(PieceType.Pawn, board.GetPiece(new Square { Row = 6, Col = 0 }).Type);
    }

    [Fact]
    public void Get_Piece_Is_Correct()
    {
        // Arrange
        var board = new Board(new Piece[8, 8], PieceColor.White);

        // Act
        board.SetPiece(new Square { Row = 4, Col = 4 },
            new Piece { Color = PieceColor.White, Type = PieceType.Queen });

        // Assert
        Assert.Equal(PieceType.Queen, board.GetPiece(new Square { Row = 4, Col = 4 }).Type);
        Assert.Equal(PieceColor.White, board.GetPiece(new Square { Row = 4, Col = 4 }).Color);
    }

    [Fact]
    public void Clone_Does_Not_Change_Original()
    {
        //Arrange
        var board = new Board(new Piece[8, 8], PieceColor.White);

        //Act
        var clone = board.Clone();

        clone.SetPiece(new Square { Row = 3, Col = 3 },
            new Piece { Type = PieceType.King, Color = PieceColor.White });

        //Assert
        Assert.NotEqual(PieceType.King, board.GetPiece(new Square { Row = 3, Col = 3 }).Type);
    }
}
