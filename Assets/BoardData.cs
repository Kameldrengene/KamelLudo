using System.Collections;
using System.Collections.Generic;

public class BoardData
{
    private string _pieces;
    private PieceColor _currentPlayer;
    private bool _isWon;
    private int _roll;

    public BoardData()
    {

    }

    public BoardData(string ps, PieceColor currentPlayer, bool isWon, int roll)
    {
        this._pieces = ps;
        this._currentPlayer = currentPlayer;
        this._isWon = isWon;
        this._roll = roll;
    }

    public string Pieces
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

