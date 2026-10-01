using System;
using UnityEngine;

// Determines player scoring (how close were they to the intended beat?)
public class AccuracyTracker : MonoBehaviour
{
    // consideration for future: should they change based on BPM? if so, how significantly?
    // times are in seconds 
    private double goodHitInterval = 0.25; 
    private double greatHitInterval = 0.15; 
    
    [SerializeField] private PlayerData playerData;
    public void DetermineAccuracy(double inputTime, double closestBeatTime)
    {
        // determine how far they were to the intended beat
        double timeToIntendedBeat = Math.Abs(inputTime - closestBeatTime);
        
        // give a rating based on how far they were
        Accuracy accuracy;

        if (timeToIntendedBeat < greatHitInterval)
        {
            accuracy = Accuracy.HomeRun;
        } else if (timeToIntendedBeat < goodHitInterval)
        {
            accuracy = Accuracy.Single;
        } else // miss
        {
            accuracy = Accuracy.Miss;
        }

        // update pitcher/batter data
        
        // fixme- update PlayerData here - pass in accuracy as a parameter
        playerData.UpdateAccuracy(accuracy);
        print("Accuracy: " + accuracy);
    }
}
