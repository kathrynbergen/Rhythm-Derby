using UnityEngine;

public class EndGameState : GameState
{
    public EndGameState(GameManager manager) : base(manager) { }

    public override void OnMyStateEntered()
    {
        PlayerData data = Object.FindFirstObjectByType<PlayerData>();
        if (data == null) return;
        
        // Player one pitches the first half; after that, player two gets to pitch.
        if (data.PlayerOneIsPitching)
        {
            data.SwapSidesAndResetInning();
            Debug.Log("Three strikes. Swap sides: player two pitches, player one bats.");
            gameManager.ChangeState(gameManager.PlayingState);
        }
        else
        {
            Debug.Log("Three strikes in both halves. Game over.");
        }
    }
}
