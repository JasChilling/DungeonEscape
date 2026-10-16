using UnityEngine;
using UnityEngine.SceneManagement;

public class HallwayFinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Victory Screen");
        }
    }
}