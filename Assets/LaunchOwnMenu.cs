using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class LaunchOwnMenu : MonoBehaviour
{
    public TMP_Text gameName;
    public TMP_Text leader;
    public TMP_Text playerList;
    public string gameId;
    // Start is called before the first frame update
    void Start()
    {
        if (SignalR.Instance.Connected)
        {
            SignalR.Instance.Connection.On<GameData>("EnterGame", (game) =>
            {
                Debug.Log(game.GameName);
                gameName.text = game.GameName;
                leader.text = game.Leader.Name;
                gameId = game.Id;

            });
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void launchGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SignalRGame.Instance.Gameid = gameId;
       
    }

    public async void deleteGame()
    {
        Debug.Log("deleting game" + gameId);
        await SignalR.Instance.Connection.InvokeAsync("deleteGame", gameId);
    }
}
