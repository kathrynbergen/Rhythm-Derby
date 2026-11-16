using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private LevelData testLevelData;

    public void StartOneInningGame()
    {
        GameManager.Instance.SetInningsToPlay(1);
        StartLevel(testLevelData); // passes in the song that is played
    }

    public void StartTwoInningsGame()
    {
        GameManager.Instance.SetInningsToPlay(2);
        StartLevel(testLevelData);
    }

    public void StartThreeInningsGame()
    {
        GameManager.Instance.SetInningsToPlay(3);
        StartLevel(testLevelData);
    }
    private void StartLevel(LevelData levelData)
    {
        GameManager.Instance.RequestLevelStart(levelData);
    }
}
