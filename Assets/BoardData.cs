using System.Collections;
using System.Collections.Generic;

public class BoardData
{
    private List<PieceData> _pieces;
    private PieceColor _currentPlayer;
    private bool _isWon;
    private int _roll;

    public BoardData()
    {
        _pieces = new List<PieceData>();
    }
    public BoardData(List<PieceData> p, PieceColor cP, bool iw, int r)
    {
        _pieces = p;
        _currentPlayer = cP;
        _isWon = iw;
        _roll = r;
    }

    public List<PieceData> Pieces
    {
        get { return _pieces; }
        set { this._pieces = value; }
    }

    public PieceColor CurrentPlayer
    {
        get { return _currentPlayer; }
        set { this._currentPlayer = value; }
    }

    public bool IsWon
    {
        get { return _isWon; }
        set { this._isWon = value; }
    }
    public int Roll
    {
        get { return _roll; }
        set { this._roll = value; }
    }
}

