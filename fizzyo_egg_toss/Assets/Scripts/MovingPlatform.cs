using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public static float moveSpeedInit = 2f;
    private float moveSpeed = 2f;
    private float positiveMoveSpeed;
    private float negativeMoveSpeed;

    // Use this for initialization
    void Start()
    {
        moveSpeed = moveSpeedInit;
        negativeMoveSpeed = -moveSpeed;
        positiveMoveSpeed = moveSpeed;

        // Some platforms will go left, some will go right
        if (Random.Range(0, 2) == 0)
        {
            moveSpeed = negativeMoveSpeed;
        }
        else
        {
            moveSpeed = positiveMoveSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Bounce back
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(10F, transform.position.y, transform.position.z), Time.deltaTime * moveSpeed);
        if (transform.position.x >= 4)
        {
            moveSpeed = negativeMoveSpeed;
        }
        else if (transform.position.x <= -4)
        {
            moveSpeed = positiveMoveSpeed;
        }
    }
}
