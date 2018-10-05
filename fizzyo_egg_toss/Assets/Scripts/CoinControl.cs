using TMPro;
using UnityEngine;

public class CoinControl : MonoBehaviour
{
    private TextMeshProUGUI coinText;
    public static int coinFrequency = 5;
    public static int baseFrequency;
    public static int currentCoinCount;

    // Use this for initialization
    void Start()
    {
        currentCoinCount = PlayerPrefs.GetInt("coinCount", 0); // 0 is the default value
        baseFrequency = 20;
    }

    // Update is called once per frame
    void Update()
    {
        // Update coin details
        coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
        coinText.text = CoinControl.currentCoinCount.ToString();
        PlayerPrefs.SetInt("coinCount", CoinControl.currentCoinCount);
    }
}
