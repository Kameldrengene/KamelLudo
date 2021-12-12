using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SignalRGame : MonoBehaviour
{
    // Start is called before the first frame update
    private static HubConnection _connection;
    private static bool _connected;
    private static string _token = null;
    private static string _connectionString = "http://localhost:5000";
    private static string _gameid = null;

    SignalRGame() { _connected = false; }


    public static HubConnection Connection
    {
        get { return _connection; }
        private set { _connection = value; }
    }
    public void EstablishConnection()
    {
        Debug.Log(ConnectionString);
        try
        {
            _connection = new HubConnectionBuilder().WithUrl(ConnectionString + "/gameHub").WithAutomaticReconnect().Build();
            _connection.On<string>("Connected", (connetionid) =>
            {
                Debug.Log(connetionid);
                Ping();

            });


            Connect();
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex);
            Connected = false;
        }
    }

    public async void Ping()
    {
        _connection.On("Ping", () =>
        {
            Debug.Log("server pinged");
        });

        while (true)
        {
            await Task.Delay(UnityEngine.Random.Range(1, 3) * 10000);
            await _connection.InvokeAsync("Ping");
        }

    }

    public async void Connect()
    {

        try
        {
            await _connection.StartAsync();
            Connected = true;
            Debug.Log("Connection started");
        }
        catch (Exception ex)
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
    public static string Gameid
    {
        get { return _gameid; }
        set { _gameid = value; }
    }
}
