using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;

    [Header("Candy")]
    public int collectedItems = 0;
    public int targetItems = 10;
    public TextMeshProUGUI counterText;

    [Header("Timer")]
    public float timeRemaining = 180f;
    public bool timeIsRunning = true;
    public TextMeshProUGUI timerText;

    private bool gameEnded = false;

   
    private static string lastPlayedLevel = "First floor";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        
        if (currentScene == "First floor" || currentScene == "Second floor" || currentScene == "Second Floor" || currentScene == "Floor 3")
        {
            lastPlayedLevel = currentScene;
        }
    }

    private void Update()
    {
        if (timeIsRunning && !gameEnded)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timeIsRunning = false;
                CheckLossCondition();
            }
        }
    }

    public void AddItem()
    {
        if (gameEnded) return;

        collectedItems++;
        if (counterText != null)
        {
            counterText.text = "Candy Collected: " + collectedItems;
        }
        if (collectedItems >= targetItems)
        {
            TriggerVictory();
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        if (timerText != null)
        {
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    void CheckLossCondition()
    {
        if (collectedItems < targetItems && !gameEnded)
        {
            TriggerDeath();
        }
    }

    void TriggerDeath()
    {
        if (gameEnded) return;
        gameEnded = true;
        Time.timeScale = 1f;

        if (Audio_Mangaer.Instance != null)
        {
            Audio_Mangaer.Instance.PlayDeathMusic();
        }

        // Wait a fraction of a second so the music starts before loading the scene
        StartCoroutine(LoadSceneWithDelay("Death Screen"));
    }

    void TriggerVictory()
    {
        if (gameEnded) return;
        gameEnded = true;
        Time.timeScale = 1f;

        if (Audio_Mangaer.Instance != null)
        {
            Audio_Mangaer.Instance.PlayWinMusic();
        }

        StartCoroutine(LoadSceneWithDelay("Victory Screen"));
    }

    IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSecondsRealtime(0.5f); // Plays audio for 0.5 seconds before switching scenes
        SceneManager.LoadScene(sceneName);
    }

    public void ProceedToNextLevel()
    {
        playClickSound();
        Time.timeScale = 1f;

        // Determines next scene based on the last active level played
        if (lastPlayedLevel == "First floor")
        {
            SceneManager.LoadScene("Second Floor");
        }
        else if (lastPlayedLevel == "Second floor" || lastPlayedLevel == "Second Floor")
        {
            SceneManager.LoadScene("Floor 3");
        }
        else
        {
            SceneManager.LoadScene("Start Menu");
        }
    }

    public void RestartLevel()
    {
        playClickSound();
        Time.timeScale = 1f;

      
        SceneManager.LoadScene(lastPlayedLevel);
    }

    public void LoadSecondFloor()
    {
        playClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Second Floor");
    }

    public void LoadFloorThree()
    {
        playClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Floor 3");
    }

    public void LoadMainMenu()
    {
        playClickSound();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start Menu");
    }

    public void QuitGame()
    {
        playClickSound();
        Time.timeScale = 1f;
        Debug.Log("Quit Game");
        Application.Quit();
    }

    private void playClickSound()
    {
        if (Audio_Mangaer.Instance != null)
        {
            Audio_Mangaer.Instance.playClicKSound();
        }
    }
}