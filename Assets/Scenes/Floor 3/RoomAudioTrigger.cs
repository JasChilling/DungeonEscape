using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAudioTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource roomAudioSource;

    void Start()
    {
        if (roomAudioSource == null)
        {
            roomAudioSource = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (roomAudioSource != null && !roomAudioSource.isPlaying)
            {
                roomAudioSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (roomAudioSource != null && roomAudioSource.isPlaying)
            {
                roomAudioSource.Stop();
            }
        }
    }
}