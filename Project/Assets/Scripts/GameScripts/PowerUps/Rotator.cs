using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed = 100;
    public float moveSpeed = 1;
    public float height = 0.2f;

    Vector3 pos;

    
    private void Start()
    {
        pos = transform.position;
    }

    void Update()
    {      
        //spin
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        //move up and down
        float newY = (Mathf.Sin(Time.time * moveSpeed) + 1) * (height / 2);
        transform.position = new Vector3(pos.x, pos.y + newY, pos.z);
    }
}
