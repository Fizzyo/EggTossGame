using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUI;
    public static bool gameIsOver = false;
    private bool playedSound;
    
    // Update is called once per frame
    void Update()
    {
        // When game is over
        if (gameIsOver == true)
        {
            gameOverMenuUI.SetActive(true);
            if (!playedSound)
            {
                SoundControl.playGameOverSound();
                GoodBreathText.alpha0();
                BadBreathText.alpha0();
                playedSound = true;
            }
        }
        else
        {
            gameOverMenuUI.SetActive(false);
            playedSound = false;
        }
    }
}
