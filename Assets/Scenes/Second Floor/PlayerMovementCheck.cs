using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementCheck : MonoBehaviour
{
    void Update()
    {
        
        if (RedLightManager.Instance != null && RedLightManager.Instance.isRedLight)
        {
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                
                SceneManager.LoadScene("Death Screen");
            }
        }
    }
}