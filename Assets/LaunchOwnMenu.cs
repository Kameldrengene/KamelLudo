using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaunchOwnMenu : MonoBehaviour
{
    public TMP_Text gameName;
    public TMP_Text leader;
    public TMP_Text playerList;
    public string gameId;
    public GameData gameData;
    public Button mybutton;

    // Start is called before the first frame update
    void Start()
    {
      

        if (SignalRGame.Instance.Connected)
        {
            SignalRGame.Instance.Connection.On<GameData>("LaunchGame", (game) =>
             {
                 launchGame();
             });
        }
        
        if (SignalR.Instance.Connected)
        {
            Debug.Log("in launch menu");

            SignalR.Instance.Connection.On<GameData>("RecieveUpdatedGame", (game) =>
            {
                gameName.text = game.GameName;
                leader.text = game.Leader.Name;
                gameId = game.Id;
                playerList.text = "";
                Debug.Log("in receive");
                foreach (Player player in game.Participants)
                {
                    playerList.text += player.Name + "\n";
                }
                gameData = game;

                if (!gameData.Leader.Name.Equals(Self.Instance.Name))
                {
                    mybutton.interactable = false;
                }
            });

            SignalR.Instance.Connection.On<GameData>("EnterGame", (game) =>
            {
                Debug.Log(game.GameName);
                gameName.text = game.GameName;
                leader.text = game.Leader.Name;
                gameId = game.Id;
                gameData = game;

            });

        }
        
    }
    


    // Update is called once per frame
    void Update()
    {
       

    }

    public async void launchGameOnClick()
    {
       
        await SignalRGame.Instance.Connection.InvokeAsync("LaunchGame",gameId);

    }

    public void launchGame()
    {
        SignalRGame.Instance.Gameid = gameId;


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
       
    }

    public async void YesOnClick()
    {
        if (gameData.Leader.Name.Equals(Self.Instance.Name))
        {
            Debug.Log("deleting game" + gameId);
            await SignalR.Instance.Connection.InvokeAsync("deleteGame", gameId);
        }
        if (!gameData.Leader.Name.Equals(Self.Instance.Name))
        {
            Debug.Log("Removing participant " + Self.Instance.Name);
            await SignalR.Instance.Connection.InvokeAsync("RemoveParticipant", gameId, Self.Instance.Name);
        }

    }
}
