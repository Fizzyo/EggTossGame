using TMPro;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{

    public static int currentScore;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI gameOverScoreText;

    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Constantly update score text
        
        scoreText.text = currentScore.ToString();

        if (GameOverMenu.gameIsOver && GameObject.Find("GameOverScoreText") != null)
        {
            gameOverScoreText = GameObject.Find("GameOverScoreText").GetComponent<TextMeshProUGUI>();
            gameOverScoreText.text = currentScore.ToString();
        }
    }

}
