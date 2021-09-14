using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAftertime : MonoBehaviour
{
    public float time = 5;

    private void Start()
    {
        Destroy(gameObject, time);  
    }

}
