using Microsoft.AspNetCore.SignalR.Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalRGameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World!");
        SignalRGame.Instance.EstablishConnection();

        SignalRGame.Instance.Connection.On<string>("Connected", (connetionid) =>
        {
            Debug.Log(connetionid);

        });

        Connect();
    }


    private async void Connect()
    {
        try
        {

            await SignalRGame.Instance.Connection.StartAsync();
            SignalRGame.Instance.Connected = true;

            Debug.Log("connection started gamehub");
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
