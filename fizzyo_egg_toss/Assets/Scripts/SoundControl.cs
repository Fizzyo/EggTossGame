using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundControl : MonoBehaviour
{

    public static AudioSource jumpSource;
    public static AudioSource coinSource;
    public static AudioSource reappearSource;
    public static AudioSource achievementSource;
    public static AudioSource buySource;
    public static AudioSource windSource;
    public static AudioSource gameOverSource;
    public static AudioSource tryAgainSource;
    public static AudioSource wellDoneSource;
    public static float audioVol;
    public static bool mute;

    public Button soundButton;
    public Sprite soundMuteIcon;
    public Sprite soundUnmuteIcon;

    private static bool created = false;

    private AudioSource musicSource;
    private bool prevMuteState;
    private string prevScene;

    void Awake()
    {
        // Only create one of it, otherwise there will be a lot of duplicated sounds
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        jumpSource = GetComponents<AudioSource>()[0];
        coinSource = GetComponents<AudioSource>()[1];
        reappearSource = GetComponents<AudioSource>()[2];
        achievementSource = GetComponents<AudioSource>()[3];
        buySource = GetComponents<AudioSource>()[4];
        musicSource = GetComponents<AudioSource>()[5];
        windSource = GetComponents<AudioSource>()[6];
        gameOverSource = GetComponents<AudioSource>()[7];
        tryAgainSource = GetComponents<AudioSource>()[8];
        wellDoneSource = GetComponents<AudioSource>()[9];

        prevMuteState = false;
        prevScene = SceneManager.GetActiveScene().name;

        audioVol = 1f;

        // Check previous setting for sound
        if (PlayerPrefs.GetInt("mute", 0) == 0)
        {
            mute = false;
        }
        else
        {
            mute = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("SoundButton") != null)
        {
            // Change icon
            if (mute)
            {
                GameObject.Find("SoundButton").GetComponent<Image>().sprite = soundMuteIcon;
                PlayerPrefs.SetInt("mute", 1);
            }
            else
            {
                GameObject.Find("SoundButton").GetComponent<Image>().sprite = soundUnmuteIcon;
                PlayerPrefs.SetInt("mute", 0);
            }

            // Mute all
            for (int i = 0; i < GetComponents<AudioSource>().Length; i++)
            {
                GetComponents<AudioSource>()[i].mute = mute;
            }

            prevMuteState = mute;
            prevScene = SceneManager.GetActiveScene().name;
        }
    }

    public static void playJumpSound()
    {
        jumpSource.PlayOneShot(jumpSource.clip, audioVol);
    }

    public static void playCoinSound()
    {
        coinSource.PlayOneShot(coinSource.clip, audioVol);
    }

    public static void playReappearSound()
    {
        reappearSource.PlayOneShot(reappearSource.clip, audioVol);
    }

    public static void playAchievementSound()
    {
        achievementSource.PlayOneShot(achievementSource.clip, audioVol);
    }

    public static void playBuySound()
    {
        buySource.PlayOneShot(buySource.clip, audioVol);
    }

    public static void playWindSound()
    {
        windSource.enabled = true;
        windSource.PlayOneShot(windSource.clip, audioVol);
    }

    public static void stopWindSound()
    {
        windSource.enabled = false;
    }

    public static void playGameOverSound()
    {
        gameOverSource.PlayOneShot(gameOverSource.clip, audioVol);
    }

    public static void playTryAgainSound()
    {
        tryAgainSource.PlayOneShot(tryAgainSource.clip, audioVol);
    }

    public static void playWellDoneSound()
    {
        wellDoneSource.PlayOneShot(wellDoneSource.clip, audioVol);
    }

    public void muteSound()
    {
        if (mute)
        {
            mute = false;
        }
        else
        {
            mute = true;
        }
    }
}
