using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstepwalk : MonoBehaviour
{

    public AudioSource footstepsounds;

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            footstepsounds.enabled = true;
        }
        else

            footstepsounds.enabled = false;
    }

}
