using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreateFields : MonoBehaviour
{
    private Vector3 defaultSize = new Vector3(2.1f,2.1f,0.1f);
    private float defaultX = -2.2f;
    private float defaultY = 4.33f;
    private float defaultZ = -0.52f;
    private List<MyField> fieldList = new List<MyField>();

    enum FieldType
    {
        DefaultField = 0,
        Star = 1,
        Globe = 2
    }

    private class MyField
    {
        public GameObject field;
        public FieldType fieldType = default;
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

    // Update is called once per frame
    void Update()
    {
        
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
                    cube.GetComponent<Renderer>().material.color = new Color32(255, 0, 255, 255);
                    MyField field = new MyField(cube);
                    field.fieldType = FieldType.DefaultField;
                    fieldList.Add(field);
                }
            }            
        }
    }
}
