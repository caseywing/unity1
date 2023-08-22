using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] float HowLongYouCanTouch = 0f;
    private float TouchCounter = 0f;


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
    private void Update()
    {
        ProcessPlayerDeath();
    }
    public void ProcessPlayerDeath()
    {
        if (playerLives <= 0)
        {
            RestGame();
        }
    }

    private void RestGame()
    {
        Debug.Log("the game has reset");
    }

    public void TakeLive()
    {
        TouchCounter -= Time.deltaTime;
        if (TouchCounter <= 0)
        {
            playerLives--;

            TouchCounter = HowLongYouCanTouch;
        }

        
        Debug.Log(playerLives);
    }
}
