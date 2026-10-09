using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Mangaer : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip background;
    public AudioClip death;
    public AudioClip collect;
    public AudioClip win;
    public AudioClip damage;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }



}
