using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;

public class SignalR
{
    private static readonly object servicelock = new object();
    private static SignalR instance = null;
    private HubConnection _connection;
    private bool _connected;
    private string _token = null;
    private string _connectionString = "http://localhost:5000";

    SignalR() { _connected = false; }

    public static SignalR Instance
    {
        get
        {
            lock (servicelock)
            {
                if (instance == null)
                {
                    instance = new SignalR();
                }
                return instance;
            }
        }
    }
    public HubConnection Connection
    {
        get { return _connection; }
    }
    public void EstablishConnection()
    {
        Debug.Log(ConnectionString);
        try
        {
            _connection = new HubConnectionBuilder().WithUrl(ConnectionString + "/lobbyHub").Build();
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

    public string ConnectionString
    {
        get { return this._connectionString; }
        set { this._connectionString = value; }
    }
}
