using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Mangaer : MonoBehaviour
{
    public static Audio_Mangaer Instance;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip background;
    public AudioClip death;
    public AudioClip collect;
    public AudioClip win;
    public AudioClip damage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (musicSource != null && background != null)
        {
            musicSource.clip = background;
            musicSource.loop = true; 
            musicSource.Play();
        }
    }

    public void PlayCollectSound()
    {
        if (sfxSource != null && collect != null)
        {
            sfxSource.PlayOneShot(collect);
        }
    }
}
