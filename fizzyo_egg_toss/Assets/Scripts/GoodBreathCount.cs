using TMPro;
using UnityEngine;

public class GoodBreathCount : MonoBehaviour
{

    public static int goodBreathCount = -1;

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = goodBreathCount.ToString();
    }
}
