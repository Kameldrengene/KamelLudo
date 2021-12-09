using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBoard : MonoBehaviour
{

    private List<int> piecesAlive = new List<int> { 4, 4, 4, 4 };
    public List<Piece> pieces = Board.pieces;

    // Start is called before the first frame update
    void Start()
    {
       //StartCoroutine(UpdatePiecesFrame());

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
        UpdatePieces();
        StartCoroutine(UpdatePiecesFrame());
    }

    public void onBlueClick()
    {
        if (piecesAlive[0] > 0)
            piecesAlive[0] -= 1;
        else
            piecesAlive[0] += 4;
        UpdatePieces();
    }

    private void UpdatePieces()
    {
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
