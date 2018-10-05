using System.Collections;
using UnityEngine;

public class GoodBreathText : MonoBehaviour
{

    private static GameObject go;
    private static bool fadeIn;
    private bool enteredDelay;
    private bool playedSound;
    public static bool goodBreath;
    
    // Use this for initialization
    void Start()
    {
        goodBreath = false;
        fadeIn = false;
        enteredDelay = false;
        playedSound = false;

        go = gameObject;
    }
    
    void LateUpdate()
    {
        // Good breath - Well done text fade in and out
        if (goodBreath && !fadeIn && gameObject.GetComponent<CanvasGroup>().alpha == 0)
        {
            fadeIn = true;
            playedSound = false;
        }
        else
        {
            if (!enteredDelay)
            {
                if (fadeIn)
                {
                    if (!playedSound)
                    {
                        SoundControl.playWellDoneSound();
                        playedSound = true;
                    }
                    gameObject.GetComponent<CanvasGroup>().alpha += 0.025f;
                }
                else
                {
                    gameObject.GetComponent<CanvasGroup>().alpha -= 0.025f;
                }

                if (gameObject.GetComponent<CanvasGroup>().alpha == 1)
                {
                    StartCoroutine(Delay(3));
                }
            }
        }
    }

    IEnumerator Delay(float delay)
    {
        // Delay for text
        enteredDelay = true;
        yield return new WaitForSeconds(delay);
        enteredDelay = false;
        fadeIn = false;
        goodBreath = false;
    }

    public static void alpha0()
    {
        // Reset text back to alpha 0 - transparent
        go.GetComponent<CanvasGroup>().alpha = 0f;
        fadeIn = false;
        goodBreath = false;
    }

}
