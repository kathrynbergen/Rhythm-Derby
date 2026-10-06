using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get;  private set; }
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip menuMusic;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        PlayMusic(menuMusic);
    }
    
    // for playing any music (menu music, lose/win music, etc)
    public void PlayMusic(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.Play();
    }
    
    // for playing music when you need to synchronize (like start of rhythm game song)
    public void PlayMusic(AudioClip music, double startTime)
    {
        audioSource.Stop();
        audioSource.clip = music;
        audioSource.PlayScheduled(startTime);
    }
}