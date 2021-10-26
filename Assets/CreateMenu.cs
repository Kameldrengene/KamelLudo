using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

public class CreateMenu : MonoBehaviour
{
    public TMP_InputField gameName;

    public async void CreateGameAsync()
    {

        if (Singleton.Instance.Connected)
        {
            await Singleton.Instance.Connection.InvokeAsync("CreateLobby", Self.Instance.Name, gameName.text);
        }
        
    }
}
