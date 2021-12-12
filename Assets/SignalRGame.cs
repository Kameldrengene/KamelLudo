using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SignalRGame : MonoBehaviour
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
            this.Connected = false;
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
