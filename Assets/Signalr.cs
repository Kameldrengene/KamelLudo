using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

public class Signalr : MonoBehaviour
{
    //private static HubConnection connection;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World!");
        

        Singleton.Instance.EstablishConnection("http://localhost:5000/lobbyHub");

        Singleton.Instance.Connection.On<string>("Connected", (connetionid) =>
        {
            Debug.Log(connetionid);

        });
      
        Connect();
    }

    private async void Connect()
    {
        try
        {
            
            await Singleton.Instance.Connection.StartAsync();
            Singleton.Instance.Connected = true;

            Debug.Log("connection started");
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
