using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveSpawner : MonoBehaviour
{
    float graveTimer = 0.0f;
    

    public GameObject[] graves;
  
    
    void Start()
    {
        
    }

    void Update()
    {    
        graveTimer += Time.deltaTime;
        

        if (graveTimer > 1.0f)
        {    
            int graveIndex = Random.Range(0, graves.Length);

            graves[graveIndex].GetComponent<Grave>().grave.SetActive(true);

          
            graveTimer = 0;     
        }
    }
}
