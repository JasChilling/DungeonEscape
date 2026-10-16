using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int hearts = 3;
    public Image[] heartImages; // Drag your 3 UI Heart images here in the Inspector

    public void LoseHeart()
    {
        if (hearts > 0)
        {
            hearts--;
            UpdateHeartsUI();

            if (hearts <= 0)
            {
                // Trigger death screen or loss condition
                SceneManager.LoadScene("Death Screen");
            }
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < hearts)
            {
                heartImages[i].enabled = true; // Show heart
            }
            else
            {
                heartImages[i].enabled = false; // Hide lost heart
            }
        }
    }
}