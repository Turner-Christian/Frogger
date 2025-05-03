using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Frogger frogger;
    private Home[] homes;
    private int score;
    private int lives;

    void Awake()
    {
        homes = FindObjectsOfType<Home>();
        frogger = FindObjectOfType<Frogger>();
    }

    private void NewGame()
    {
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

        NewRound();
    }

    private void NewRound()
    {
        frogger.Respawn(); // Respawn the frog

    }

    public void HomeOccupied()
    {
        frogger.gameObject.SetActive(false); // Deactivate the frog
        if (Cleared())
        {
            Invoke(nameof(NewLevel), 1f); // Start a new level if all homes are occupied
        }else{
            Invoke(nameof(NewRound), 1f); // Start a new round if not all homes are occupied
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
        //TODO: Update score UI
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        //TODO: Update score UI

    }
}
