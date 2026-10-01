using UnityEngine;

public class AccuracySnapper : MonoBehaviour
{
    [SerializeField] private TempoManager tempoManager;
    [SerializeField] private AccuracyTracker accuracyTracker;

    // Returns int corresponding with the quarter note the player intended to hit
    public int SnapToClosestBeat(double inputTime)
    {
        float interval = tempoManager.QuarterNoteInterval;

        // Converts the input to a beat, then rounds to the nearest int (beat).
        // ex. inputTime = 4.05sec, interval = 0.88, then closestBeat = round(~4.6)= 5
        // Input corresponds to the "4.6th" beat, which is "snapped" to 5.
        int closestBeat = Mathf.RoundToInt((float)(inputTime / interval));
        
        // determine accuracy
        accuracyTracker.DetermineAccuracy(inputTime, tempoManager.GetBeatTimeFromStart(closestBeat));
        
        return closestBeat;
    }
}
