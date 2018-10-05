using TMPro;
using UnityEngine;
using Fizzyo;

public class NameRegister : MonoBehaviour
{

    public TextMeshProUGUI orText;
    public TextMeshProUGUI lastChanceText;
    public GameObject GOPanel;
    private TMP_InputField name;
    private bool focused;
    private bool lastChanceTaken = false;
    FizzyoDevice fd;

    void Start()
    {
        name = gameObject.GetComponent<TMP_InputField>();
        name.characterLimit = 20;

        fd = new FizzyoDevice();

        name.text = PlayerPrefs.GetString("playerName", "Anonymous");
    }

    // Update is called once per frame
    void Update()
    {
        if (focused)
        {
            // Either press enter or the device button to submit
            if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || fd.ButtonDown()) && !Input.GetMouseButtonDown(0) && name.text.Trim() != "")
            {
                OnSubmit();
            }
        }
        else
        {
            name.ActivateInputField();
            name.text = name.text.Trim();
        }

        // Check if last chance has been used
        if (!lastChanceTaken)
        {
            // Ensure that breath is the best quality
            if (BreathRecogniser.topQualityBreath)
            {
                BadBreathText.alpha0();
                HealthControl.lives += 1;
                Transform Egg = GameObject.Find("Egg").transform;
                Rigidbody2D rb2d = Egg.GetComponent<Rigidbody2D>();
                rb2d.constraints = RigidbodyConstraints2D.None;
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
                HealthControl.lifeSpriteArr[0].SetActive(true);

                Egg.position = new Vector3(OnCollision.lastCollidedPlatform.position.x, OnCollision.lastCollidedPlatform.position.y + 1.5f, OnCollision.lastCollidedPlatform.position.z);
                OnCollision.lastCollidedPlatform.gameObject.GetComponent<EdgeCollider2D>().enabled = true;
                Egg.parent = OnCollision.lastCollidedPlatform.gameObject.transform;

                GameOverMenu.gameIsOver = false;

                lastChanceTaken = true;
                orText.enabled = false;
                lastChanceText.enabled = false;
            }
        }
    }

    void LateUpdate()
    {
        focused = name.isFocused;
    }

    void OnSubmit()
    {
        Debug.Log("Name: " + name.text);
        PlayerPrefs.SetString("playerName", name.text);
        LeaderboardControl.addToScorePlayerLBDict((int)ScoreControl.currentScore, name.text);
        GOPanel.SetActive(false);
        //gameObject.SetActive(false);
    }
}
