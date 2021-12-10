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

    public PieceColor PieceColor { get => _pieceColor; set => _pieceColor = value; }
    public int PieceID { get => _pieceID; set => _pieceID = value; }
    public int FieldID { get => _fieldID; set => _fieldID = value; }
    public int FieldQuadrant { get => _fieldQuadrant; set => _fieldQuadrant = value; }
    public bool IsInPlay { get => _isInPlay; set => _isInPlay = value; }
    public bool IsDone { get => _isDone; set => _isDone = value; }
}

