using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get;  private set; }

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

    public void StartGame(LevelData levelData)
    {
        AudioManager.Instance.PlayMusic(levelData.Music);

        SceneManager.LoadScene("Game");
    }
}
