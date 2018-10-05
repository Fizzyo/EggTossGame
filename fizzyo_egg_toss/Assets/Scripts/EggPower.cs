using UnityEngine;

public class EggPower : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        // Certain eggs have certain powers
        if (EggLoadControl.selectedEgg == 4)
        {
            HealthControl.lives += 1;
        }

        if (EggLoadControl.selectedEgg == 5)
        {
            EggControl.jumpForce = 18f; //else 13f
        }

        if (EggLoadControl.selectedEgg == 8)
        {
            CoinControl.coinFrequency = 10;
        }
    }
}
