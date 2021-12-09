using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

public class GameList : MonoBehaviour
{
     
    public GameObject listItemObj;
    public Transform lobbyListHolder;
    public List<GameData> ListLobbies { get; set; }
    public List<GameObject> Lobbies { get; private set; }
    // Start is called before the first frame update
    void Start()
    {

        SignalR.Instance.Connection.On<List<GameData>>("ReceiveLobbies", (lobbies) =>
            {
                Debug.Log("im here");
                Debug.Log("Lobbies: " + lobbies.Count);
                foreach (var item in lobbies)
                {
                    GameObject lobby = Instantiate(listItemObj, lobbyListHolder);
                    lobby.name = item.GameName;
                    Button joinbtn = lobby.GetComponentInChildren<Button>();
                    TMP_Text[] texts = lobby.GetComponentsInChildren<TMP_Text>();
                    texts[0].text = item.Leader.Name;
                    texts[1].text = item.GameName;
                    if(item.Participants.Count == 0)
                    {
                        texts[2].text = "ingen";
                    }
                    else
                    {
                        texts[2].text = item.Participants.Count.ToString();
                    }
                    Debug.Log("particia " + item.Participants.Count.ToString());               
                    joinbtn.onClick.AddListener(()=> onJoinClick(item));
                    Lobbies.Add(lobby);
                }
            });

        SignalR.Instance.Connection.On<GameData>("ReceiveGame", (game) =>
        {
            Debug.Log("received game");
    
            
                GameObject lobby = Instantiate(listItemObj, lobbyListHolder);
                Button joinbtn = lobby.GetComponentInChildren<Button>();
                TMP_Text[] texts = lobby.GetComponentsInChildren<TMP_Text>();
                texts[0].text = game.Leader.Name;
                texts[1].text = game.GameName;
                if (game.Participants.Count == 0)
                {
                    texts[2].text = "ingen";
                }
                else
                {
                    texts[2].text = game.Participants.Count.ToString();
                }
                Debug.Log("particia " + game.Participants.Count.ToString());
                joinbtn.onClick.AddListener(() => onJoinClick(game));
           
        });

        SignalR.Instance.Connection.On<GameData>("RemoveGame", (gamedata) =>
        {
            Debug.Log("remove game" + gamedata.GameName);
            
            foreach (var item in Lobbies)
            {
                if(item.name == gamedata.GameName)
                {
                    Destroy(item.gameObject);
                }
            }
        });


        ArrayList arlist = new ArrayList()
                        {
                            1,
                            "Bill",
                            300,
                            4.5F,
                            1,
                            "Bill",
                            300,
                            4.5F
                        };
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public async Task InvokeLobbiesAsync()
    {
        if (SignalR.Instance.Connected)
        {
           await SignalR.Instance.Connection.InvokeAsync("getLobbies");
        }

    }

    public async void onJoinClick(GameData gamedata)
    {
        Debug.Log("clicked name: " + gamedata.GameName + " id:" + gamedata.Id );
        if (SignalR.Instance.Connected)
        {
            //await SignalR.Instance.Connection.InvokeAsync("CreateLobby", Self.Instance.Name, gameName.text);
        }
    }
}

