using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public Text setsTxt;
    public Text breathsTxt;

    public void changeScene()
    {
        FizSession.sets = int.Parse(setsTxt.text);
        FizSession.breaths = int.Parse(breathsTxt.text);
        SceneManager.LoadScene("Game");
    }
}
