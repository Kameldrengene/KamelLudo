using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System;

public class SignalR : MonoBehaviour
{
    private static readonly object servicelock = new object();
    private static HubConnection _connection;
    private static bool _connected;
    private static string _token = null;
    private static string _connectionString = "http://localhost:5000";
    private static string _gameId = null;

    SignalR() { _connected = false; }

    public static HubConnection Connection
    {
        get { return _connection; }
        set { _connection = value; }
    }
    public void EstablishConnection()
    {
        Debug.Log(ConnectionString);
       
        _connection = new HubConnectionBuilder().WithUrl(ConnectionString + "/lobbyHub").WithAutomaticReconnect().Build();
  
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
            Debug.Log("Connection started");
        }catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }
       
    }

    public static string Token
    {
        get { return _token; }
        set { _token = value; }
    }

    public static bool Connected
    {
        get { return _connected; }
        set { _connected = value; }
    }

    public static string ConnectionString
    {
        get { return _connectionString; }
        set { _connectionString = value; }
    }
    public static string GameId
    {
        get { return _gameId; }
        set { _gameId = value; }
    }
}
