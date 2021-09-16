using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour
{
    private void PlayFootStepsSound()
    {
        FindObjectOfType<AudioManager>().Play("WalkSound");       
    }
}
