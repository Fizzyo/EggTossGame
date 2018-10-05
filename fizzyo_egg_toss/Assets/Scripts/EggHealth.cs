using UnityEngine;

public class EggHealth : MonoBehaviour
{
    private Transform mCamera;
    public int lives;

    // Use this for initialization
    private void Awake()
    {
        mCamera = GameObject.FindWithTag("MainCamera").transform;
        HealthControl.lives = lives;
    }

    // Update is called once per frame
    void Update()
    {
        // When egg drops - If drop below certain distance
        if (transform.position.y < (mCamera.position.y - 15f))
        {
            if (HealthControl.lives != 0)
            {
                HealthControl.deductLife();
                AchievementControl.consectJump = 0;

                if (HealthControl.lives > 0)
                {
                    SoundControl.playReappearSound();
                    transform.position = new Vector3(OnCollision.lastCollidedPlatform.position.x, OnCollision.lastCollidedPlatform.position.y + 1.5f, OnCollision.lastCollidedPlatform.position.z);
                    OnCollision.lastCollidedPlatform.gameObject.GetComponent<EdgeCollider2D>().enabled = true;
                    transform.parent = OnCollision.lastCollidedPlatform.gameObject.transform;
                }
            }
        }
    }

    void FixedUpdate()
    {
        // When player loses all lives
        if (HealthControl.lives == 0 && (transform.position.y < (mCamera.position.y - 50f)))
        {
            transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GameOverMenu.gameIsOver = true;
        }
    }
}
