using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    private string _id;
    private string _gameName;
    private Player _leader;
    private List<Player> _participants;

    public GameData()
    {
        _participants = new List<Player>();
    }

    public GameData(string id, string gameName, Player leader, List<Player> participants)
    {
        _id = id;
        _gameName = gameName;
        _leader = leader;
        _participants = participants;
    }

    public string GameName
    {
        get { return _gameName; }
        set { this._gameName = value; }
    }
    public string Id
    {
        get { return _id; }
        set { this._id = value; }
    }

    public Player Leader
    {
        get { return _leader; }
        set { this._leader = value; }
    }
    public List<Player> Participants
    {
        get { return _participants; }
        set { this._participants = value; }
    }

    public override string ToString()
    {
        return String.Format("{0}    {1}    {2}", GameName, Leader, Participants.Count);
    }
}