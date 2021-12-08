using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;

public class Singleton
{
    private static readonly object servicelock = new object();
    private static Singleton instance = null;
    private HubConnection _connection;
    private bool _connected;
    private string _token = null;

    Singleton() { _connected = false; }

    public static Singleton Instance
    {
        get
        {
            lock (servicelock)
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }
    }
    public HubConnection Connection
    {
        get { return _connection; }
    }
    public void EstablishConnection(string url)
    {
        try
        {
            _connection = new HubConnectionBuilder().WithUrl(url).Build();
        }catch (System.Exception ex)
        {
            Debug.Log(ex);
            this.Connected = false;
        }
    }

    public string Token
    {
        get { return this._token; }
        set { this._token = value; }
    }

    public bool Connected
    {
        get { return this._connected; }
        set { this._connected = value; }
    }
}
