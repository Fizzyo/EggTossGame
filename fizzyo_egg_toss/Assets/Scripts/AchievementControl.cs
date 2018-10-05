using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class AchievementControl : MonoBehaviour
{

    public static Sprite[] achievementSpritesReduced;
    public static Sprite[] achievementSpritesOrig;
    public static HashSet<int> achievementsUnlocked = new HashSet<int>();
    public static int consectJump;
    public static int totalJump;
    public static int bestScore;
    public int prevAchCount = 0;
    public GameObject scoreText;
    private String sceneName;
    private String pprefAch;
    private int prevTotalJump;

    // Use this for initialization
    void Awake()
    {
        // Load achievement sprites
        achievementSpritesReduced = Resources.LoadAll<Sprite>("Sprites/Achievements/Reduced");
        achievementSpritesOrig = Resources.LoadAll<Sprite>("Sprites/Achievements/Original");

        // Achievement related values
        consectJump = 0;
        totalJump = PlayerPrefs.GetInt("totalJump", 0); // 0 is the default value
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        pprefAch = PlayerPrefs.GetString("pprefAch", "");

        prevTotalJump = totalJump;

        // If there are pre-existing achievements, load them
        if (pprefAch != "")
        {
            foreach (char c in pprefAch.ToCharArray())
            {
                achievementsUnlocked.Add(Int32.Parse(c.ToString()));
            }
        }

    }

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        // Load up best score on main menu scene
        if (sceneName.Equals("MainMenu"))
        {
            TextMeshProUGUI st = scoreText.GetComponent<TextMeshProUGUI>();
            st.text = bestScore.ToString();
        }
    }

    public void OnClick()
    {
        // Load up the achievement panel with the appropriate sprites
        ArrayList al = new ArrayList();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Achievement"))
        {
            al.Add(go.name);
        }
        al.Sort();

        for (int i = 0; i < al.Count; i++)
        {
            if (!achievementsUnlocked.Contains(i))
            {
                GameObject.Find((String)al[i]).GetComponent<Image>().sprite = achievementSpritesReduced[9];
            }
            else if (GameObject.Find((String)al[i]).GetComponent<Image>().sprite == achievementSpritesReduced[9])
            {
                GameObject.Find((String)al[i]).GetComponent<Image>().sprite = achievementSpritesReduced[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Constant achievement check
        if (sceneName.Equals("Game"))
        {
            if (consectJump >= 50)
            {
                achievementsUnlocked.Add(2);
            }
            else if (consectJump >= 20)
            {
                achievementsUnlocked.Add(1);
            }
            else if (consectJump >= 10)
            {
                achievementsUnlocked.Add(0);
            }

            if (CoinControl.currentCoinCount >= 1000)
            {
                achievementsUnlocked.Add(4);
            }

            if (totalJump >= 1000)
            {
                achievementsUnlocked.Add(7);
            }
            else if (totalJump >= 500)
            {
                achievementsUnlocked.Add(6);
            }
            else if (totalJump >= 100)
            {
                achievementsUnlocked.Add(5);
            }

            if (EggLoadControl.eggSprites != null && GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sprite == EggLoadControl.eggSprites[8])
            {
                achievementsUnlocked.Add(8);
            }

            if (ScoreControl.currentScore > bestScore)
            {
                bestScore = ScoreControl.currentScore;
                PlayerPrefs.SetInt("bestScore", bestScore);
            }

            if (totalJump != prevTotalJump)
            {
                PlayerPrefs.SetInt("totalJump", totalJump);
                prevTotalJump = totalJump;
            }

        }
        else if (sceneName.Equals("MainMenu"))
        {
            if (EggLoadControl.eggsAvailable.Count == 5)
            {
                achievementsUnlocked.Add(3);
            }
        }

        if (achievementsUnlocked.Count != prevAchCount)
        {
            pprefAch = "";
            foreach (int achID in achievementsUnlocked.ToArray())
            {
                pprefAch = achID + pprefAch;
            }
            Debug.Log("Preload achievements: " + pprefAch);
            PlayerPrefs.SetString("pprefAch", pprefAch);
        }

        prevAchCount = achievementsUnlocked.Count;

    }
}
