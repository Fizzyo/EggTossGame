using UnityEngine;

public class OnCollision : MonoBehaviour
{
    private Transform mCamera;
    private Transform collideObject;
    private Vector2 pointOfContact;
    private float smoothSpeed = .05f;
    private bool enterTop = false;
    private bool genPlatform = false;
    public static Transform lastCollidedPlatform;
    public GameObject BackgroundCloud;

    // Use this for initialization
    void Start()
    {
        BackgroundCloud = GameObject.Find("BackgroundCloud");
        mCamera = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Platform generation
        if (genPlatform)
        {
            LevelGenerator.LevelGenerate();
            genPlatform = false;
        }

        // Camera movement
        if (collideObject != null)
        {
            float newPosY = 4f + collideObject.position.y;

            if ((newPosY - mCamera.position.y) > 0.0001)
            {
                Vector3 newPos = new Vector3(mCamera.position.x, newPosY, mCamera.position.z);
                mCamera.position = Vector3.Lerp(mCamera.position, newPos, smoothSpeed);
            }
            else
            {
                collideObject = null;
            }

            if ((newPosY - mCamera.position.y) < 0.5)
            {
                GameObject.FindWithTag("Player").GetComponent<EggControl>().enabled = true;
            }

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pointOfContact = collision.contacts[0].normal;
            
            if (collision.gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
            {
                if (pointOfContact == new Vector2(0, -1))
                {
                    if (lastCollidedPlatform != transform && lastCollidedPlatform != null)
                    {
                        // If you hit a skip a platform and collide with the one on top, disable the collider for the skipped platform
                        // Logic for the wing egg that can skip a platform
                        if (LevelGenerator.Platforms[1] == gameObject && LevelGenerator.Platforms[0].GetComponent<EdgeCollider2D>().enabled == true)
                        {
                            LevelGenerator.Platforms[0].GetComponent<EdgeCollider2D>().enabled = false;
                            ScoreControl.currentScore += 2;

                        }
                        else if (LevelGenerator.Platforms[3] == gameObject && LevelGenerator.Platforms[2].GetComponent<EdgeCollider2D>().enabled == true)
                        {
                            LevelGenerator.Platforms[2].GetComponent<EdgeCollider2D>().enabled = false;
                            ScoreControl.currentScore += 2;
                        }
                        else
                        {
                            ScoreControl.currentScore += 1;
                        }
                        
                        AchievementControl.consectJump += 1;
                        AchievementControl.totalJump += 1;

                        Debug.Log("Consect: " + AchievementControl.consectJump + ", Total: " + AchievementControl.totalJump);
                    }
                    lastCollidedPlatform = transform;
                    enterTop = true;

                    if (transform.position.y > mCamera.position.y)
                    {
                        collideObject = transform;
                        GameObject.FindWithTag("Player").GetComponent<EggControl>().enabled = false;
                        genPlatform = true;
                    }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (enterTop)
        {
            transform.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            enterTop = false;
        }
    }
}
