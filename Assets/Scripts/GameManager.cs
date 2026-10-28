using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;  private set; }
    
    public GameState CurrentState;
    public PlayingGameState PlayingState;
    
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
        PlayingState = new PlayingGameState(this);
        
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
            if (CurrentState is EnteringLevelState enteringLevelState) enteringLevelState.SetupLoadedLevel();
        }
    }
    public LevelData CurrentLevelData => currentLevelData;
}
