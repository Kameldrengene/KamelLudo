using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateBoard : MonoBehaviour
{

    private List<int> piecesAlive = new List<int> { 4, 4, 4, 4 };
    public List<Piece> pieces = Board.pieces;
    public List<Vector3> pVector = new List<Vector3>();

    private List<Vector3> getAllPos()
    {
        if(pVector.Count > 0)
        {
            return pVector;
        }
        List<Vector3> tempList = new List<Vector3>();
        foreach(Piece p in pieces)
        {
            tempList.Add(p.pieceObject.transform.position);
        }
        return tempList;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void onBlueClick()
    {
        //UpdateDeadPieces();
        pVector = getAllPos();
        UpdatePiecesPosition();
    }

    private int tick = 0;
    private void UpdatePiecesPosition()
    {
        if (tick == 0)
        {
            pieces[0].field = CreateFields.Instance.getStart(pieces[0].getPieceColor());
            Vector3 pos = pieces[0].field.field.transform.position;
            pieces[0].pieceObject.transform.position = new Vector3(pos[0], pos[1], pieces[0].pieceObject.transform.position[2]);
            tick++;
            Debug.Log("pos: " + pos);
        }
        else
        {
            Debug.Log("Piece location updated");
            pieces[0].pieceObject.transform.position = pVector[0];
            tick = 0;
        }
        Debug.Log("pos T: " + pVector[0]);

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
