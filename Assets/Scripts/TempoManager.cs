using System.Collections;
using UnityEngine;

public class TempoManager : MonoBehaviour
{
    private int BPM = 68; // should be changed based on the song

    private int eightNoteCount; // measures tracked in 8th notes due to accuracy being compared by the 8th note.

    public void Start()
    {
        eightNoteCount = 0;
        startMetronome();
    }

    private void startMetronome()
    {
        StartCoroutine(BeatInterval());
    }
    // Called every interval when a "tick" is (every 8th note)
    public void UpdateMetronomeTick()
    {
        eightNoteCount++;
        //print("beatCount: " + beatCount);
        print("measure:" + eightNoteCount/8 + ", eighth note: " + eightNoteCount%8);
    }
    
    // Time between beats (every 8th note)
    public IEnumerator BeatInterval()
    {
        yield return new WaitForSeconds(getEighthNoteBeatsPerSecond());
        UpdateMetronomeTick();
        StartCoroutine(BeatInterval());
    }
    
    private float getBeatsPerSecond()
    {
        return 60f / BPM;
    }

    private float getEighthNoteBeatsPerSecond()
    {
        return 60f / 8 / BPM;
    }
}
