using UnityEngine;

public class DarkCloudGenerator : MonoBehaviour
{
    public GameObject darkCloud;
    private Transform mCamera;
    private int currentTime;
    private int prevTime;
    private int goodBreath;

    void Start()
    {
        mCamera = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Generate 2 clouds every 10 seconds
        currentTime = (int)Time.timeSinceLevelLoad;
        if (!GameOverMenu.gameIsOver)
        {
            if (prevTime != currentTime && currentTime % 10 == 0 && currentTime > 10)
            {
                prevTime = currentTime;
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(darkCloud, new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(mCamera.position.y - 4f, mCamera.position.y + 4f), -1), Quaternion.identity).transform.parent = mCamera;
                }
            }
        }
    }
}