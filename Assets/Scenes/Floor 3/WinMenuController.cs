using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuController : MonoBehaviour
{
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Start Menu"); 
    }

    
    public void QuitGame()
    {
        Debug.Log("Quit Game pressed.");
        Application.Quit();
    }
}