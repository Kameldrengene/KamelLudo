using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateData
{
    private List<PieceData> _pData;
    private GameData _gData;
    private List<Player> _players = new List<Player>();
    private int _legalMoves = 0;
    private int _playerIndex;
    private static UpdateData instance;
    public static UpdateData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UpdateData();
            }
            return instance;
        }
    }
    public List<PieceData> PData{ get { return _pData; } set { this._pData = value; } }
    public GameData GData{ get { return _gData; } set { this._gData = value; } }
    public List<Player> Players { get { return _players; } set { this._players = value; } }
    public int LegalMoves { get { return _legalMoves; } set { this._legalMoves= value; } }
    public int PlayerIndex { get { return _playerIndex; } set { this._playerIndex = value; } }
}
