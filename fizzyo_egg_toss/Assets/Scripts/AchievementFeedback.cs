using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementFeedback : MonoBehaviour {

    private bool fadeIn;
    private bool enteredDelay;
    private bool playedSound;
    private HashSet<int> prevAchievementUnlocked;
    private Queue<ArrayList> achievementQueue = new Queue<ArrayList>();

    // Use this for initialization
    void Start () {
        prevAchievementUnlocked = new HashSet<int>(AchievementControl.achievementsUnlocked);
        fadeIn = false;
        enteredDelay = false;
        playedSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if there are new achievements unlocked
        if (!prevAchievementUnlocked.SetEquals(AchievementControl.achievementsUnlocked))
        {
            foreach (int i in AchievementControl.achievementsUnlocked.Except(prevAchievementUnlocked)) {
                achievementDetails(i);
            }
            prevAchievementUnlocked = new HashSet<int>(AchievementControl.achievementsUnlocked);
        }

    }

    void LateUpdate()
    {   
        // Achievement panel fade in and out
        if (achievementQueue.Count != 0 && !fadeIn && gameObject.GetComponent<CanvasGroup>().alpha == 0)
        {
            ArrayList al = achievementQueue.Dequeue();
            GameObject.Find("AchievementPanelIcon").GetComponent<Image>().sprite = (Sprite)al[0];
            GameObject.Find("AchievementPanelTitle").GetComponent<TextMeshProUGUI>().text = al[1].ToString();
            fadeIn = true;
            playedSound = false;
        } else { 
            if (!enteredDelay) { 
                if (fadeIn)
                {
                    if (!playedSound)
                    {
                        SoundControl.playAchievementSound();
                        playedSound = true;
                    }
                    gameObject.GetComponent<CanvasGroup>().alpha += 0.025f;
                } else
                {
                    gameObject.GetComponent<CanvasGroup>().alpha -= 0.025f;
                }

                if (gameObject.GetComponent<CanvasGroup>().alpha == 1)
                {
                    StartCoroutine(delay(3));
                }
            }
        }
    }
    
    IEnumerator delay(float delay)
    {
        // Delay for achievement panel
        enteredDelay = true;
        yield return new WaitForSeconds(delay);
        enteredDelay = false;
        fadeIn = false;
    }
        
    void achievementDetails(int achNum)
    {
        string name = "";

        switch(achNum)
        {
            case 0:
                name = "Professional Jumper";
                break;
            case 1:
                name = "Master Jumper";
                break;
            case 2:
                name = "Grandmaster Jumper";
                break;
            case 3:
                name = "Collector Jumper";
                break;
            case 4:
                name = "Gold Saver Jumper";
                break;
            case 5:
                name = "100 Jumper";
                break;
            case 6:
                name = "500 Jumper";
                break;
            case 7:
                name = "1000 Jumper";
                break;
            case 8:
                name = "Faberge";
                break;
        }

        achievementQueue.Enqueue(new ArrayList {AchievementControl.achievementSpritesReduced[achNum], name});
    }
}
