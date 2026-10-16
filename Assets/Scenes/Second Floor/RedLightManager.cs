using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RedLightManager : MonoBehaviour
{
    public static RedLightManager Instance;

    [Header("UI & Visuals")]
    public GameObject redLightPanel;
    public Image warningImage;
    public Sprite[] warningSprites;

    [Header("Audio")]
    public AudioSource alertAudioSource;
    public AudioClip redLightAlertSound;

    public AudioSource ambientAudioSource;
    public AudioClip ambientTrack;

    [Header("Timing")]
    public float minGreenTime = 6f; 
    public float maxGreenTime = 10f; 
    public float redDuration = 2f;

    [HideInInspector]
    public bool isRedLight = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (redLightPanel != null) redLightPanel.SetActive(false);
        if (warningImage != null) warningImage.gameObject.SetActive(false);

        
        if (ambientAudioSource != null && ambientTrack != null)
        {
            ambientAudioSource.clip = ambientTrack;
            ambientAudioSource.loop = true;
            ambientAudioSource.Play();
        }

        StartCoroutine(RedLightRoutine());
    }

    IEnumerator RedLightRoutine()
    {
        while (true)
        {
            
            isRedLight = false;
            if (redLightPanel != null) redLightPanel.SetActive(false);
            if (warningImage != null) warningImage.gameObject.SetActive(false);

            
            if (alertAudioSource != null)
            {
                alertAudioSource.Stop();
            }

            
            if (ambientAudioSource != null && ambientTrack != null)
            {
                if (!ambientAudioSource.isPlaying)
                {
                    ambientAudioSource.clip = ambientTrack;
                    ambientAudioSource.loop = true;
                    ambientAudioSource.Play();
                }
            }

            
            float greenDuration = Random.Range(minGreenTime, maxGreenTime);
            float normalPlayTime = Mathf.Max(1f, greenDuration - 2f);
            yield return new WaitForSeconds(normalPlayTime);

            
            if (ambientAudioSource != null)
            {
                ambientAudioSource.Stop();
            }

            yield return new WaitForSeconds(2f);

            
            isRedLight = true;
            if (redLightPanel != null) redLightPanel.SetActive(true);
            if (warningImage != null) warningImage.gameObject.SetActive(true);

            if (warningSprites != null && warningSprites.Length > 0 && warningImage != null)
            {
                StartCoroutine(AnimateWarningImage());
            }

            if (alertAudioSource != null && redLightAlertSound != null)
            {
                alertAudioSource.clip = redLightAlertSound;
                alertAudioSource.loop = true;
                alertAudioSource.Play();
            }

            
            yield return new WaitForSeconds(redDuration);

            
            if (alertAudioSource != null)
            {
                alertAudioSource.Stop();
            }
        }
    }

    IEnumerator AnimateWarningImage()
    {
        int index = 0;
        while (isRedLight)
        {
            warningImage.sprite = warningSprites[index];
            index = (index + 1) % warningSprites.Length;
            yield return new WaitForSeconds(0.1f);
        }
    }
}