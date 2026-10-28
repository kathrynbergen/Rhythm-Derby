using UnityEngine;
using UnityEngine.SceneManagement;

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

    }

    public void ChangeState(GameState newState)
    {
        if (null != CurrentState)
        {
            CurrentState.OnMyStateExited();
        }
        
        CurrentState = newState;
        CurrentState?.OnMyStateEntered();
    }

    public void RequestLevelStart(LevelData levelData)
    {
        if (levelData == null)
        {
            Debug.LogError("Cannot start a level without LevelData.");
            return;
        }

        currentLevelData = levelData;
        ChangeState(new EnteringLevelState(this));
    }
    // every time the level is loaded, method is called
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            if (CurrentState is EnteringLevelState enteringLevelState)
                enteringLevelState.SetupLoadedLevel();
        }
    }
    internal bool SetupLevel()
    {
        TempoManager tempoManager = FindFirstObjectByType<TempoManager>();
        
        if (tempoManager != null)
        {
            // Update metronome and start song
            ResetRunData();
            tempoManager.ResetForNewRun();
            tempoManager.UpdateBPM(currentLevelData.BPM);
            double startTime = AudioSettings.dspTime + 0.1; // starts .1 second after loading
            AudioManager.Instance.PlayMusic(currentLevelData.Music, startTime);
            tempoManager.StartMetronome(startTime); 
            return true;
        } else
        {
            Debug.Log("couldn't find TempoManager in GameManager.SetupLevel()");
            return false;
        }
    }

    private void ResetRunData()
    {
        PlayerData playerData = FindFirstObjectByType<PlayerData>();
        if (playerData != null)
            playerData.ResetRun();
    }
}
