using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBoard : MonoBehaviour
{

    private List<int> piecesAlive = new List<int> { 4, 4, 4, 4 };
    public List<Piece> pieces = Board.pieces;
    public List<Vector3> oldLocation = Board.locations;
    private Button rollButton;
    private List<Button> buttonPiece = new List<Button>();
    private List<PieceData> currPlayerPiece = new List<PieceData>();
    private List<Field>[] fields = Board.fieldList;
    private int roll;
    private GameObject turnText;
    private GameObject rollText;
    private GameObject gameText;
    private TextMesh tt;
    private TextMesh rt;
    private TextMesh gt;
    private List<PieceData> pData;


    // Start is called before the first frame update
    async void Start()
    {
        LoadGameObjects();
        SignalRGame.Instance.Connection.On<GameData>("UpdateGame", (game) =>
        {
            Debug.Log("Board hello: From Board data caller");
            Debug.Log("Board data test: " + game.Game.IsWon);
            pData = JsonSerializer.Deserialize<List<PieceData>>(game.Game.Pieces);
            Debug.Log("pData size: " + pData.Count());
            UpdateData.Instance.PData = pData;
            UpdateData.Instance.GData = game;
            Debug.Log("Board Game Roll: " + game.Game.Roll);
            Debug.Log("Board player: " + game.Game.CurrentPlayer);
            Debug.Log("Piece: " + game.Game.Pieces);
            setRollButton(false);
            updateGame();
            Debug.Log("Board exist: "+game.Game);
            


        });
        SignalRGame.Instance.Connection.On<GameData>("GetGame", (game) =>
        {
            Debug.Log("Game name: " + game.GameName);
            Debug.Log("Game ID: " + game.Id);
            Debug.Log("Game Participants: " + game.Participants.Count());
            Debug.Log("Game Leader: " + game.Leader.Name);
            //Add players to one list
            UpdateData.Instance.Players.Add(game.Leader);
            for(int i = 0; i < game.Participants.Count();i++)
            {
                UpdateData.Instance.Players.Add(game.Participants[i]);
            }
            int it = 0;
            foreach(Player p in UpdateData.Instance.Players)
            {
                Debug.Log("Players: " + p.Name);
                if (Self.Instance.Name.Equals(p.Name))
                {
                    UpdateData.Instance.PlayerIndex = it;
                }
                it++;
            }
            gt.text = "Game: " + game.GameName;


        });
        await SignalRGame.Instance.Connection.InvokeAsync("GetGame", SignalRGame.Instance.Gameid);
        
        await SignalRGame.Instance.Connection.InvokeAsync("UpdateGame", SignalRGame.Instance.Gameid, "0", "0","0"); //ID, Roll, LegalMoves, PieceID
        Debug.Log(SignalRGame.Instance.Gameid);
        Debug.Log("START LOADED!!!!");
        Debug.Log("SignalR Game Connected: " + SignalRGame.Instance.Connected);
        Debug.Log("Game loaded");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void increasePlayerIndex()
    {
        LoadGameObjects();

        UpdateData.Instance.PlayerIndex = (UpdateData.Instance.PlayerIndex + 1) % 4;
        Button turnB;
        turnB = GameObject.Find("TurnB").GetComponent<Button>();
        Text text = turnB.GetComponent<Text>();
        switch ((int)UpdateData.Instance.GData.Game.CurrentPlayer)
        {
            case 0:
                text.color = Color.yellow;
                break;
            case 1:
                text.color = Color.green;
                break;
            case 2:
                text.color = Color.red;
                break;
            case 3:
                text.color = Color.blue;
                break;
            default:
                return;
                break;
        }
        checkMyTurn();

    }

    private IEnumerator UpdatePiecesFrame()
    {
        yield return new WaitForSeconds(1.0f);
        /*if (piecesAlive[0] > 0)
            piecesAlive[0] -= 1;
        else
            piecesAlive[0] += 4;*/
        UpdateDeadPieces();
        StartCoroutine(UpdatePiecesFrame());
    }

    private void updateGame()
    {
        checkMyTurn();

        tt.text = "Turn: " + UpdateData.Instance.GData.Game.CurrentPlayer.ToString();
        rt.text = "Roll: " + UpdateData.Instance.GData.Game.Roll;
        
        for(int i = 0; i < pData.Count(); i++)
        {
            /*pData[0].IsInPlay = true;
            pData[0].FieldID = 1;
            pData[0].FieldQuadrant = 1;*/
            Debug.Log("pData piececolor: " + pData[0].PieceColor);
            //If Piece is not in play and is not done
            if (!pData[i].IsInPlay && !pData[i].IsDone)
            {
                pieces[i].pieceObject.transform.position = oldLocation[i]; //Set to home position
            } else if(pData[i].IsDone) //If piece is done, remove it
            {
                pieces[i].pieceObject.SetActive(false);
            }
            else if(pData[i].IsInPlay) //If piece is in play, then set to fieldID
            {
                pieces[i].pieceObject.transform.position = fields[pData[i].FieldQuadrant][pData[i].FieldID].field.transform.position;
            }
        }
        
    }

    public void checkMyTurn() {
        if((PieceColor)UpdateData.Instance.PlayerIndex == UpdateData.Instance.GData.Game.CurrentPlayer)
        {
            setRollButton(true);
        }
        else
        {
            setRollButton(false);
        }
        
    }

    public void LoadGameObjects()
    {
        turnText = GameObject.Find("turntext");
        tt = turnText.GetComponent<TextMesh>();
        rollText = GameObject.Find("rollText");
        rt = rollText.GetComponent<TextMesh>();
        gameText = GameObject.Find("gameText");
        gt = gameText.GetComponent<TextMesh>();
        rollButton = GameObject.Find("Roll").GetComponent<Button>();
    }

    public void setRollButton(bool b)
    {
        LoadGameObjects();
        rollButton.enabled = b;
        rollButton.interactable = b;
    }
    public void disablePieceButtons()
    {
        foreach (Button b in buttonPiece)
        {
            b.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            b.enabled = false;
            b.interactable = false;
        }
    }

    public async void OnRollClick()
    {
        //UpdateDeadPieces();
        //UpdatePiecesPosition();

        LoadGameObjects();
        roll = Dice.Instance.roll();
        UpdateData.Instance.LegalMoves = 0;
        Debug.Log("Roll: " + roll);
        rt.text = "Roll: " + roll;
        currPlayerPiece.Clear();
        Debug.Log("UpdateData pData: " + UpdateData.Instance.PData.Count());
        Debug.Log("UpdateData gData: " + UpdateData.Instance.GData.GameName);
        //TODO: Get player color instead of piececolor blue <-----
        foreach(PieceData p in UpdateData.Instance.PData)
        {
            if(p.PieceColor == UpdateData.Instance.GData.Game.CurrentPlayer)
            {
                currPlayerPiece.Add(p);
                Debug.Log("Piece added: " + p.PieceColor);
            }
        }
        buttonPiece.Clear();
        for(int i = 1; i < 5; i++)
        {
            buttonPiece.Add(GameObject.Find("P" + i.ToString()).GetComponent<Button>());
        }
        
        int it = 0;
        foreach(Button b in buttonPiece)
        {
            
            if (currPlayerPiece[it].isMoveable(roll) && currPlayerPiece[it].PieceColor == UpdateData.Instance.GData.Game.CurrentPlayer)
            {
                Debug.Log("Moveable piece: " + currPlayerPiece[it].PieceColor + " " + it);
                Debug.Log("Moveable piece (Playercolor): " + UpdateData.Instance.GData.Game.CurrentPlayer);
                UpdateData.Instance.LegalMoves++;
                b.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                b.enabled = true;
                b.interactable = true;
                setRollButton(false);
            }
            else
            {
                b.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                b.enabled = false;
                b.interactable = false;
            }
            it++;
        }
        if (UpdateData.Instance.LegalMoves <= 0)
        {
            //Send choice to server with roll and legalMoves;
            await TakeTurn(roll, UpdateData.Instance.LegalMoves, 0);
        }

    }

    private async Task TakeTurn(int r, int lm, int pid)
    {

        await SignalRGame.Instance.Connection.InvokeAsync("UpdateGame", SignalRGame.Instance.Gameid, r.ToString(), lm.ToString(), pid.ToString());

    }




    public async void OnPieceClick(int p)
    {
        if(currPlayerPiece[p].isMoveable(roll))
        {
            
            if (!currPlayerPiece[p].IsInPlay)
            {
                currPlayerPiece[p].IsInPlay = true;
            }
        }
        disablePieceButtons();
        setRollButton(true);
        await TakeTurn(roll, UpdateData.Instance.LegalMoves, p);


    }

    private void UpdatePiecesPosition(Piece p)
    {

        p.field = CreateFields.Instance.getStart(p.getPieceColor());
        Vector3 pos = p.field.field.transform.position;
        p.pieceObject.transform.position = new Vector3(pos[0], pos[1], p.pieceObject.transform.position[2]);
        Debug.Log("pos: " + pos);



    }

    private void UpdateDeadPieces()
    {
        if (piecesAlive[0] > 0)
            piecesAlive[0] -= 1;
        else
            piecesAlive[0] += 4;
        int blue = piecesAlive[0], yellow = piecesAlive[1], green = piecesAlive[2], red = piecesAlive[3];
        foreach (Piece p in pieces)
        {
            switch (p.getPieceColor())
            {
                case PieceColor.blue:
                    if (blue > 0)
                    {
                        p.pieceObject.SetActive(true);
                        blue--;
                    }
                    else
                        p.pieceObject.SetActive(false);
                    break;
                case PieceColor.yellow:
                    if (yellow > 0)
                    {
                        p.pieceObject.SetActive(true);
                        yellow--;
                    }
                    else
                        p.pieceObject.SetActive(false);
                    break;
                case PieceColor.green:
                    if (green > 0)
                    {
                        p.pieceObject.SetActive(true);
                        green--;
                    }
                    else
                        p.pieceObject.SetActive(false);
                    break;
                case PieceColor.red:
                    if (red > 0)
                    {
                        p.pieceObject.SetActive(true);
                        red--;
                    }
                    else
                        p.pieceObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
