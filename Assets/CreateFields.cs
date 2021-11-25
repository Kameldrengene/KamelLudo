using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateFields : MonoBehaviour
{
    private Vector3 defaultSize = new Vector3(2.1f,2.1f,0.1f);
    private float defaultX = -2.2f;
    private float defaultY = 4.33f;
    private float defaultZ = -0.52f;
    private List<Field>[] fieldList = { new List<Field>(), new List<Field>(), new List<Field>() , new List<Field>() };
    private List<Field>[] dasdasd;

    public Field getStart(PieceColor pieceColor)
    {
        return fieldList[(int)pieceColor][16];
    }

    public Field getBank(PieceColor pieceColor)
    {
        return fieldList[(int)pieceColor][10];
    }

    enum FieldType
    {
        DefaultField = 0,
        Star = 1,
        Globe = 2,
        Entrance = 3
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeFields();
        Debug.Log(fieldList[0].Count);
        //AnimatedMove();
    }

    //private int nextUpdate = 1;
    
    // Update is called once per frame
    void Update()
    {
        /*if (Time.time >= nextUpdate)
        {*/
            //Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            //nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            // Call your fonction
       AnimatedMove();

        //}


    }
    void AnimatedMove()
    {
        Debug.Log("Length " + fieldList.Length);
        for(int i = 0; i < fieldList.Length; i++)
        {
            Debug.Log("I length " + fieldList[i].Count);
            for(int j = 0; j < fieldList[i].Count; j++)
            {
                if (fieldList[i][j].NextField(null) != null)
                {
                    Debug.Log(i+" "+j);
                    fieldList[i][j].NextField(null).field.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                }
            }
        }
    }

    private void AssignNextFields()
    {
        for (int i = 0; i < fieldList.Length;i++)
        {
            for (int j = 0; j < fieldList[i].Count; j++)
            {
                if(j < 5)
                {
                    fieldList[i][j].nextField=fieldList[i][j + 1];
                } else if(j == 5)
                {
                    fieldList[i][j].nextField=fieldList[i][11];
                } else if(j == 11)
                {
                    fieldList[i][j].nextField=fieldList[i][17];
                } else if(j == 6)
                {
                    fieldList[i][j].nextField = null;
                }
            }
        }
        
    }

    private void MakeFields()
    {
        
        for (int i = 0; i < 4; i++)
        {
            for (int k = 0; k < 3; k++)
            {
                for (int j = 0; j < 6; j++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.localScale = defaultSize;
                    //For each rotation change from (x,y) to (y,-x)
                    float[] pos = { 2.2f * k + defaultX, 2.2f * j + defaultY, defaultZ };
                    Vector3 newPos = new Vector3(pos[0], pos[1], pos[2]);
                    if (i == 1)
                        newPos = new Vector3(pos[1], -pos[0], pos[2]);
                    else if(i == 2)
                        newPos = new Vector3(-pos[0], -pos[1], pos[2]);
                    else if(i == 3)
                        newPos = new Vector3(-pos[1], pos[0], pos[2]);
                    cube.transform.position = newPos;
                    Field field = null;
                    if (j == 3 && k == 0) //Globus
                    {
                        field = new GlobusField(cube);
                        cube.GetComponent<Renderer>().material.color = new Color32(255, 0, 255, 255);
                    }
                    else if (j == 4 && k == 2)//Globus and field
                    {
                        field = new SafeHomeField(cube);
                        cube.GetComponent<Renderer>().material.color = new Color32(255, 100, 255, 255);
                    }
                    else if (j == 5 && k == 1) //Entrance
                    {
                        field = new EntranceField(cube);
                        cube.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 255);
                    }
                    else if (j == 0 && k == 0) //Star
                    {
                        field = new StarField(cube);
                        cube.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 255);
                    }
                    else //Default
                    {
                        field = new NormalField(cube);
                        cube.GetComponent<Renderer>().material.color = new Color32(0, 255, 0, 255);
                    }
                    fieldList[i].Add(field);
                }
            }            
        }
        AssignNextFields();
    }
}
