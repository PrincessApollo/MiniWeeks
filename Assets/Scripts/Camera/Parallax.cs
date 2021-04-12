using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //Anton
    private float length, startPos;
    public GameObject cam;
    public float effect;

    void Start()
    {
        startPos = transform.position.x;        //Sets start position
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - effect));
        float distance = (cam.transform.position.x * effect);       //Sets distance based on where the camera is and how big parallax effect

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);      //Sets the new position

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;      //Repeats the background
    }
}
