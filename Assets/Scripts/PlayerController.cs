using UnityEngine; // Required for Mathf and other Unity classes

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    public Transform movePoint; // The point to move towards
    public LayerMask whatStopsMovement; // Layer mask to define what stops the player's movement
    public Animator animator; // Animator component for player animations
    public bool onLog; // Flag to check if the player is on a log
    private Transform movingLog; // Reference to the moving log object
    private bool isTryingToMove = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movePoint.parent = null; // Detach the movePoint from the player
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
        {
            isTryingToMove = false; // Reset the trying to move flag when the player reaches the move point

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                TryMove(new Vector3(1f, 0f, 0f));
                transform.rotation = Quaternion.Euler(0f, 0f, -90f); // Face right
                isTryingToMove = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                TryMove(new Vector3(-1f, 0f, 0f));
                transform.rotation = Quaternion.Euler(0f, 0f, 90f); // Face right
                isTryingToMove = true;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                TryMove(new Vector3(0f, 1f, 0f));
                transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Face right
                isTryingToMove = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                TryMove(new Vector3(0f, -1f, 0f));
                transform.rotation = Quaternion.Euler(0f, 0f, 180f); // Face right
                isTryingToMove = true;
            }
        }
    }

    private void FixedUpdate()
{
    if (onLog && movingLog != null && !isTryingToMove)
    {
        // Move the movePoint with the log
        Vector3 logDelta = movingLog.GetComponent<Rigidbody2D>().linearVelocity * Time.fixedDeltaTime;
        movePoint.position += logDelta;
    }
}

    void TryMove(Vector3 direction)
    {
        if (!Physics2D.OverlapCircle(movePoint.position + direction, 0.2f, whatStopsMovement))
        {
            movePoint.position += direction;
            animator.SetTrigger("Hop"); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Log"))
        {
            movingLog = other.gameObject.transform; // Get the transform of the log object
            onLog = true; // Set the onLog flag to true
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Log"))
        {
            onLog = false; // Set the onLog flag to false
        }
    }

// This method is called when the player collides with another object
    private void OnCollisionEnter2D(Collision2D other) {
        // Check if the player collides with a car and the player is not dead
        if (other.gameObject.CompareTag("Car") && !GameManager.PLAYERDEAD) // Check if the player collides with a car and the player is not dead
        {
            GameManager.PLAYERDEAD = true; // Set the player as dead
            Destroy(gameObject); // Destroy the player object
        }

        
    }
}
