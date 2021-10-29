using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(5,5,5);
        cube.transform.position = new Vector3(0,0,0);
        cube.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
