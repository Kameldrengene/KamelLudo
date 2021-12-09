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
    // Start is called before the first frame update
    void Start()
    {
        
            Singleton.Instance.Connection.On<List<GameData>>("ReceiveLobbies", (lobbies) =>
            {
                Debug.Log("im here");
                Debug.Log("Lobbies: " + lobbies.Count);
                foreach (var item in lobbies)
                {
                    GameObject lobby = Instantiate(listItemObj, lobbyListHolder);
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
        if (Singleton.Instance.Connected)
        {
           await Singleton.Instance.Connection.InvokeAsync("getLobbies");
        }

    }

    public void onJoinClick(GameData gamedata)
    {
        Debug.Log("clicked " + gamedata.GameName);
    }
}

