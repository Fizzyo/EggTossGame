using TMPro;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{

    public static int currentScore;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI gameOverScoreText;

    void Update()
    {
        // Constantly update score text
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreText.text = currentScore.ToString();

        if (GameOverMenu.gameIsOver && GameObject.Find("GameOverScoreText") != null)
        {
            gameOverScoreText = GameObject.Find("GameOverScoreText").GetComponent<TextMeshProUGUI>();
            gameOverScoreText.text = currentScore.ToString();
        }
    }

}
