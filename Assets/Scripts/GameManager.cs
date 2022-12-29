using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //text

    public TextMeshProUGUI scoreText;

    public TMP_InputField inputValue;
    //button/etc

    public GameObject highScoreParent;
    public GameObject scorePrefab;

    public GameObject restartScreen;
    public GameObject inputScoreScreen;
    public GameObject highScoreScreen;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject gameplayUI;

    private ParticleSystem dustParticle;
    //DAY N NITE - Controlling speed of objects passing by!
    public float speed = 7f;

    public AudioClip bgMusic;

    private float score; //actual score
    private float point; //to shoot projectiles
    const float MAX_POINT = 5;

    public float gravityValue = -9.8f;

    private List<GameObject> all_scores = new List<GameObject>();

    private SpawnManager spawnManager;
    private PlayerController playerController;
    private ScoreDB scoreDatabase;
    private AudioSource myAudio;
    private Slider bgMusicSlider;
    private Slider pointSlider;
    //bool
    private bool isPaused;
    public bool isGameActive;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreDatabase = GameObject.Find("Score Database").GetComponent<ScoreDB>();
        myAudio = GetComponent<AudioSource>();
        bgMusicSlider = titleScreen.GetComponentInChildren<Slider>();
        pointSlider = gameplayUI.GetComponentInChildren<Slider>();
        dustParticle = GameObject.Find("Dust").GetComponent<ParticleSystem>();

        isGameActive = false;
    }

    private void Update()
    {
        myAudio.volume = bgMusicSlider.value;
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
        if (point > MAX_POINT)
        {
            point = MAX_POINT;
        }
        pointSlider.value = point / MAX_POINT;
    }

    public float Point{
        get {
            return point;
        }
    }
    public float Max_Point
    {
        get { return MAX_POINT;}
    }

    public void GameOver()
    {
        isGameActive = false;
        restartScreen.SetActive(true);
        bgMusicSlider.value = 0.2f;
        dustParticle.playbackSpeed = 1f;
    }
    public void StartGame()
    {
        score = 0;
        point = 0;

        UpdateScore(0);
        UpdatePoint(0);

        myAudio.clip = bgMusic;
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
                myAudio.Pause();
                Time.timeScale = 0f;
                pauseScreen.SetActive(true);
                isPaused = true;
            }
            else
            {
                myAudio.UnPause();
                Time.timeScale = 1f;
                pauseScreen.SetActive(false);
                isPaused = false;
            }
        }
    }

    public void InputScore()
    {
        restartScreen.SetActive(false);
        gameplayUI.SetActive(false);

        inputScoreScreen.SetActive(true);
    }
    public void DisplayScore()
    {

        if (!string.IsNullOrEmpty(inputValue.text))
        {
            string name = inputValue.text;
            scoreDatabase.AddScore(name, score);

            inputScoreScreen.SetActive(false);

            List<Dictionary<string, string>> listScores = scoreDatabase.GetListOfScores();

            int rank = 1;
            foreach (Dictionary<string, string> pair in listScores)
            {
                GameObject tempObj = Instantiate(scorePrefab);
                tempObj.GetComponent<ScoreScript>().SetScore(rank, pair["name"], Int32.Parse(pair["score"]));
                tempObj.transform.SetParent(highScoreParent.transform);
                all_scores.Add(tempObj);
                rank++;
            }
            highScoreScreen.SetActive(true);
        }
    }

    public void ClearScore()
    {
        foreach (GameObject obj in all_scores)
        {
            Destroy(obj);
        }
        scoreDatabase.Clear();
    }
    IEnumerator StartDelay()
    {
        titleScreen.SetActive(false);
        gameplayUI.SetActive(true);
        myAudio.Play();

        playerController.animator.SetBool("Start", true);

        yield return new WaitForSeconds(3);

        isGameActive = true;
        dustParticle.playbackSpeed = speed / 2;
        StartCoroutine(spawnManager.SpawnEnemy());
        StartCoroutine(spawnManager.SpawnPoint());
        StartCoroutine(ScoreIncrement());
    }

    public void PlaySound(AudioClip audioClip, float scale = 1f)
    {
        myAudio.PlayOneShot(audioClip, scale);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
