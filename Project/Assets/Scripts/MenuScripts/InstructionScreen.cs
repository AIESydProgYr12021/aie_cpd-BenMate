using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionScreen : MonoBehaviour
{
    public GameObject helpPc;
    public GameObject helpAndroid;

    bool onAndroid = false;

    void Start()
    {

#if UNITY_ANDROID
        onAndroid = true;
#endif
        

    }

    void Update()
    {
        if (onAndroid)
        {
            helpAndroid.SetActive(true);
        }
        else
        {
            helpPc.SetActive(true);
        }

    }

}
