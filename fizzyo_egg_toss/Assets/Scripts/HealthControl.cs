using UnityEngine;

//[UnityEditor.InitializeOnLoad]
public class HealthControl : MonoBehaviour
{
    private Transform HUD;
    private Vector3 lifePos;
    public GameObject lifeSprite;
    public static int lives;
    public static GameObject[] lifeSpriteArr;

    void Start()
    {
        // Array of GameObject - egg life sprite
        HUD = GameObject.Find("HUD").transform;
        lifeSpriteArr = new GameObject[lives];
        lifePos = new Vector3(8.75f, 4.7f);

        for (int i = 0; i < lives; i++)
        {
            lifeSpriteArr[i] = Instantiate(lifeSprite, lifePos, Quaternion.identity);
            lifeSpriteArr[i].transform.parent = HUD;
            lifePos.x -= 0.55f;
        }
    }

    public static void deductLife()
    {
        lives--;
        lifeSpriteArr[lives].SetActive(false);
    }
}
