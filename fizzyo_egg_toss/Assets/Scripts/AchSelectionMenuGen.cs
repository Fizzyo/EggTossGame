using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchSelectionMenuGen : MonoBehaviour
{

    private GameObject Locked;
    private int achSelectedNum;

    public void OnClickAch()
    {
        //  Generate details for the achievement page
        Locked = GameObject.Find("Locked");

        String name = gameObject.name;
        String[] nameArr = name.Split('_');
        achSelectedNum = Int32.Parse(nameArr[0]);

        // Check if the achievement has been unlocked to decide which sprite to use
        if (AchievementControl.achievementsUnlocked.Contains(achSelectedNum))
        {
            GameObject.Find("AchievementIcon").GetComponent<Image>().sprite = AchievementControl.achievementSpritesOrig[achSelectedNum];
            Locked.SetActive(false);
        }
        else
        {
            GameObject.Find("AchievementIcon").GetComponent<Image>().sprite = AchievementControl.achievementSpritesOrig[9];
        }

        // Get achievement names and descriptions
        String nameText = "";
        for (int i = 1; i < nameArr.Length - 1; i++)
        {
            nameText += nameArr[i] + " ";
        }
        GameObject.Find("AchievementNameText").GetComponent<TextMeshProUGUI>().text = nameText;

        String description;
        switch (achSelectedNum)
        {
            case 0:
                description = "Attained 10 consecutive successful tosses!";
                break;
            case 1:
                description = "Attained 20 consecutive successful tosses!";
                break;
            case 2:
                description = "Attained 50 consecutive successful tosses!\nRare feat!";
                break;
            case 3:
                description = "Collected 5 eggs! Go on and collect 'em all!";
                break;
            case 4:
                description = "Saved a total of 1000 coins!";
                break;
            case 5:
                description = "Attained a total of 100 successful tosses! ";
                break;
            case 6:
                description = "Attained a total of 500 successful tosses! ";
                break;
            case 7:
                description = "Attained a total of 1000 successful tosses! ";
                break;
            case 8:
                description = "Be a Royalty!";
                break;
            default:
                description = "";
                break;
        }
        GameObject.Find("Description").GetComponent<TextMeshProUGUI>().text = description;
    }

}
