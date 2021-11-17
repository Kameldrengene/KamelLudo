using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public TMP_InputField ipInfo;
    public string ip = "http://localhost:5000";
    public static Options options = null;
    public Button playButton;
    public Button playNextButton;

    private static readonly object servicelock = new object();

    Options() { }

    public static Options Instance
    {
        get
        {
            lock (servicelock)
            {
                if (options == null)
                {
                    options = new Options();
                }
                return options;
            }
        }
    }

    public void ConnectToIP()
    {
        ip = ipInfo.text;
        if(ip.Length == 0)
        {
            ip = "http://localhost:5000";
        }
        playButton.interactable = false;
        playNextButton.interactable = false;
        if(!(ip.Contains("http://") || ip.Contains("https://")))
        {
            ip = "http://" + ip;
        }
        Singleton.Instance.EstablishConnection(ip+ "/lobbyhub");
        Debug.Log(ip);

        Singleton.Instance.Connection.On<string>("Connected", (connetionid) =>
        {
            
            Debug.Log(connetionid);

        });
        Singleton.Instance.Connected = false;

        Connect();


    }


    private async void Connect()
    {
        try
        {
            await Singleton.Instance.Connection.StartAsync();
            Debug.Log("connection started");
            Singleton.Instance.Connected = true;
            playButton.interactable = true;
            playNextButton.interactable = true;
        }
        catch (System.Exception ex)
        {

            Debug.Log(ex.Message);
        }

    }
}
