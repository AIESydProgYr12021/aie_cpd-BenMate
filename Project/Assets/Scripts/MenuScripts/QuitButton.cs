using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    bool onWindows = false;

    public GameObject quitButton;

    void Update()
    {

#if UNITY_STANDALONE
    onWindows = true;
#endif

        if (onWindows)
        {
            quitButton.SetActive(true);
        }

    }
}
