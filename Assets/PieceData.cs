using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PieceData
{
    private PieceColor _pieceColor;
    private int _pieceID;
    private int _fieldID;
    private int _fieldQuadrant;
    private bool _isInPlay;
    private bool _isDone;

    public bool isMoveable(int roll)
    {
        if (roll == 6 && !_isDone)
            return true;
        else if (!_isInPlay || _isDone)
            return false;
        return true;
    }

    public PieceData(PieceColor pc, int pID, int fID, int fQ, bool inPlay, bool Done)
    {
        this._pieceColor = pc;
        this._pieceID = pID;
        this._fieldID = fID;
        this._fieldQuadrant = fQ;
        this._isInPlay = inPlay;
        this._isDone = Done;
    }

    public PieceColor PieceColor{ get {return _pieceColor; } set {this._pieceColor = value; } }
    public int PieceID{ get {return _pieceID; } set {this._pieceID = value; } }
    public int FieldID{ get {return _fieldID; } set {this._fieldID = value; } }
    public int FieldQuadrant{ get {return _fieldQuadrant; } set {this._fieldQuadrant= value; } }
    public bool IsInPlay{ get {return _isInPlay; } set {this._isInPlay = value; } }
    public bool IsDone{ get {return _isDone; } set {this._isDone = value; } }
}

