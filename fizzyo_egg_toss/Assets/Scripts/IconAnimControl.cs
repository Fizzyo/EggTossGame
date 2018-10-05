using UnityEngine;

public class IconAnimControl : MonoBehaviour {

    public GameObject EggBackButton;
    public GameObject AchBackButton;
    public GameObject LeaderboardBackButton;
    public GameObject EggSelectBackButton;
    public GameObject AchSelectBackButton;
    public GameObject EggShopButton;
    public GameObject LeaderboardButton;
    public GameObject AchievementButton;

    public void revertButtonSize()
    {
        // Revert the button back to their original sizes because of panel enable and disable issue
        EggBackButton.GetComponent<Animator>().enabled = false;
        AchBackButton.GetComponent<Animator>().enabled = false;
        LeaderboardBackButton.GetComponent<Animator>().enabled = false;
        EggSelectBackButton.GetComponent<Animator>().enabled = false;
        AchSelectBackButton.GetComponent<Animator>().enabled = false;
        EggShopButton.GetComponent<Animator>().enabled = false;
        LeaderboardButton.GetComponent<Animator>().enabled = false;
        AchievementButton.GetComponent<Animator>().enabled = false;

        EggBackButton.GetComponent<RectTransform>().sizeDelta = new Vector2(60f, 60f);
        AchBackButton.GetComponent<RectTransform>().sizeDelta = new Vector2(60f, 60f);
        LeaderboardBackButton.GetComponent<RectTransform>().sizeDelta = new Vector2(60f, 60f);
        EggSelectBackButton.GetComponent<RectTransform>().sizeDelta = new Vector2(60f, 60f);
        AchSelectBackButton.GetComponent<RectTransform>().sizeDelta = new Vector2(60f, 60f);
        EggShopButton.GetComponent<RectTransform>().sizeDelta = new Vector2(150f, 150f);
        LeaderboardButton.GetComponent<RectTransform>().sizeDelta = new Vector2(150f, 150f);
        AchievementButton.GetComponent<RectTransform>().sizeDelta = new Vector2(150f, 150f);

        EggBackButton.GetComponent<Animator>().enabled = true;
        AchBackButton.GetComponent<Animator>().enabled = true;
        LeaderboardBackButton.GetComponent<Animator>().enabled = true;
        EggSelectBackButton.GetComponent<Animator>().enabled = true;
        AchSelectBackButton.GetComponent<Animator>().enabled = true;
        EggShopButton.GetComponent<Animator>().enabled = true;
        LeaderboardButton.GetComponent<Animator>().enabled = true;
        AchievementButton.GetComponent<Animator>().enabled = true;
    }
}
