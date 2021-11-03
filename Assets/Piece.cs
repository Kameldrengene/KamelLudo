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
}
