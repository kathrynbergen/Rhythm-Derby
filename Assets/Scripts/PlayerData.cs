using UnityEngine;

// playerdata can count # of misses("outs")/single/homeruns and store % accuracy score
public class PlayerData : MonoBehaviour
{
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
    
    public void UpdateAccuracy(Accuracy accuracy)
    {
        // TEMP - treats player 1 as pitcher, player 2 as batter - update and add a check for
        // who is pitching when we add the "swap sides" mechanic

        // FIXME - only updating player1 now regardless of p1 or p2 who hit it. change this.
        switch (accuracy)
        {
            case Accuracy.HomeRun:
                playerOneHomeRunCount++;
                playerOneAccuracyScore++;
                return;
            case Accuracy.Single:
                playerOneSingleCount++;
                playerOneAccuracyScore++;
                return;
            case Accuracy.Strike:
                playerOneMissCount++;
                return;
            default:
                Debug.Log("Unrecognized accuracy");
                return;
        }

    }
    
}
