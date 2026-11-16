using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private LevelData testLevelData;
    
    [SerializeField] private Canvas mainMenuCanvas;
    [SerializeField] private Canvas inningSelectionCanvas;

    public void OnClickStartGame()
    {
        // hide menu canvas
        mainMenuCanvas.enabled = false;
        // show selection canvas
        inningSelectionCanvas.enabled = true;
    }

    public void OnClickLeaderboard()
    {
        Debug.Log("leaderboard shows up now!!");
    }
    public void OnBackButtonClick()
    {
        // hide selection canvas
        mainMenuCanvas.enabled = true;
        // show menu canvas
        inningSelectionCanvas.enabled = false;
    }
    public void OnClickStartOneInningGame()
    {
        GameManager.Instance.SetInningsToPlay(1);
        StartLevel(testLevelData); // passes in the song that is played
    }

    public void OnClickStartTwoInningsGame()
    {
        GameManager.Instance.SetInningsToPlay(2);
        StartLevel(testLevelData);
    }

    public void OnClickStartThreeInningsGame()
    {
        GameManager.Instance.SetInningsToPlay(3);
        StartLevel(testLevelData);
    }
    private void StartLevel(LevelData levelData)
    {
        GameManager.Instance.RequestLevelStart(levelData);
    }
}
