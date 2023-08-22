using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3, score = 0;
    [SerializeField] float HowLongYouCanTouch = 0f;
    [SerializeField] Text scoreText, livesText;
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
    private void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }
    public void AddToScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
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
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    public void AddToLives()
    {
        playerLives++;
        livesText.text = playerLives.ToString();
    }
    public void TakeLive()
    {
        TouchCounter -= Time.deltaTime;
        if (TouchCounter <= 0)
        {
            playerLives--;

            livesText.text = playerLives.ToString();

            TouchCounter = HowLongYouCanTouch;
        }

        
        
    }
}
