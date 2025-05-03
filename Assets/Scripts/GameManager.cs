using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Frogger frogger;
    public AudioSource src; // Reference to the audio source for sound effects
    public AudioClip homeSound; // Reference to the sound played when the frog reaches home
    public GameObject gameOverMenu; // Reference to the game over menu UI
    public TextMeshProUGUI scoreText; // Reference to the score text UI
    public TextMeshProUGUI livesText; // Reference to the lives text UI
    public TextMeshProUGUI timeText; // Reference to the time text UI
    private Home[] homes;
    private int score;
    private int lives;
    private int time;

    void Awake()
    {
        homes = FindObjectsOfType<Home>();
        frogger = FindObjectOfType<Frogger>();
    }

    void Start()
    {
        NewGame(); // Start a new game
    }

    private void NewGame()
    {
        gameOverMenu.SetActive(false);
        SetScore(0);
        SetLives(3);
        NewLevel();
    }

    private void NewLevel()
    {
        for (int i = 0; i < homes.Length; i++)
        {
            homes[i].enabled = false; // Disable all homes
        }

        Respawn();
    }

    private void Respawn()
    {
        frogger.Respawn(); // Respawn the frog

        StopAllCoroutines(); // Stop any existing timers
        StartCoroutine(Timer(30)); // Start a timer for 30 seconds
    }

    private IEnumerator Timer(int duration)
    {
        time = duration; // Set the timer duration
        timeText.text = time.ToString(); // Update the timer UI

        while (time > 0)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            time--; // Decrease the timer
            timeText.text = time.ToString(); // Update the timer UI
        }

        frogger.Death(); // If time runs out, the frog dies
    }

    public void Died()
    {
        SetLives(lives - 1); // Decrease lives by 1

        if (lives > 0)
        {
            Invoke(nameof(Respawn), 1f); // Respawn the frog after 1 second
        }
        else
        {
            Invoke(nameof(GameOver), 1f); // If no lives left, game over
        }
        
    }

    private void GameOver()
    {
        frogger.gameObject.SetActive(false); // Deactivate the frog
        gameOverMenu.SetActive(true); // Show the game over menu

        StopAllCoroutines(); // Stop all timers
        StartCoroutine(PlayAgain()); // Start the play again coroutine
    }

    private IEnumerator PlayAgain()
    {
        bool playAgain = false;
        while (!playAgain)
        {
            if (Input.GetKeyDown(KeyCode.Return)) // Check for space key press
            {
                playAgain = true; // Set playAgain to true to exit the loop
            }

            yield return null; // Wait for the next frame
        }

        NewGame(); // Start a new game
    }

    public void AdvancedRow()
    {
        SetScore(score + 10);
    }

    public void HomeOccupied()
    {
        src.PlayOneShot(homeSound); // Play the home sound
        frogger.gameObject.SetActive(false); // Deactivate the frog
        int bonusPoints = time * 20; // Calculate bonus points based on remaining time
        SetScore(score + bonusPoints + 50);

        if (Cleared())
        {
            SetScore(score + 1000); // Bonus for clearing all homes
            SetLives(lives + 1); // Bonus life for clearing all homes
            Invoke(nameof(NewLevel), 1f); // Start a new level if all homes are occupied
        }else{
            Invoke(nameof(Respawn), 1f); // Start a new round if not all homes are occupied
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < homes.Length; i++)
        {
            if (!homes[i].enabled) // Check if any home is not occupied
            {
                return false;
            }
        }

        return true; // All homes are occupied
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString(); // Update the score UI
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = lives.ToString(); // Update the lives UI
    }
}
