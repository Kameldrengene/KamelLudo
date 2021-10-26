using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour
{
    public TMP_InputField playerName;
    
    public void SavePlayer()
    {
        Self.Instance.Name = playerName.text;
        Debug.Log(Self.Instance.Name);
    }

   
}
