using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public AudioClip pressButton;
    public AudioClip RedLight;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name.ToLower();

        if (sceneName.Contains("death"))
        {
            PlayDeathMusic();
        }
        else if (sceneName.Contains("victory") || sceneName.Contains("win"))
        {
            PlayWinMusic();
        }
        else playBackgroundMusic();

    }
    public void playBackgroundMusic()
    {
        if (musicSource != null && background != null)
        {
            musicSource.Stop();
            musicSource.clip = background;
            musicSource.loop = true;
            musicSource.Play();
        }
    }



    public void playClicKSound()
    {
        if (sfxSource != null && pressButton != null)
        {
            sfxSource.PlayOneShot(pressButton);
        }
    }
    public void PlayCollectSound()
    {
        if (sfxSource != null && collect != null)
        {
            sfxSource.PlayOneShot(collect);
        }
    }

    public void PlayDeathMusic()
    {
        if (musicSource != null && death != null)
        {
            musicSource.clip = death;
            musicSource.loop = false;
            musicSource.Play();

        }
    }
    public void PlayWinMusic()
    {

        if (musicSource != null && win != null)
        {
            musicSource.clip = win;
            musicSource.loop = false;
            musicSource.Play();

        }
    }

    public void PlayRedLightSound()
    {
        if (musicSource != null && win != null)
        {
            musicSource.clip = RedLight;
            musicSource.loop = false;
            musicSource.Play();


        }
    }
}
