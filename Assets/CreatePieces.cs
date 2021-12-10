using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class CreatePieces : MonoBehaviour
{
    private float defaultX = -9.844f;
    private float defaultY = 7.719f;
    private float defaultZ = -1.29f;
    private float midStartY = 9.77f;
    public List<Piece> pieces = Board.pieces;
    public List<Vector3> locations= Board.locations;
    // Start is called before the first frame update
    void Start()
    {
        MakePieces();

    }


    // Update is called once per frame


    void Update()
    {

    }

    

    private void MakePieces()
    {
        float angle = 1.5708f; //1.5708 radians almost equals 90 degrees
        for(int j = 0; j < 4; j++)
        {
            float[] pos = { (float)(-Math.Sin(angle*j)*(defaultY-midStartY)+defaultX), (float)(Math.Cos(angle * j) * (defaultY - midStartY) + defaultX), defaultZ };
            for (int i = 0; i < 4; i++)
            {
                GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                Vector3 newPos = new Vector3(pos[0],pos[1], pos[2]);
                cylinder.transform.rotation = new Quaternion(90f, 0f, 0f, 90f);
                Piece newPiece = null;
                if (i == 0) //Blue
                {
                    newPos = new Vector3(pos[1], -pos[0], pos[2]);
                    cylinder.transform.position = newPos;
                    cylinder.GetComponent<Renderer>().material.color = Color.blue;
                    newPiece = new BluePiece(cylinder, j+1);

                }
                else if (i == 1) //Yellow
                {
                    newPos = new Vector3(-pos[0], -pos[1], pos[2]);
                    cylinder.transform.position = newPos;
                    cylinder.GetComponent<Renderer>().material.color = Color.yellow;
                    newPiece = new YellowPiece(cylinder, j + 1);

                }
                else if (i == 2) //Green
                {
                    newPos = new Vector3(-pos[1], pos[0], pos[2]);
                    cylinder.transform.position = newPos;
                    cylinder.GetComponent<Renderer>().material.color = Color.green;
                    newPiece = new GreenPiece(cylinder, j + 1);

                }
                else //Red
                {
                    newPos = new Vector3(pos[0],pos[1], pos[2]);
                    cylinder.transform.position = newPos;
                    cylinder.GetComponent<Renderer>().material.color = Color.red;
                    newPiece = new RedPiece(cylinder, j + 1);

                }
                GameObject text = new GameObject();
                TextMesh t = text.AddComponent<TextMesh>();
                t.text = newPiece.pieceID+"";
                t.fontSize = 20;
                text.GetComponent<Renderer>().material.color = Color.black;
                text.transform.position = new Vector3(newPos[0]-0.5f,newPos[1],newPos[2]+1f);
                text.transform.parent = cylinder.transform;
                pieces.Add(newPiece);
                locations.Add(newPiece.pieceObject.transform.position);

            }
        }
        
    }
}
