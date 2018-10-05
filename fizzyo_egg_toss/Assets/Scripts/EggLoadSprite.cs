using System;
using UnityEngine;

public class EggLoadSprite : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        try
        {
            // Load currently selected egg sprite
            gameObject.GetComponent<SpriteRenderer>().sprite = EggLoadControl.returnSelectedEggSprite();
        }
        catch (Exception e)
        {
            Debug.Log("Start from MainMenu Scene for the Egg sprites to load correctly.");
        }
    }
}
