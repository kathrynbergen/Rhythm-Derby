using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private AccuracySnapper accuracySnapper;
    [SerializeField] private TempoManager tempoManager;
    private bool pitchProcessing = false;
    public PitcherSpriteAnimator Pitcher;
    public BatterSpriteAnimator Batter;


    private int targetPitchBeat = -1; // Intended pitch beat - for snapping
    // Called when the player presses the button corresponding to pitching (player 1)
    
    public void Pitch(InputAction.CallbackContext context)
    {
        if (context.performed) // Ensures method is only called once when the player presses down the button fully
        {
            int currentBeat = tempoManager.GetCurrentBeat();

            //can be easily changed for different patterns
            int windupBeat = currentBeat;
            int throwBeat = currentBeat + 1;
            int idleBeat = currentBeat + 2;

            StartCoroutine(AnimatePitch(windupBeat, throwBeat, idleBeat));
        }

        
            
            // Check which beat the pitcher intended to pitch at and snap input to that beat
            //targetPitchBeat = accuracySnapper.SnapToClosestBeat(tempoManager.GetSongTime()); 
            //print("targetPitchBeat: " + targetPitchBeat);
            
    }
    
    private IEnumerator AnimatePitch(int windupBeat, int throwBeat, int idleBeat)
    {
        yield return WaitUntilBeat(windupBeat);
        Pitcher.ChangeToWindupSprite();
        print("Pitcher: Winding up pitch");

        yield return WaitUntilBeat(throwBeat);
        Pitcher.ChangeToThrowingSprite();
        print("Pitcher: Throw!");

        yield return WaitUntilBeat(idleBeat);
        Pitcher.ChangeToIdleSprite();
        print("Pitcher: Back to Idle");
    }
    
    private IEnumerator WaitUntilBeat(int beat)
    {
        double targetTime = tempoManager.GetBeatTime(beat);

        while (AudioSettings.dspTime < targetTime)
            yield return null;
    }

    IEnumerator WaitForThrow(double windupTime, double throwTime, double idleTime)
    {
        while (AudioSettings.dspTime < windupTime)
        {
            yield return null;
        }
        Pitcher.ChangeToWindupSprite();
        print("pitcher windup");
        
        while (AudioSettings.dspTime < throwTime)
        {
            yield return null;
        }
        Pitcher.ChangeToThrowingSprite();
        print("pitcher throw");
        
        while (AudioSettings.dspTime < idleTime)
        {
            yield return null;
        }
        Pitcher.ChangeToIdleSprite();
        print("pitcher idle");
    }

    // Called when the player presses the button corresponding to swinging (player 2)
    public void Swing(InputAction.CallbackContext context)
    {
        if (context.performed) // Ensures method is only called once when the player presses down the button fully
        {
            int currentBeat = tempoManager.GetCurrentBeat();

            //can be easily changed for different patterns
            int swingBeat = currentBeat;
            int backToIdleBeat = currentBeat + 1;
            
            Batter.ChangeToSwingSprite();
            
            StartCoroutine(AnimateSwing(swingBeat, backToIdleBeat));
            // Check which beat the batter intended to hit at
            // Check if they were supposed to swing on that beat
            // Give score based on accuracy / if they were supposed to swing on that beat
        }
    }

    IEnumerator AnimateSwing(int swingBeat, int backToIdleBeat)
    {
        yield return WaitUntilBeat(swingBeat);
        Batter.ChangeToSwingSprite();
        print("Batter: Swing!");
        
        yield return WaitUntilBeat(backToIdleBeat);
        Batter.ChangeToIdleSprite();
        print("Batter: Back to Idle");
    }
}
