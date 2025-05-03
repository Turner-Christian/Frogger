using Unity.VisualScripting;
using UnityEngine;

public class Home : MonoBehaviour
{
    public GameObject frog;

    private void OnEnable()
    {
        frog.SetActive(true);
    }

    private void OnDisable()
    {
        frog.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enabled = true; // Enable this script
            FindObjectOfType<GameManager>().HomeOccupied(); // Notify the GameManager that the frog has reached home
        }
    }
}
