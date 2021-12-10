using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBoard : MonoBehaviour
{

    private List<int> piecesAlive = new List<int> { 4, 4, 4, 4 };
    public List<Piece> pieces = Board.pieces;
    private List<Vector3> pVector = Board.locations;
    private Button rollButton;
    private List<Button> buttonPiece = new List<Button>();
    private List<Piece> currPlayerPiece = new List<Piece>();
    private int roll;
    private int legalMoves = 0;



    // Start is called before the first frame update
    async void Start()
    {
        SignalRGame.Instance.Connection.On<GameData>("GetGame", (game) =>
        {
            Debug.Log("Game name: " + game.GameName);
            Debug.Log("Game ID: " + game.Id);
        });
        await SignalRGame.Instance.Connection.InvokeAsync("GetGame", SignalRGame.Instance.Gameid);
        Debug.Log(SignalRGame.Instance.Gameid);
        Debug.Log("START LOADED!!!!");
        Debug.Log("SignalR Game Connected: " + SignalRGame.Instance.Connected);
        
        Debug.Log("Game loaded");
        
    }

    // Update is called once per frame
    void Update()
    {
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


    public void onRollClick()
    {
        //UpdateDeadPieces();
        //UpdatePiecesPosition();
        roll = Dice.Instance.roll();
        Debug.Log("Roll: " + roll);
        rollButton = GameObject.Find("Roll").GetComponent<Button>();
        currPlayerPiece.Clear();
        //TODO: Get player color instead of piececolor blue <-----
        foreach(Piece p in pieces)
        {
            if(p.getPieceColor() == PieceColor.blue)
            {
                currPlayerPiece.Add(p);
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
            
            if (currPlayerPiece[it].isMoveable(roll))
            {
                
                legalMoves++;
                b.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                b.enabled = true;
                b.interactable = true;
                rollButton.enabled = false;
                rollButton.interactable = false;
            }
            else
            {
                b.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                b.enabled = false;
                b.interactable = false;
            }
            it++;
        }

    }

    public void onPieceClick(int p)
    {
        if(currPlayerPiece[p].isMoveable(roll))
        {
            foreach (Button b in buttonPiece)
            {
                b.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                b.enabled = false;
                b.interactable = false;
            }
            rollButton.enabled = true;
            rollButton.interactable = true;
            if (!currPlayerPiece[p].isInPlay)
            {
                currPlayerPiece[p].isInPlay = true;
                UpdatePiecesPosition(currPlayerPiece[p]);
            }
        }
        

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
