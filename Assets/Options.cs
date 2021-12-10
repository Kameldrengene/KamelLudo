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
    public Button playButton;
    public Button playNextButton;

    public void ConnectToIP()
    {
        ip = ipInfo.text;
        if(ip.Length == 0)
        {
            ip = "http://localhost:5000";
        }
        /*
        if(!(ip.Contains("http://") || ip.Contains("https://")))
        {
            ip = "http://" + ip;
        }
        */
        SignalR.Instance.ConnectionString = ip;
        SignalRGame.Instance.ConnectionString = ip;
     
    }

}
