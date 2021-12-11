using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System;

public class SignalR
{
    private static readonly object servicelock = new object();
    private static SignalR instance = null;
    private HubConnection _connection;
    private bool _connected;
    private string _token = null;
    private string _connectionString = "http://localhost:5000";
    private string _gameId = null;

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
        set { _connection = value; }
    }
    public void EstablishConnection()
    {
        Debug.Log(ConnectionString);
        try
        {
            _connection = new HubConnectionBuilder().WithUrl(ConnectionString + "/lobbyHub").WithAutomaticReconnect().Build();
            //_connection.Closed += async (error) =>
            //{
            //    await Task.Delay(1000);
            //    await _connection.StartAsync();
            //};
        }catch (Exception ex)
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
            Debug.Log("Connection started Lobby");
        }catch(Exception ex)
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
    public string GameId
    {
        get { return this._gameId; }
        set { this._gameId = value; }
    }
}
