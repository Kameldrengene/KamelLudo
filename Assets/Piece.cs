using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceColor
{
    blue = 0,
    yellow = 1,
    green = 2,
    red = 3
}

public abstract class Piece
{
    protected PieceColor pieceColor;
    protected GameObject position;
    protected Player owner;

    protected Piece(){}
}

public class RedPiece : Piece
{
    public RedPiece() : base()
    {
        this.pieceColor = PieceColor.red;
    }
}


public class BluePiece : Piece
{
    public BluePiece() : base()
    {
        this.pieceColor = PieceColor.blue;
    }
}

public class YellowPiece : Piece
{
    public YellowPiece() : base()
    {
        this.pieceColor = PieceColor.yellow;
    }
}

public class GreenPiece : Piece
{
    public GreenPiece() : base()
    {
        this.pieceColor = PieceColor.green;
    }
}