using System.Collections;
using UnityEngine;

public class Frogger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite leapSprite;
    public Sprite deathSprite;
    public AudioSource src;
    public AudioClip jumpSound, hitSound;
    private Vector3 spawnPosition; // Store the spawn position of the frog
    private float farthestRow;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
    }

    [System.Obsolete]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Move(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            Move(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            Move(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            Move(Vector3.right);
        }
    }

    [System.Obsolete]
    private void Move(Vector3 direction) // Move the frog in the specified direction
    {
        Vector3 destination = transform.position + direction;

        Collider2D barrier = Physics2D.OverlapBox(
            destination,
            Vector2.zero,
            0f,
            LayerMask.GetMask("Barrier")
        );
        Collider2D platform = Physics2D.OverlapBox(
            destination,
            Vector2.zero,
            0f,
            LayerMask.GetMask("Platform")
        );
        Collider2D obstacle = Physics2D.OverlapBox(
            destination,
            Vector2.zero,
            0f,
            LayerMask.GetMask("Obstacle")
        );

        if (barrier != null) // Check for barriers in the direction of movement
        {
            return;
        }

        if (platform != null)
        {
            transform.SetParent(platform.transform);
        }
        else
        {
            transform.SetParent(null);
        }

        if (obstacle != null && platform == null) // Check for obstacles in the direction of movement
        {
            transform.position = destination;
            Death();
        }
        else
        {
            if (destination.y > farthestRow) // Check if the frog has moved to a new row
            {
                farthestRow = destination.y;
                FindObjectOfType<GameManager>().AdvancedRow(); // Notify the GameManager that the frog has advanced a row
            }
            StartCoroutine(Leap(destination));
        }
    }

    private IEnumerator Leap(Vector3 destination) // Animate the frog's leap to the destination
    {
        Vector3 startPosition = transform.position;
        float elapsed = 0f;
        float duration = 0.125f;

        spriteRenderer.sprite = leapSprite;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
        spriteRenderer.sprite = idleSprite;
        src.PlayOneShot(jumpSound); // Play the jump sound
    }

    [System.Obsolete]
    public void Death() // Handle the frog's death
    {
        StopAllCoroutines(); // Stop any ongoing leap animations

        src.PlayOneShot(hitSound); // Play the hit sound
        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = deathSprite;
        enabled = false; // Disable the script to stop movement

        FindObjectOfType<GameManager>().Died(); // Notify the GameManager that the frog has died
    }

    public void Respawn()
    {
        StopAllCoroutines(); // Stop any ongoing leap animations

        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
        spriteRenderer.sprite = idleSprite;
        farthestRow = spawnPosition.y;
        gameObject.SetActive(true);
        enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (
            enabled
            && other.gameObject.layer == LayerMask.NameToLayer("Obstacle")
            && transform.parent == null
        )
        {
            Death(); // Handle collision with obstacles
        }
    }
}
