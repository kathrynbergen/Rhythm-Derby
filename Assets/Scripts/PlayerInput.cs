using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private AccuracySnapper accuracySnapper;
    [SerializeField] private TempoManager tempoManager;
    private bool pitchProcessing = false;

    private int targetPitchBeat = -1; // Intended pitch beat - for snapping
    // Called when the player presses the button corresponding to pitching (player 1)
    public void Pitch(InputAction.CallbackContext context) 
    {
        if (context.performed) // Ensures method is only called once when the player presses down the button fully
        {
            print("pitch"); 
            
            // Check which beat the pitcher intended to pitch at and snap input to that beat
            targetPitchBeat = accuracySnapper.SnapToClosestBeat(tempoManager.GetSongTime()); 
            print("targetPitchBeat: " + targetPitchBeat);

            // Track intended pitch time for batter to hit at - batter should swing one beat after the pitcher
        }
    }

    // Called when the player presses the button corresponding to swinging (player 2)
    public void Swing(InputAction.CallbackContext context)
    {
        if (context.performed) // Ensures method is only called once when the player presses down the button fully
        {
            print("swing");
            // Check which beat the batter intended to hit at
            // Check if they were supposed to swing on that beat
            // Give score based on accuracy / if they were supposed to swing on that beat
        }
    }
}
