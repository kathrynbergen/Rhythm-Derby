using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private AccuracySnapper accuracySnapper;
    [SerializeField] private TempoManager tempoManager;
    [SerializeField] private PitcherSpriteAnimator Pitcher;
    [SerializeField] private BatterSpriteAnimator Batter;
    [SerializeField] private BallAnimator Ball;
    [SerializeField] private AccuracyTracker accuracyTracker;
    
    private int[] beatPattern;

    private int targetPitchBeat = -1; // Intended pitch beat - for snapping
    private int targetSwingBeat = -1;

    public void Pause(InputAction.CallbackContext context)
    {
        if (!context.performed || GameManager.Instance == null)
            return;

        if (GameManager.Instance.CurrentState is StateGamePaused) // unpause if already paused
            GameManager.Instance.ChangeState(GameManager.Instance.PlayingState);
        else // enter pause state
            GameManager.Instance.ChangeState(new StateGamePaused(GameManager.Instance));
    }
    // Player one always uses J. Which play J triggers depends on the current half.
    public void Pitch(InputAction.CallbackContext context)
    {
        if (!context.performed || InputsAreLocked()) return;

        PlayerData data = FindPlayerData();
        if (data == null) return;

        if (data.PlayerOneIsPitching) StartPitch();
        else StartSwing();
    }
    
    // can eventually take a parameter for the input type (ex. pressing j vs k correlates to pattern 1 vs 2)
    // from the player to determine which beat pattern should be returned
    private int[] determinePitcherBeatPattern(int currentBeat)
    {
        int [] beatPattern = new int[3];
        
        //pattern 1: one beat to pitch, one beat to react
        beatPattern[0] = currentBeat; // windup
        beatPattern[1] = currentBeat + 1; // throw (batter hits here)
        beatPattern[2] = currentBeat + 2; // back to idle
        
        return beatPattern;
    }
    
    // Player two always uses F. Which play F triggers depends on the current half.
    public void Swing(InputAction.CallbackContext context)
    {
        if (!context.performed || InputsAreLocked()) return;

        PlayerData data = FindPlayerData();
        if (data == null) return;

        if (data.PlayerOneIsPitching) StartSwing();
        else StartPitch();
    }

    private void StartPitch()
    {
        int currentBeat = accuracySnapper.SnapToClosestBeat(tempoManager.GetSongTime());
        print("intended beat: " + currentBeat);

        beatPattern = determinePitcherBeatPattern(currentBeat);
        targetSwingBeat = beatPattern[1];
        StartCoroutine(AnimatePitch(beatPattern[0], beatPattern[1], beatPattern[2]));
    }

    private void StartSwing()
    {
        double inputTime = tempoManager.GetSongTime();
        double targetTime = tempoManager.GetBeatTimeFromStart(targetSwingBeat);
        accuracyTracker.DetermineAccuracy(inputTime, targetTime);

        int currentBeat = accuracySnapper.SnapToClosestBeat(inputTime);
        Batter.ChangeToDoneSwingingSprite();
        StartCoroutine(AnimateSwing(currentBeat, currentBeat + 1));
    }

    private PlayerData FindPlayerData()
    {
        return Object.FindFirstObjectByType<PlayerData>();
    }
    
    private IEnumerator AnimatePitch(int windupBeat, int throwBeat, int idleBeat)
    {
        yield return WaitUntilBeat(windupBeat);
        Pitcher.ChangeToWindupSprite(); // pitcher: windup

        yield return WaitUntilBeat(throwBeat);
        Pitcher.ChangeToThrowingSprite(); // pitcher: throw
        
        Ball.Appear();

        yield return new WaitForSeconds(0.05f);

        Ball.Vanish();

        yield return WaitUntilBeat(idleBeat);
        Pitcher.ChangeToIdleSprite(); // pitcher: back to idle
    }
    
    private IEnumerator WaitUntilBeat(int beat)
    {
        double targetTime = tempoManager.GetBeatTime(beat);

        while (AudioSettings.dspTime < targetTime) yield return null;
    }

    IEnumerator AnimateSwing(int swingBeat, int backToIdleBeat)
    {
        yield return WaitUntilBeat(swingBeat);
        Batter.ChangeToSwingingSprite();
        yield return new WaitForSeconds(0.1f);
        Batter.ChangeToDoneSwingingSprite(); // batter: swing
        
        yield return WaitUntilBeat(backToIdleBeat);
        Batter.ChangeToIdleSprite(); // batter: back to idle
    }

    private bool InputsAreLocked()
    {
        // locks inputs until after the countdown
        return GameManager.Instance == null || !(GameManager.Instance.CurrentState is PlayingGameState);
    }
}
