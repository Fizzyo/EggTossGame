using UnityEngine;

public class RandomMovement : MonoBehaviour {

    private const float SPEED = 2f;
    private Vector3 direction;
    private Transform mCamera;

    void Start()
    {
        mCamera = GameObject.FindWithTag("MainCamera").transform;
        direction = (new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f))).normalized;
    }

    void Update()
    {
        // Dark clouds random movement within the camera view
        transform.position += direction * SPEED * Time.deltaTime;
        float randY = 1f;
        float randX = 1f;

        if (transform.position.y >= mCamera.position.y + 4.5f || transform.position.y <= mCamera.position.y - 4.5f || transform.position.x <= -5f || transform.position.x >= 5f)
        {
            if (transform.position.x >= 0)
            {
                randX = -1f;
            }

            if (transform.position.y >= mCamera.position.y)
            {
                randY = -1f;
            }
            direction = (new Vector2(Random.Range(0f, randX), Random.Range(0f, randY))).normalized;
        }
    }
}
