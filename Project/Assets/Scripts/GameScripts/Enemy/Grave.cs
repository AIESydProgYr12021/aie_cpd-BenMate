using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour
{
  
    public GameObject[] zombies;
    public GameObject grave;

    float timer = 0.0f;

    void Update()
    {
        if (!grave.activeSelf)
            return;
      
        timer += Time.deltaTime;

        if (timer > 2.0f)
        {
            int randEnemy = Random.Range(0, zombies.Length);

            Instantiate(zombies[randEnemy],transform.position, transform.rotation);

            grave.SetActive(false);
            timer = 0;
        }


    }
}
