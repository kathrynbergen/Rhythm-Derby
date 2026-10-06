using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private LevelData testLevelData;

    public void StartTestLevel()
    {
        GameManager.Instance.StartGame(testLevelData);
    }
}