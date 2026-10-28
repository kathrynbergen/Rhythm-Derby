using UnityEngine.SceneManagement;

public class EnteringLevelState : GameState
{
    private bool levelSetupComplete;

    public EnteringLevelState(GameManager manager) : base(manager) { }

    public override void OnMyStateEntered()
    {
        SceneManager.LoadScene("Game");
    }

    public void SetupLoadedLevel()
    {
        if (levelSetupComplete)
            return;

        if (!gameManager.SetupLevel())
            return;

        levelSetupComplete = true;
        gameManager.ChangeState(gameManager.PlayingState);
    }
}
