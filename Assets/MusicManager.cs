using System;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    private static MusicManager Instance;
    private AudioSource audioSource;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider musicSlider;
    public AudioClip backgroundMusic;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);


        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (backgroundMusic != null)
        {
            PlayBackgroundMusic(false, backgroundMusic);
        }

        musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider.value); });
    }

    public static void SetVolume(float volume)
    {
        Instance.audioSource.volume = volume;
    }

    private void PlayBackgroundMusic(bool v, object backgroundMusic)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    public static void PlayBackgroundMusic(bool resetSong, AudioClip audioClip = null)
    {
        if (audioClip != null)
        {
             Instance.audioSource.clip = audioClip;
        }
        if ( Instance.audioSource.clip != null)
        {
            if (resetSong)
            {
                 Instance.audioSource.Stop();
            }
             Instance.audioSource.Play();
        }
    }
    public static void PauseBackgroundMusic()
    {
         Instance.audioSource.Pause();
    }
}
