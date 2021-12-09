using Microsoft.AspNetCore.SignalR.Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListLobbies : MonoBehaviour
{
    public async void InvokeLobbiesAsync()
    {
        if (Singleton.Instance.Connected)
        {
            await Singleton.Instance.Connection.InvokeAsync("getBareLobbies");
            Debug.Log("In Invoke Lobbies");
        }

    }
}
