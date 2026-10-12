using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject tutorialPanel;
    public AudioSource sfxAudioSource;
    public AudioClip clickSound;

    void Update()
    {
        // Closes the tutorial panel when Escape is pressed and the panel is active
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (tutorialPanel != null && tutorialPanel.activeSelf)
            {
                tutorialPanel.SetActive(false);
            }
        }
    }

    public void PlayGame(string gameSceneName)
    {
        PlayClick();
        SceneManager.LoadScene(gameSceneName);
    }

    public void ToggleTutorial(bool isOpen)
    {
        PlayClick();
        if (tutorialPanel != null) tutorialPanel.SetActive(isOpen);
    }

    public void QuitGame()
    {
        PlayClick();
        Debug.Log("Quit Game");
        Application.Quit();
    }

    private void PlayClick()
    {
        if (sfxAudioSource != null && clickSound != null)
        {
            sfxAudioSource.PlayOneShot(clickSound);
        }
    }
}