using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Start()
    {
        PlayMusic(menuMusic);
    }
    
    private void OnDestroy()
    {
        if (Instance != this) return;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Instance = null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Menu") PlayMusic(menuMusic);
    }
    
    // for playing any music (menu music, lose/win music, etc)
    public void PlayMusic(AudioClip music)
    {
        if (audioSource == null || music == null) return;
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

    public void StopMusic()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }
}
