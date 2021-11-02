using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateFields : MonoBehaviour
{
    private Vector3 defaultSize = new Vector3(2.1f,2.1f,0.1f);
    private float defaultX = -2.2f;
    private float defaultY = 4.33f;
    private float defaultZ = -0.52f;
    private List<MyField>[] fieldList = { new List<MyField>(), new List<MyField>(), new List<MyField>() , new List<MyField>() };

    enum FieldType
    {
        DefaultField = 0,
        Star = 1,
        Globe = 2,
        Entrance = 3
    }

    private class MyField
    {
        public GameObject field;
        public FieldType fieldType = default;
        public MyField nextField = null;
        public MyField(GameObject field)
        {
            this.field = field;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeFields();
       

    }

    private int nextUpdate = 1;
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextUpdate)
        {
            Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            // Call your fonction
            AnimatedMove();
        }
    
        
    }
    void AnimatedMove()
    {
        
    }

    private void AssignNextFields()
    {
        for (int j = 0; j < fieldList.Length;j++)
        {
            for (int i = 0; i < fieldList[j].Count; i++)
            {

            }
        }
        
    }

    private void MakeFields()
    {
        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                for (int k = 0; k < 3; k++)
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
                    MyField field = new MyField(cube);
                    if (j == 4 && k == 2)
                    {
                        cube.GetComponent<Renderer>().material.color = new Color32(255, 0, 255, 255);
                        field.fieldType = FieldType.Globe;
                    }
                    else if (j == 3 && k == 0)
                    {
                        cube.GetComponent<Renderer>().material.color = new Color32(255, 0, 255, 255);
                        field.fieldType = FieldType.Globe;
                    }
                    else if (j == 5 && k == 1)
                    {
                        cube.GetComponent<Renderer>().material.color = new Color32(0, 0, 255, 255);
                        field.fieldType = FieldType.Entrance;
                    }
                    else
                    {
                        cube.GetComponent<Renderer>().material.color = new Color32(0, 255, 0, 255);
                        field.fieldType = FieldType.DefaultField;
                    }
                    fieldList[i].Add(field);
                }
            }            
        }
        AssignNextFields();
    }
}
