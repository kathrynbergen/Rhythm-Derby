using System;
using UnityEngine;

// playerdata can count # of misses("outs")/single/homeruns and store % accuracy score
public class PlayerData : MonoBehaviour
{
    public bool PlayerOneIsPitching = true; // player one pitches first
    private int inningStrikeCount;
    // Percentage corresponding with correct notes hit
    private float playerOneAccuracyScore = 0;
    private float playerTwoAccuracyScore = 0;
    
    // Counts total # of inputs
    private int playerOneInputCount = 0;
    private int playerTwoInputCount = 0;

    // counts accuracy
    private int playerOneHomeRunCount = 0;
    private int playerOneSingleCount = 0;
    private int playerOneMissCount = 0;
    
    private int playerTwoHomeRunCount = 0;
    private int playerTwoSingleCount = 0;
    private int playerTwoMissCount = 0;

    public void ResetRun()
    {
        PlayerOneIsPitching = true;
        inningStrikeCount = 0;
        playerOneAccuracyScore = 0;
        playerTwoAccuracyScore = 0;
        playerOneInputCount = 0;
        playerTwoInputCount = 0;
        playerOneHomeRunCount = 0;
        playerOneSingleCount = 0;
        playerOneMissCount = 0;
        playerTwoHomeRunCount = 0;
        playerTwoSingleCount = 0;
        playerTwoMissCount = 0;
    }
    
    public void UpdateAccuracy(Accuracy accuracy)
    {
        bool batterIsPlayerOne = !PlayerOneIsPitching;
        switch (accuracy)
        {
            case Accuracy.HomeRun:
                if (batterIsPlayerOne) { playerOneHomeRunCount++; playerOneAccuracyScore++; }
                else { playerTwoHomeRunCount++; playerTwoAccuracyScore++; }
                return;
            case Accuracy.Single:
                if (batterIsPlayerOne) { playerOneSingleCount++; playerOneAccuracyScore += 0.5f; }
                else { playerTwoSingleCount++; playerTwoAccuracyScore += 0.5f; }
                return;
            case Accuracy.Strike:
                inningStrikeCount++;
                if (batterIsPlayerOne) playerOneMissCount++;
                else playerTwoMissCount++;
                return;
            default:
                Debug.Log("Unrecognized accuracy");
                return;
        }

    }

    public int GetPlayerMissCount()
    {
        return inningStrikeCount;
    }

    public void SwapSidesAndResetInning()
    {
        PlayerOneIsPitching = !PlayerOneIsPitching;
        inningStrikeCount = 0;
    }
    
}
