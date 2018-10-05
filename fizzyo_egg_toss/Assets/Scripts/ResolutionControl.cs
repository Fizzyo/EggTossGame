using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionControl : MonoBehaviour
{

    void Update()
    {
        // Force screen resolution
        if (!Screen.fullScreen)
        {
            Screen.SetResolution(1366, 768, true);
        }
    }
}
