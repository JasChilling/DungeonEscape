using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public GameObject promptCanvas;
    private bool playerInRange = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (promptCanvas != null) promptCanvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptCanvas != null) promptCanvas.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            
            if (Audio_Mangaer.Instance != null)
            {
                Audio_Mangaer.Instance.PlayCollectSound();
            }

            GameManger.Instance.AddItem();
            Destroy(gameObject);
        }
    }
}