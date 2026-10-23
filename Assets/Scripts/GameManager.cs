using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;  private set; }
    
    public GameState CurrentState;
    public GameState PlayingState;
    
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
        PlayingState = new GameState(this);
        CurrentState = PlayingState;
        
        DontDestroyOnLoad(gameObject);
        
        // subscribes OnSceneLoaded to scene loading event
        // means that whenever a scene is done loading, OnSceneLoaded is called
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.StateUpdate();
        }

        CurrentState.StateUpdate();
    }

    public void ChangeState(GameState newState)
    {
        if (null != CurrentState)
        {
            CurrentState.OnMyStateExited();
        }
        
        CurrentState = newState;
        CurrentState.OnMyStateEntered();
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
