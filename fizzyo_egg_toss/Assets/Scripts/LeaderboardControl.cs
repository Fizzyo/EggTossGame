using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class LeaderboardControl : MonoBehaviour
{
    public static SortedDictionary<string, int> scorePlayerLBDict = new SortedDictionary<string, int>();
    public TextMeshProUGUI leaderboardText;

    // Use this for initialization
    void Start()
    {
        // Retrieve previous leaderboard data
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.GetString("playerLB_" + i, "") != "" && !scorePlayerLBDict.Keys.Contains(PlayerPrefs.GetString("playerLB_" + i, "")))
            {
                scorePlayerLBDict.Add(PlayerPrefs.GetString("playerLB_" + i, ""), PlayerPrefs.GetInt("scoreLB_" + i, -1));
            }
        }
    }

    public void generateLeaderboard()
    {
        leaderboardText.GetComponent<TextMeshProUGUI>().text = "";
        foreach (KeyValuePair<string, int> item in scorePlayerLBDict.OrderBy(key => key.Value).Reverse())
        {
            leaderboardText.GetComponent<TextMeshProUGUI>().text += string.Format("{0,-22} {1,-5}\n", item.Key, item.Value);
            Debug.Log(item.Key + " " + item.Value);
        }
    }

    public static void addToScorePlayerLBDict(int score, string name)
    {
        // Update if the person is in the leaderboard already
        if (scorePlayerLBDict.Keys.Contains(name))
        {
            if (score > scorePlayerLBDict[name])
            {
                scorePlayerLBDict[name] = score;
            }
        }
        else
        {
            // If there are already 5 in the leaderboard, check if current score is greater than the min score
            if (scorePlayerLBDict.Count() == 5)
            {
                if (score > scorePlayerLBDict.Values.Min())
                {
                    scorePlayerLBDict.Add(name, score);
                    scorePlayerLBDict.Remove(scorePlayerLBDict.OrderBy(key => key.Value).ToArray()[0].Key);
                }
            }
            else
            {
                scorePlayerLBDict.Add(name, score);
            }

        }

        int count = 0;
        foreach (KeyValuePair<string, int> item in scorePlayerLBDict.OrderBy(key => key.Value).Reverse())
        {
            // Reload a new set
            PlayerPrefs.DeleteKey("scoreLB_" + count);
            PlayerPrefs.DeleteKey("playerLB_" + count);

            PlayerPrefs.SetString("playerLB_" + count, item.Key);
            PlayerPrefs.SetInt("scoreLB_" + count, item.Value);

            Debug.Log(PlayerPrefs.GetInt("scoreLB_" + count, -1) + " " + PlayerPrefs.GetString("playerLB_" + count, ""));
            count++;
        }
    }
}
