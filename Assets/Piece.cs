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
    protected GameObject pieceObject;
    protected Player owner;

    protected Piece(GameObject pieceObject){
        this.pieceObject = pieceObject;
    }
}

public class RedPiece : Piece
{
    public RedPiece(GameObject piece) : base(piece)
    {
        this.pieceColor = PieceColor.red;
    }
}


public class BluePiece : Piece
{
    public BluePiece(GameObject piece) : base(piece)
    {
        this.pieceColor = PieceColor.blue;
    }
}

public class YellowPiece : Piece
{
    public YellowPiece(GameObject piece) : base(piece)
    {
        this.pieceColor = PieceColor.yellow;
    }
}

public class GreenPiece : Piece
{
    public GreenPiece(GameObject piece) : base(piece)
    {
        this.pieceColor = PieceColor.green;
    }
}