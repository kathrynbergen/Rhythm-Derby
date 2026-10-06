using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;  private set; }

    private LevelData currentLevelData;

    public void Awake()
    {
        // singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);
        
        // subscribes OnSceneLoaded to scene loading event
        // means that whenever a scene is done loading, OnSceneLoaded is called
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    public void StartGame(LevelData levelData)
    {
        currentLevelData = levelData; // Used to hold onto current level selected when moving to next scene

        SceneManager.LoadScene("Game");
    }
    // every time the level is loaded, method is called
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            SetupLevel();
        }
    }
    private void SetupLevel()
    {
        TempoManager tempoManager = FindFirstObjectByType<TempoManager>();
        
        if (tempoManager != null)
        {
            // Update metronome and start song
            tempoManager.UpdateBPM(currentLevelData.BPM);
            double startTime = AudioSettings.dspTime + 0.1; // starts .1 second after loading
            AudioManager.Instance.PlayMusic(currentLevelData.Music, startTime);
            tempoManager.StartMetronome(startTime); 
        } else
        {
            Debug.Log("couldn't find TempoManager in GameManager.SetupLevel()");
            return;
        }
    }
}
