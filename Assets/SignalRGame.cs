using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalRGame
{
    // Start is called before the first frame update
    private static readonly object servicelock = new object();
    private static SignalRGame instance = null;
    private HubConnection _connection;
    private bool _connected;
    private string _token = null;
    private string _connectionString = "http://localhost:5000";
    private string _gameid = null;

    SignalRGame() { _connected = false; }

    public static SignalRGame Instance
    {
        get
        {
            lock (servicelock)
            {
                if (instance == null)
                {
                    instance = new SignalRGame();
                }
                return instance;
            }
        }
    }
    public HubConnection Connection
    {
        get { return _connection; }
        set { _connection = value; }
    }
    public void EstablishConnection()
    {
        Debug.Log(ConnectionString);
        try
        {
            _connection = new HubConnectionBuilder().WithUrl(ConnectionString + "/gameHub").WithAutomaticReconnect().Build();
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            this.Connected = false;
        }
        _connection.On<string>("Connected", (connetionid) =>
        {
            Debug.Log(connetionid);

        });
        Connect();

    }

    public async void Connect()
    {

        try
        {
            await _connection.StartAsync();
            Connected = true;
            Debug.Log("Connection started Game");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
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
    public string Gameid
    {
        get { return this._gameid; }
        set { this._gameid = value; }
    }
}
