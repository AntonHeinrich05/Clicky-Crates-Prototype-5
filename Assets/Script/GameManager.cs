using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnIntervall = 1f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    private int score = 0;
    public int lives = 3;
    public bool isGameActive = true;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    private bool isPaused = false;

    private AudioSource audioSource;
    public AudioClip destroySound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            changePaused();
        }
    }

    IEnumerator SpawnTargets()
    {
        while (isGameActive) {
            yield return new WaitForSeconds(spawnIntervall);
            int randomIndex = Random.Range(0, targets.Count);
            Instantiate(targets[randomIndex]);
        }
    }


    public void AddScore(int scoreToAdd) {

        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void AddLives(int livesToAdd)
    {
        lives += livesToAdd;

        
        if (lives <= 0)
        {
            isGameActive = false;
            GameOver();
            lives = 0;
        }
        livesText.text = "Lives: " + lives;
    }


    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(SpawnTargets());
        scoreText.text = "Score: " + score;
        livesText.text = "Lives: " + lives;

        titleScreen.SetActive(false);
        //difficulty
        spawnIntervall /= difficulty;
    }

    public void changePaused()
    {
        if (!isPaused)
        {
            pauseScreen.SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            pauseScreen.SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    public void DestroySound()
    {
        audioSource.PlayOneShot(destroySound);
    }
}
