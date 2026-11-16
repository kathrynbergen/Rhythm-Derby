using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;  private set; }
    
    public GameState CurrentState;
    public PlayingGameState PlayingState;
    
    private LevelData currentLevelData;
    private int inningsToPlay = 1;
    private int completedInnings;
    private TMP_Text currentInningText;
    

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
        completedInnings = 0;
        ChangeState(new EnteringLevelState(this));
    }

    public void SetInningsToPlay(int innings)
    {
        inningsToPlay = Mathf.Max(1, innings);
    }

    public bool CompleteInning()
    {
        completedInnings++;
        UpdateCurrentInningText();
        return completedInnings >= inningsToPlay;
    }

    private void UpdateCurrentInningText()
    {
        if (currentInningText == null)
        {
            foreach (TMP_Text text in FindObjectsByType<TMP_Text>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (text.gameObject.name != "CurrentInning-Text") continue;
                currentInningText = text;
                break;
            }
        }

        if (currentInningText != null)
        {
            int currentInning = Mathf.Min(completedInnings + 1, inningsToPlay);
            currentInningText.text = $"Inning: {currentInning}";
        }
    }
    // every time the level is loaded, method is called
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            currentInningText = null;
            UpdateCurrentInningText();
            if (CurrentState is EnteringLevelState enteringLevelState) enteringLevelState.SetupLoadedLevel();
        }
    }
    public LevelData CurrentLevelData => currentLevelData;
}
