using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3, score = 0;
    [SerializeField] float immortality = 0f;
    [SerializeField] Text scoreText, livesText;
    [SerializeField] Image[] hearts;
    public bool imortal = false;
    public bool keepTouching = false;
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
        stopingTouchcounter();
        if(keepTouching)
        {
            StartCoroutine(BeingImortal());
        }

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

        if (playerLives >= 3)
        {
            playerLives = 3;
        }

        UpdateHearts();

        livesText.text = playerLives.ToString();
    }
    public void TakeLive()
    {
        if (TouchCounter <= 0)
        {
            playerLives--;
            UpdateHearts();
            livesText.text = playerLives.ToString();

            TouchCounter = immortality;
        }
    }
    private void stopingTouchcounter()
    {   
        if (TouchCounter <= 0)
        {
            TouchCounter = 0;
        }
        else
        {   
            TouchCounter -= Time.deltaTime;
        }

    }
    IEnumerator BeingImortal()
    {
      
        imortal = true;

        if (keepTouching)
        {   
            yield return new WaitForSeconds(TouchCounter);

            imortal = false;
        }
    }

    void UpdateHearts()
    {
        for (int i=0; i < hearts.Length; i++)
        {
            if(i < playerLives)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
