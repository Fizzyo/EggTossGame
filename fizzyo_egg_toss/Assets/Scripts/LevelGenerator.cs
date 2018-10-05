using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    private static GameObject platformPrefab;
    private static GameObject coinPrefab;
    private static int numberOfPlatforms = 2;
    private static float levelWidth = 4;
    public static GameObject coingen;
    public static GameObject[] Platforms = new GameObject[4];//Keep twice the number of platforms
    public static int current_platform = 0;
    public static float minY;
    public static float maxY;
    public static float spawnPosY = -3.8f;

    public static void LevelGenerate()
    {
        platformPrefab = GameObject.FindWithTag("Ground");
        coinPrefab = GameObject.FindWithTag("Coin");

        platformPrefab.GetComponent<EdgeCollider2D>().enabled = true;

        // Increase in speed per generation
        MovingPlatform.moveSpeedInit += 0.05f;

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            bool staticPlatform = false;

            // Determine which set of platforms to destroy
            if (((current_platform == 3) && (Platforms[0] != null)) || (Platforms[current_platform] != null))
            {
                if ((current_platform == 3))
                {
                    Destroy(Platforms[0]);
                    Destroy(Platforms[1]);
                }
                else if ((current_platform == 1) && (Platforms[3] != null))
                {
                    Destroy(Platforms[2]);
                    Destroy(Platforms[3]);
                }
                current_platform += 1;
                if (current_platform > 3)
                {
                    current_platform = 0;
                }
            }

            spawnPosY += 3.2f;
            Vector3 spawnPosition = new Vector3 { y = spawnPosY };

            // 1 in 5 chance of getting a static platform
            if (Random.Range(0, 5) == 0 && (Platforms[3] != null))
            {
                int prevPlatformIndex;
                if (current_platform != 0)
                {
                    prevPlatformIndex = current_platform - 1;
                }
                else
                {
                    prevPlatformIndex = 3;
                }

                // In the event there is consecutive static platforms
                spawnPosition.x = Platforms[prevPlatformIndex].transform.position.x;
                staticPlatform = true;
            }
            else
            {
                spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            }

            Platforms[current_platform] = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

            // Where the coins are generated.
            Random.InitState((int)Random.Range(0, 100));
            int randomrange = Random.Range(0, CoinControl.baseFrequency);
            Vector3 coinPos = new Vector3 { y = 0.5f, z = -0.1f };

            // Change the integer in the if statement to change the probabilty.
            if (randomrange >= CoinControl.baseFrequency - CoinControl.coinFrequency)
            {
                coingen = Instantiate(coinPrefab, spawnPosition + coinPos, Quaternion.identity);
                coingen.transform.parent = Platforms[current_platform].transform;
            }

            // Determine if it will be a static platform
            if (staticPlatform)
            {
                Platforms[current_platform].GetComponent<MovingPlatform>().enabled = false;
            }
            else
            {
                Platforms[current_platform].GetComponent<MovingPlatform>().enabled = true;
            }
        }
        if (Platforms[0] != null && Platforms[2] != null)
        {
            platformPrefab.GetComponent<EdgeCollider2D>().enabled = false;
        }
    }
}
