using Microsoft.AspNetCore.SignalR.Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListLobbies : MonoBehaviour
{
    public async void InvokeLobbiesAsync()
    {
        if (SignalR.Instance.Connected)
        {
            await SignalR.Instance.Connection.InvokeAsync("getBareLobbies");
            Debug.Log("In Invoke Lobbies");
        }

    }
}
