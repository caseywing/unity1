using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;

    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;

        if (numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLive();
        }
        else
        {
            RestGame();
        }
    }

    private void RestGame()
    {
        Debug.Log("the game has reset");
    }

    private void TakeLive()
    {
        playerLives--;
    }
}
