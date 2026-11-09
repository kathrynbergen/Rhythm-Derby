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
        
        // Player one pitches the first half; after that, player two gets to pitch.
        if (data.PlayerOneIsPitching)
        {
            data.SwapSidesAndResetInning();
            foreach (SpriteAnimator spriteAnimator in Object.FindObjectsByType<SpriteAnimator>(FindObjectsSortMode.None))
            {
                spriteAnimator.SwapSides();
            }
            // move popup
            if (popupManager == null) popupManager = Object.FindFirstObjectByType<PopupManager>();
            popupManager.SwapSides();
            Debug.Log("Three strikes. Swap sides: player two pitches, player one bats.");
            gameManager.ChangeState(gameManager.PlayingState);
        }
        else
        {
            Debug.Log("Three strikes in both halves. Game over.");
            GameOver();
        }
    }

    public override void OnMyStateExited()
    {
        if (endGameCanvas != null) endGameCanvas.enabled = false;
    }

    private void GameOver()
    {
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
