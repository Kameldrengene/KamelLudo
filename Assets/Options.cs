using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public TMP_InputField ipInfo;

    public void ConnectToIP()
    {
        Self.Instance.Name = ipInfo.text;
        if(Self.Instance.Name.Length == 0)
        {
            Self.Instance.Name = "http://localhost:5000";
        }
        Debug.Log(Self.Instance.Name+"/lobbyhub");
    }
}
