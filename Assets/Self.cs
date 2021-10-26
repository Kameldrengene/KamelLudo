using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Self
{
    private static readonly object servicelock = new object();
    private static Self _instance = null;
    private string _name;

    private Self()
    {
    }

    public static Self Instance
    {
        get
        {
            lock (servicelock)
            {
                if (_instance == null)
                {
                    _instance = new Self();
                }
                return _instance;
            }
        }
    }

    public string Name
    {
        get { return this._name; }
        set { this._name = value; }
    }
}