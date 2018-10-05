using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EggSelectionMenuGen : MonoBehaviour {

    private GameObject Buy;
    private GameObject Select;
    private GameObject Selected;
    public static int eggShopSelectedNum;
    public static String price;

    public void OnClickEgg()
    {
        // Generate egg selection panel
        Buy = GameObject.Find("Buy");
        Select = GameObject.Find("Select");
        Selected = GameObject.Find("Selected");
        
        String name = gameObject.name;
        eggShopSelectedNum = Int32.Parse(Regex.Match(name, @"\d+").Value);
        MatchCollection matches = Regex.Matches(name, "[A-Z]([a-z]+)");
        
        GameObject.Find("Egg").GetComponent<Image>().sprite = EggLoadControl.eggSprites[eggShopSelectedNum];

        String nameText = "";
        for (int i = 0; i < matches.Count-1; i++)
        {
            nameText += matches[i] + " ";
        }
        GameObject.Find("EggNameText").GetComponent<TextMeshProUGUI>().text = nameText;

        price = "NIL";
        String description = "";
        switch (eggShopSelectedNum)
        {
            case 1:
                price = "100";
                break;
            case 2:
                price = "100";
                break;
            case 3:
                price = "150";
                break;
            case 4:
                price = "275";
                description = "Bonus: Adds an extra life!";
                break;
            case 5:
                price = "400";
                description = "Bonus: Tosses higher!";
                break;
            case 6:
                description = "Unlocked after achieving Master Tosser!";
                break;
            case 7:
                description = "Unlocked after achieving 1000 Jumper!";
                break;
            case 8:
                description = "Unlocked after achieving Money Saver!\nBonus: Double the chance of seeing a gold coin!";
                break;
            default:
                break;

        }
        GameObject.Find("PriceText").GetComponent<TextMeshProUGUI>().text = price;
        GameObject.Find("Description").GetComponent<TextMeshProUGUI>().text = description;

        // Determine what option (buy/select/selected) to display
        if (price.Equals("NIL"))
        {
            GameObject.Find("EggPrice").SetActive(false);
            Buy.SetActive(false);
        }

        if (EggLoadControl.eggsAvailable.Contains(eggShopSelectedNum))
        {
            Buy.SetActive(false);
        }
        
        if (eggShopSelectedNum != EggLoadControl.selectedEgg || !EggLoadControl.eggsAvailable.Contains(eggShopSelectedNum))
        {
            Selected.SetActive(false);
        }

        if (eggShopSelectedNum == EggLoadControl.selectedEgg || !EggLoadControl.eggsAvailable.Contains(eggShopSelectedNum))
        {
            Select.SetActive(false);
        }

        if (Buy.activeSelf == true && Int32.Parse(price) > CoinControl.currentCoinCount)
        {
            Buy.GetComponent<Button>().enabled = false;
            Buy.GetComponent<TextMeshProUGUI>().color = new Color(200f / 255f, 216f / 255f, 197f / 255f);
        } else
        {
            Buy.GetComponent<Button>().enabled = true;
            Buy.GetComponent<TextMeshProUGUI>().color = new Color(63f / 255f, 250f / 255f, 20f / 255f);
        }

    }
}
