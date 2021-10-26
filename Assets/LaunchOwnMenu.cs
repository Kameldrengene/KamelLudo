using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

public class LaunchOwnMenu : MonoBehaviour
{
    public TMP_Text gameName;
    public TMP_Text leader;
    public TMP_Text playerList;
    // Start is called before the first frame update
    void Start()
    {
        if (Singleton.Instance.Connected)
        {
            Singleton.Instance.Connection.On<GameData>("EnterGame", (game) =>
            {
                Debug.Log(game.GameName);
                gameName.text = game.GameName;
                leader.text = game.Leader.Name;

            });
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
