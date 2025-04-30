using UnityEngine;

public class LogController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the log movement
    private int rand;
    private bool moveLeft; // Flag to determine the direction of movement
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the log
        if(transform.position.x > 0) // Check if the log is on the right side of the screen
        {
            moveLeft = true; // Set the log to move left
        }
        else if(transform.position.x < 0) // Check if the log is on the left side of the screen
        {
            moveLeft = false; // Set the log to move right
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(moveLeft)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f); // Rotate the log to face the opposite direction
            rb.linearVelocity = Vector2.left * moveSpeed; // Move the log to the left at the specified speed
            if (transform.position.x < -20f) // Check if the log has moved off-screen
            {
                Destroy(gameObject); // Destroy the log object
            }
        } else {
            rb.linearVelocity = Vector2.right * moveSpeed; // Move the log to the left at the specified speed
            if (transform.position.x > 20f) // Check if the log has moved off-screen
            {
                Destroy(gameObject); // Destroy the log object
            }
        }
    }
}
