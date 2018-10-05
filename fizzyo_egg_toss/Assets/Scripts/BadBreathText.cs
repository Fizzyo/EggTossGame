using System.Collections;
using UnityEngine;

public class BadBreathText : MonoBehaviour
{
    private static GameObject go;
    private static bool fadeIn;
    private bool enteredDelay;
    private bool playedSound;

    public static bool badBreath;
    public static bool badBreathCloud;

    // Use this for initialization
    void Start()
    {
        badBreath = false;
        fadeIn = false;
        enteredDelay = false;
        playedSound = false;

        go = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Since the Fizzyo Breath Analyser is faulty as the breath is always at 4 (best quality)
        // Press on the right CTRL then input breath to simulate a low quality breath
        if (Input.GetKey(KeyCode.RightControl))
        {
            badBreath = true;
        }
    }

    void LateUpdate()
    {
        // Bad breath - Try again text fade in and out
        if (badBreathCloud && !fadeIn && gameObject.GetComponent<CanvasGroup>().alpha == 0)
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
                        SoundControl.playTryAgainSound();
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
        badBreathCloud = false;
    }

    public static void alpha0()
    {
        // Reset text back to alpha 0 - transparent
        go.GetComponent<CanvasGroup>().alpha = 0f;
        fadeIn = false;
        badBreathCloud = false;
    }

}
