using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceColor
{
    blue = 3,
    yellow = 0,
    green = 1,
    red = 2
}

public abstract class Piece
{
    protected PieceColor pieceColor;
    public GameObject pieceObject { get; set; }
    public Field  field{ get; set; }
    protected Player owner;
    public int pieceID { get; set; }
    private bool isInPlay { get; set; }
    private bool isDone { get; set; }
    protected Piece(GameObject pieceObject, int id){
        this.pieceObject = pieceObject;
        this.pieceID = id;
        this.isDone = false;
        this.isInPlay = false;
    }

    public PieceColor getPieceColor(){
        return pieceColor;
    }

    public bool isMoveable(int roll)
    {
        if (roll == 6 && !isDone)
            return true;
        else if (!isInPlay || isDone)
            return false;
        return true;
    }
}

public class RedPiece : Piece
{
    public RedPiece(GameObject piece, int id) : base(piece, id)
    {
        this.pieceColor = PieceColor.red;
    }
}


public class BluePiece : Piece
{
    public BluePiece(GameObject piece, int id) : base(piece, id)
    {
        this.pieceColor = PieceColor.blue;
    }
}

public class YellowPiece : Piece
{
    public YellowPiece(GameObject piece, int id) : base(piece, id)
    {
        this.pieceColor = PieceColor.yellow;
    }
}

public class GreenPiece : Piece
{
    public GreenPiece(GameObject piece, int id) : base(piece, id)
    {
        this.pieceColor = PieceColor.green;
    }
}