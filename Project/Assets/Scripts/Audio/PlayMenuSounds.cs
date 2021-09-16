using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuSounds : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuMusic");
    }
}
