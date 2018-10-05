using UnityEngine;
using UnityEngine.SceneManagement;
using Fizzyo;

public class BreathRecogniser : MonoBehaviour
{

    public static bool topQualityBreath = false;
    private static bool created = false;
    private bool unwantedBreathInput = false;
    private bool firstBreath = false;
    private int badBreath;

    void Awake()
    {
        // Only create one of it, otherwise there will be a lot of breath analyzers
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
        FizzyoFramework.Instance.Recogniser.BreathStarted += OnBreathStarted;
        FizzyoFramework.Instance.Recogniser.BreathComplete += OnBreathEnded;
    }

    void OnBreathStarted(object sender)
    {
        Debug.Log("Current start breath scene: " + SceneManager.GetActiveScene().name);
        // Because Z and D are also default inputs for breath, it has been removed, otherwise the typing of names would be a problem
        // It won't affect the actual device
        if (!(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.D)) && (!GameOverMenu.gameIsOver || GameObject.Find("NameInput") != null))
        {
            Debug.Log("Breath started");
            if (SceneManager.GetActiveScene().name == "Game" && !firstBreath)
            {
                SoundControl.playWindSound();
            }
            unwantedBreathInput = false;
        }
        else
        {
            unwantedBreathInput = true;
        }
    }

    void OnBreathEnded(object sender, ExhalationCompleteEventArgs e)
    {
        Debug.Log("Current end breath scene: " + SceneManager.GetActiveScene().name);

        if (unwantedBreathInput && SceneManager.GetActiveScene().name == "Game" && GoodBreathCount.goodBreathCount == -1)
        {
            GoodBreathCount.goodBreathCount = 0;
        }

        if (!unwantedBreathInput && SceneManager.GetActiveScene().name == "Game")
        {
            // Counter breath analyser and calibration problem
            if (GoodBreathCount.goodBreathCount == -1 || firstBreath)
            {
                GoodBreathCount.goodBreathCount = 0;
                if (firstBreath)
                {
                    firstBreath = false;
                }
                else
                {
                    firstBreath = true;
                }
            }
            else
            {
                Debug.Log("Breath ended");
                int bq;
                if (BadBreathText.badBreath)
                {
                    // Simulate bad breath since the Fizzyo Breath Analyser is faulty as the breath is always at 4 (best quality)
                    bq = 2;
                }
                else
                {
                    bq = e.BreathQuality;
                }

                Debug.Log("Breath quality: " + bq);
                SoundControl.stopWindSound();

                if (!MenuButtonControl.gamePaused)
                {
                    if (bq <= 2) // Low breath quality
                    {
                        BadBreathText.badBreath = false;
                        BadBreathText.badBreathCloud = true;
                        badBreath++;
                        Debug.Log("Bad Breath Count: " + badBreath);
                        foreach (GameObject dc in GameObject.FindGameObjectsWithTag("DarkCloud"))
                        {
                            Color color = dc.GetComponent<Renderer>().material.color;
                            color.a = 1f;
                            dc.GetComponent<Renderer>().material.color = color;
                        }
                    }

                    if (bq >= 3) // Good breath quality
                    {
                        Debug.Log(GameObject.Find("NameInput"));
                        if (GameObject.Find("NameInput") == null)
                        {
                            GoodBreathText.goodBreath = true;
                            GoodBreathCount.goodBreathCount++;
                        }
                        foreach (GameObject dc in GameObject.FindGameObjectsWithTag("DarkCloud"))
                        {
                            Destroy(dc);
                        }
                        if (bq == 4)
                        {
                            topQualityBreath = true;
                        }
                    }
                }
            }
        }
    }

    void LateUpdate()
    {
        topQualityBreath = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Fading of the dark clouds
        if (FizzyoFramework.Instance.Device.Pressure() > 0 && !unwantedBreathInput)
        {
            foreach (GameObject dc in GameObject.FindGameObjectsWithTag("DarkCloud"))
            {
                Color color = dc.GetComponent<Renderer>().material.color;
                color.a -= 0.01f;
                dc.GetComponent<Renderer>().material.color = color;
            }
        }
    }
}
