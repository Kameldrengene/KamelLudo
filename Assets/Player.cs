using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string _name;

    public Player(string name)
    {
        _name = name;
    }
    public string Name
    {
        get { return this._name; }
        set { this._name = value; }
    }
}
