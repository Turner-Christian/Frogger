using UnityEngine;

public class CarController : MonoBehaviour
{
    public Sprite[] Sprite_Sheet; // Array of sprites for the car
    public float moveSpeed = 2f; // Speed of the car movement
    private int rand;
    private bool moveLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rand = Random.Range(0, Sprite_Sheet.Length); // Get a random index for the sprite
        GetComponent<SpriteRenderer>().sprite = Sprite_Sheet[rand]; // Set the sprite of the car spawner
        if(transform.position.x > 0) // Check if the car is on the right side of the screen
        {
            moveLeft = true; // Set the car to move left
        }
        else if(transform.position.x < 0) // Check if the car is on the left side of the screen
        {
            moveLeft = false; // Set the car to move right
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if(moveLeft)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime; // Move the car to the left at the specified speed
            if (transform.position.x < -10f) // Check if the car has moved off-screen
            {
                Destroy(gameObject); // Destroy the car object
            }
        } else {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f); // Rotate the car to face the opposite direction
            transform.position += Vector3.right * moveSpeed * Time.deltaTime; // Move the car to the right at the specified speed
            if (transform.position.x > 10f) // Check if the car has moved off-screen
            {
                Destroy(gameObject); // Destroy the car object
            }
        }
    }
}
