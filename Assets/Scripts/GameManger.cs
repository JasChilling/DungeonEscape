using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;
    public int collectedItems = 0;
    public TextMeshProUGUI counterText; // Use "Text" if using standard UI Text

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem()
    {
        collectedItems++;
        if (counterText != null)
        {
            counterText.text = "Candy Collected: " + collectedItems;
        }
    }
}
