using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveSpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] zombies;

    float timer = 0.0f;

    void Start()
    {
        
    }

   
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3.0f)
        {
            int randEnemy = Random.Range(0, zombies.Length);
            int randspawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(zombies[randEnemy], spawnPoints[randspawnPoint].position, transform.rotation);

            timer = 0;
        }
    }
}
