using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EggLoadControl : MonoBehaviour
{
    public static Sprite[] eggSprites;
    public static HashSet<int> eggsAvailable = new HashSet<int>();
    public static int selectedEgg;
    public GameObject Select;
    public GameObject Selected;
    private int pprefEggs;

    // Use this for initialization
    void Awake()
    {
        // Load all egg sprites and player egg data
        eggSprites = Resources.LoadAll<Sprite>("Sprites/Eggs");
        eggsAvailable.Add(0);

        pprefEggs = PlayerPrefs.GetInt("pprefEggs", 0);
        selectedEgg = PlayerPrefs.GetInt("pprefSelectedEgg", 0);

        int eggIDs = pprefEggs;
        while (eggIDs % 10 > 0)
        {
            eggsAvailable.Add(eggIDs % 10);
            eggIDs = eggIDs / 10;
        }
    }

    public void BuyEgg()
    {
        // Buy egg from egg store
        SoundControl.playBuySound();

        eggsAvailable.Add(EggSelectionMenuGen.eggShopSelectedNum);
        gameObject.GetComponent<TextMeshProUGUI>().fontSize = 60;

        CoinControl.currentCoinCount -= Int32.Parse(EggSelectionMenuGen.price);

        pprefEggs = pprefEggs * 10 + EggSelectionMenuGen.eggShopSelectedNum;
        PlayerPrefs.SetInt("pprefEggs", pprefEggs);

        gameObject.SetActive(false);
        Select.SetActive(true);
    }

    public void SelectEgg()
    {
        // Select egg from egg store
        selectedEgg = EggSelectionMenuGen.eggShopSelectedNum;
        PlayerPrefs.SetInt("pprefSelectedEgg", selectedEgg);
        gameObject.GetComponent<TextMeshProUGUI>().fontSize = 60;
        gameObject.SetActive(false);
        Selected.SetActive(true);
    }

    public static Sprite returnSelectedEggSprite()
    {
        return eggSprites[selectedEgg];
    }

    void Update()
    {
        // Unlock eggs when achievements unlocked
        if (AchievementControl.achievementsUnlocked.Contains(1))
        {
            eggsAvailable.Add(6);
        }
        else if (AchievementControl.achievementsUnlocked.Contains(4))
        {
            eggsAvailable.Add(8);
        }
        else if (AchievementControl.achievementsUnlocked.Contains(7))
        {
            eggsAvailable.Add(7);
        }
    }
}
