using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenuUI;
    public static bool gameIsOver = false;

    private bool playedSound;
    
    // Update is called once per frame
    void Update()
    {
        // When game is over
        if (gameIsOver)
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
        /*if (gameIsOver)
        {
            if (FizSession.sets > 0)
            {
                HealthControl.lives = FizSession.breaths;
                FizSession.sets--;
                SceneManager.LoadScene("Game");
                LevelGenerator.spawnPosY = -3.8f;
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
            gameIsOver = false;
        }*/
    }
}
