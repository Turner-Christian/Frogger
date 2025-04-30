using UnityEngine; // Required for Mathf and other Unity classes

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    public Transform movePoint; // The point to move towards
    public LayerMask whatStopsMovement; // Layer mask to define what stops the player's movement
    public Animator animator; // Animator component for player animations

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
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                TryMove(new Vector3(1f, 0f, 0f));
                transform.rotation = Quaternion.Euler(0f, 0f, -90f); // Face right
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                TryMove(new Vector3(-1f, 0f, 0f));
                transform.rotation = Quaternion.Euler(0f, 0f, 90f); // Face right
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                TryMove(new Vector3(0f, 1f, 0f));
                transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Face right
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                TryMove(new Vector3(0f, -1f, 0f));
                transform.rotation = Quaternion.Euler(0f, 0f, 180f); // Face right
            }
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
}
