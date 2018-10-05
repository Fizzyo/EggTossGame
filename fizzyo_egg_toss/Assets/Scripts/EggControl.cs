using UnityEngine;
using Fizzyo;

public class EggControl : MonoBehaviour
{
    public static float jumpForce = 13f;
    private bool jump = false;
    private bool grounded = false;
    private Rigidbody2D rb2d;
    private Vector2 pointOfContact;
    FizzyoDevice fd;

    // Use this for initialization
    void Start()
    {
        LevelGenerator.LevelGenerate();
        rb2d = GetComponent<Rigidbody2D>();
        fd = new FizzyoDevice();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Since we have no button, we use up arrow key to simulate
        if ((fd.ButtonDown() || Input.GetKeyDown(KeyCode.UpArrow)) && grounded && !Input.GetMouseButtonDown(0))
        {
            jump = true;

            grounded = false;
            SoundControl.playJumpSound();
        }
    }

    void FixedUpdate()
    {
        if (jump)
        {
            Vector2 velocity = rb2d.velocity;
            velocity.y = jumpForce;
            rb2d.velocity = velocity;
            jump = false;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Grab the normal of the contact point we touched
            pointOfContact = collision.contacts[0].normal; 

            // If collide from the top
            if (pointOfContact == new Vector2(0, 1))
            {
                grounded = true;

                // Make platform the egg's parent so that they will move together
                transform.parent = collision.transform;

                // Makes the egg in the middle of the platform
                transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 0.625f, -0.1f); 
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // On exit, remove as egg's parent
        transform.parent = null;
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.6f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            SoundControl.playCoinSound();
            CoinControl.currentCoinCount += 1;
        }
    }
}
