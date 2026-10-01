using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private AccuracySnapper accuracySnapper;
    [SerializeField] private TempoManager tempoManager;
    [SerializeField] private PitcherSpriteAnimator Pitcher;
    [SerializeField] private BatterSpriteAnimator Batter;
    
    private bool pitchProcessing = false;

    private int targetPitchBeat = -1; // Intended pitch beat - for snapping
    
    
    // Called when the player presses the button corresponding to pitching (player 1)
    public void Pitch(InputAction.CallbackContext context)
    {
        if (context.performed) // Ensures method is only called once when the player presses down the button fully
        {
            // Check which beat the pitcher intended to pitch at and snap input to that beat
            int currentBeat = accuracySnapper.SnapToClosestBeat(tempoManager.GetSongTime()); 
            print("intended beat: " + currentBeat);
            
            // would always round down to the previous beat, so we don't want to use this in case the player is early
            //int currentBeat = tempoManager.GetCurrentBeat();
            
            int[] beatPattern = determinePitcherBeatPattern(currentBeat); 

            StartCoroutine(AnimatePitch(beatPattern[0], beatPattern[1], beatPattern[2]));
        }
    }
    
    // can eventually take a parameter for the input type (ex. pressing j vs k correlates to pattern 1 vs 2)
    // from the player to determine which beat pattern should be returned
    private int[] determinePitcherBeatPattern(int currentBeat)
    {
        int [] beatPattern = new int[3];
        
        //pattern 1: one beat to pitch, one beat to react
        beatPattern[0] = currentBeat;
        beatPattern[1] = currentBeat + 1;
        beatPattern[2] = currentBeat + 2;
        
        return beatPattern;
    }
    
    // Called when the player presses the button corresponding to swinging (player 2)
    public void Swing(InputAction.CallbackContext context)
    {
        if (context.performed) // Ensures method is only called once when the player presses down the button fully
        {
            int currentBeat = accuracySnapper.SnapToClosestBeat(tempoManager.GetSongTime()); 

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
    
    private IEnumerator AnimatePitch(int windupBeat, int throwBeat, int idleBeat)
    {
        yield return WaitUntilBeat(windupBeat);
        Pitcher.ChangeToWindupSprite(); // pitcher: windup

        yield return WaitUntilBeat(throwBeat);
        Pitcher.ChangeToThrowingSprite(); // pitcher: throw

        yield return WaitUntilBeat(idleBeat);
        Pitcher.ChangeToIdleSprite(); // pitcher: back to idle
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
        Pitcher.ChangeToWindupSprite(); // pitcher windup
        
        while (AudioSettings.dspTime < throwTime)
        {
            yield return null;
        }
        Pitcher.ChangeToThrowingSprite(); // pitcher throw
        
        while (AudioSettings.dspTime < idleTime)
        {
            yield return null;
        }
        Pitcher.ChangeToIdleSprite(); // pitcher idle
    }

    

    IEnumerator AnimateSwing(int swingBeat, int backToIdleBeat)
    {
        yield return WaitUntilBeat(swingBeat);
        Batter.ChangeToSwingSprite(); // batter: swing
        
        yield return WaitUntilBeat(backToIdleBeat);
        Batter.ChangeToIdleSprite(); // batter: back to idle
    }
}
