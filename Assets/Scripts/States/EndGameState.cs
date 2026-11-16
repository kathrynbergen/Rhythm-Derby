using UnityEngine;

public class EndGameState : GameState
{
    private Canvas endGameCanvas;
    private PopupManager popupManager;
    public EndGameState(GameManager manager) : base(manager) { }

    public override void OnMyStateEntered()
    {
        PlayerData data = Object.FindFirstObjectByType<PlayerData>();
        if (data == null) return;
        
        bool inningComplete = !data.PlayerOneIsPitching; // player one pitches once per inning
        data.SwapSidesAndResetInning();
        foreach (SpriteAnimator spriteAnimator in Object.FindObjectsByType<SpriteAnimator>(FindObjectsSortMode.None))
        {
            spriteAnimator.SwapSides();
        }

        if (popupManager == null) popupManager = Object.FindFirstObjectByType<PopupManager>();
        if (popupManager != null) popupManager.SwapSides();

        if (inningComplete && gameManager.CompleteInning())
        {
            Debug.Log("All innings complete. Game over.");
            GameOver();
            return;
        }

        Debug.Log("Three strikes. Swapping sides.");
        gameManager.ChangeState(gameManager.PlayingState);
    }

    public override void OnMyStateExited()
    {
        if (endGameCanvas != null) endGameCanvas.enabled = false;
    }

    private void GameOver()
    {
        PlayerData data = Object.FindFirstObjectByType<PlayerData>();
        if (data != null)
        {
            float playerOneAccuracy = data.GetPlayerOneAccuracy();
            float playerTwoAccuracy = data.GetPlayerTwoAccuracy();
            GameUI.ShowFinalResults(data.GetPlayerOneAccuracyPercent(), data.GetPlayerTwoAccuracyPercent(),
                data.GetPlayerOneHighestCombo(), data.GetPlayerTwoHighestCombo(),
                playerOneAccuracy > playerTwoAccuracy, Mathf.Approximately(playerOneAccuracy, playerTwoAccuracy));
        }

        if (endGameCanvas == null)
        {
            foreach (Canvas candidate in Object.FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (candidate.name != "EndGame-Canvas") continue;
                endGameCanvas = candidate;
                break;
            }
        }

        if (endGameCanvas == null)
        {
            Debug.LogError("Cannot show game over screen: EndGame-Canvas was not found in the loaded scene.");
            return;
        }

        endGameCanvas.enabled = true;
        if (AudioManager.Instance != null) AudioManager.Instance.StopMusic();
    }
}
