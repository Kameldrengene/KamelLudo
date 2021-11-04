using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreatePieces : MonoBehaviour
{

    private Vector3 defaultSize = new Vector3(2.1f, 2.1f, 0.1f);
    private float defaultX = -9.844f;
    private float defaultY = 7.719f;
    private float defaultZ = -1.29f;
    private float midStartY = 9.77f;
    private List<Piece>[] pieces = { new List<Piece>(), new List<Piece>(), new List<Piece>(), new List<Piece>() };
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
        float angle = 1.6f; //Why is 1.6f 90 ish degrees??????????????????????????
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
                    newPiece = new BluePiece(cylinder);

                }
                else if (i == 1) //Yellow
                {
                    newPos = new Vector3(-pos[0], -pos[1], pos[2]);
                    cylinder.transform.position = newPos;
                    cylinder.GetComponent<Renderer>().material.color = Color.yellow;
                    newPiece = new YellowPiece(cylinder);

                }
                else if (i == 2) //Green
                {
                    newPos = new Vector3(-pos[1], pos[0], pos[2]);
                    cylinder.transform.position = newPos;
                    cylinder.GetComponent<Renderer>().material.color = Color.green;
                    newPiece = new GreenPiece(cylinder);

                }
                else //Red
                {
                    newPos = new Vector3(pos[0],pos[1], pos[2]);
                    cylinder.transform.position = newPos;
                    cylinder.GetComponent<Renderer>().material.color = Color.red;
                    newPiece = new RedPiece(cylinder);

                }

            }
        }
        
    }
}
