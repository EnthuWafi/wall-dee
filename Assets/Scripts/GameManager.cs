using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //text

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI pointText;

    //button/etc

    public GameObject restartScreen;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject gameplayUI;

    //DAY N NITE - Controlling speed of objects passing by!
    public float speed = 7f;

    private float score; //actual score
    private float point; //to shoot projectiles

    public float gravityValue = -9.8f;
    

    private SpawnManager spawnManager;
    private PlayerController playerController;

    //bool
    private bool isPaused;
    public bool isGameActive;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        isGameActive = false;
    }

    private void Update()
    {
        PauseGame();
    }

    IEnumerator ScoreIncrement()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(1);
            UpdateScore(1);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdatePoint(int pointToAdd)
    {
        point += pointToAdd;
        pointText.text = "Point: " + point;
    }

    public void GameOver()
    {
        isGameActive = false;

        restartScreen.SetActive(true);

    }
    public void StartGame()
    {
        score = 0;
        point = 0;

        UpdateScore(0);
        UpdatePoint(0);

        StartCoroutine(StartDelay());

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P) && isGameActive)
        {
            if (!isPaused)
            {
                Time.timeScale = 0f;
                pauseScreen.SetActive(true);
                isPaused = true;
            }
            else
            {
                Time.timeScale = 1f;
                pauseScreen.SetActive(false);
                isPaused = false;
            }
        }
    }

    IEnumerator StartDelay()
    { 
        titleScreen.SetActive(false);
        gameplayUI.SetActive(true);

        playerController.animator.SetBool("Start", true);

        yield return new WaitForSeconds(3);

        
        isGameActive = true;
        StartCoroutine(spawnManager.SpawnEnemy());
        StartCoroutine(spawnManager.SpawnPoint());
        StartCoroutine(ScoreIncrement());
    }
}
