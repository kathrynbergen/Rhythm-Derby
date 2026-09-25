using UnityEngine;

public class AccuracySnapper : MonoBehaviour
{
    public TempoManager tempoManager;

    // Returns int corresponding with the quarter note the player intended to hit
    public int SnapToClosestBeat(double inputTime)
    {
        float interval = tempoManager.QuarterNoteInterval;

        // Converts the input to a beat, then rounds to the nearest int (beat).
        // ex. inputTime = 4.05sec, interval = 0.88, then closestBeat = round(~4.6)= 5
        // Input corresponds to the "4.6th" beat, which is "snapped" to 5.
        int closestBeat = Mathf.RoundToInt((float)(inputTime / interval));
        
        return closestBeat;
    }
}
