﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonControl : MonoBehaviour {

    public static bool gamePaused = false;

    private void resetState()
    {
        // Need to reset the essential static variables
        GameOverMenu.gameIsOver = false;
        LevelGenerator.spawnPosY = -3.8f;
        LevelGenerator.current_platform = 0;
        MovingPlatform.moveSpeedInit = 2f;
        ScoreControl.currentScore = 0;
        EggControl.jumpForce = 13f;
        CoinControl.coinFrequency = 5;
        Time.timeScale = 1f;
        GoodBreathCount.goodBreathCount = 0;
        gamePaused = false;
    }

	public void restartGame()
    {
        //resetState();
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
        GameOverMenu.gameIsOver = false;
    }

    public void backToMainMenu()
    {
        //resetState();
        SceneManager.LoadScene("MainMenu");
        GameOverMenu.gameIsOver = false;
    }

    public void pauseGame()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0f;
            GoodBreathText.alpha0();
            BadBreathText.alpha0();
        } else
        {
            Time.timeScale = 1f;
        }

        GameObject.FindWithTag("Player").GetComponent<EggControl>().enabled = gamePaused;
        gamePaused = !gamePaused;
    }
}
