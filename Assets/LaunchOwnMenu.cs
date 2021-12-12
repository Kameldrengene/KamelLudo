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
    public GameObject launchmenu;
    public GameObject selectMenu;

    // Start is called before the first frame update
    void Start()
    {
      

        if (SignalRGame.Connected)
        {
            SignalRGame.Connection.On<GameData>("LaunchGame", (game) =>
             {
                 launchGame();
             });
        }
        
        if (SignalR.Connected)
        {
            Debug.Log("in launch menu");

            SignalR.Connection.On<GameData>("RemoveGame", (gamedata) =>
            {
                if (!gameData.Leader.Name.Contains(Self.Instance.Name))
                {
                    Debug.Log("kick participant" + gamedata.GameName);
                    
                    selectMenu.SetActive(true);
                    launchmenu.SetActive(false);
                    
                }
               
            });

            SignalR.Connection.On<GameData>("RecieveUpdatedGame", (game) =>
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

            SignalR.Connection.On<GameData>("EnterGame", async (game) =>
            {
                Debug.Log(game.GameName);
                this.gameName.text = game.GameName;
                this.leader.text = game.Leader.Name;
                this.gameId = game.Id;
                this.gameData = game;
                this.playerList.text = "";
                await SignalRGame.Connection.InvokeAsync("AddToGroup", game.Id);

            });

        }
        
    }
    


    // Update is called once per frame
    void Update()
    {
       

    }

    public async void launchGameOnClick()
    {
       
        await SignalRGame.Connection.InvokeAsync("LaunchGame",gameId);

    }

    public void launchGame()
    {
        SignalRGame.Gameid = gameId;


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
       
    }

    public async void YesOnClick()
    {
        if (SignalR.Connected)
        {
            if (gameData.Leader.Name.Equals(Self.Instance.Name))
            {
                Debug.Log("deleting game" + gameId);
                await SignalR.Connection.InvokeAsync("deleteGame", gameId);
            }
            if (!gameData.Leader.Name.Equals(Self.Instance.Name))
            {
                Debug.Log("Removing participant " + Self.Instance.Name);
                await SignalR.Connection.InvokeAsync("RemoveParticipant", gameId, Self.Instance.Name);
            }
        }
    }
}
