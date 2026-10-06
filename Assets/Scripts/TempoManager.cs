using System.Collections;
using UnityEngine;

public class TempoManager : MonoBehaviour
{
    private int BPM = 80; // should be changed based on the song
    
    // Plays metronome
    public AudioSource audioSource;
    public AudioClip soundEffect;

    private double metronomeStartTime;

    private int currentBeat = 0;
    private double nextBeatTime;
    
    // Returns the time duration of quarter note interval when called, example: float interval = tempoManager.QuarterNoteInterval
    // Not stored in case of speed ups/slow downs mid game that affect BPM - calculated when QuarterNoteInterval is referenced
    public float QuarterNoteInterval
    {
        get { return 60f / BPM; }
    }

    // returns which quarter note we are closest to
    public int GetCurrentBeat()
    {
        // math explanation is in AccuracySnapper.SnapToClosestBeat
        return Mathf.RoundToInt((float)GetSongTime() / QuarterNoteInterval);
    }
    // returns how far we are into the song
    public double GetSongTime()
    {
        return AudioSettings.dspTime - metronomeStartTime; // current time since game launch - time the song was started
    }
    // Called every interval when a "tick" is (every 8th note)
    public void UpdateMetronomeTick()
    {
        audioSource.PlayOneShot(soundEffect);
        print("beat = "+ GetCurrentBeat());
    }

    public void UpdateBPM(int newBPM)
    {
        BPM = newBPM;
    }
    public void StartMetronome(double startTime)
    {
        metronomeStartTime = startTime;
        nextBeatTime = metronomeStartTime + QuarterNoteInterval;
        currentBeat = 0;
        StartCoroutine(beatInterval());
    }
    private IEnumerator beatInterval()
    {
        //always updates
        while (true)
        {
            //wait until next beat to run coroutine again
            while (AudioSettings.dspTime < nextBeatTime)
            {
                yield return null;
            }
            
            //increase tick and play sound
            UpdateMetronomeTick(currentBeat);
            
            //update when the next beat is
            nextBeatTime += QuarterNoteInterval;
        }
    }
    
    public void UpdateMetronomeTick(int beat)
    {
        //metronome
        currentBeat++;
        audioSource.PlayOneShot(soundEffect);
        print("beat = " + beat);
    }
    
    // returns time of the beat into the song
    public double GetBeatTime(int beat)
    {
        return metronomeStartTime + (beat * QuarterNoteInterval);
    }
    
    // returns the time of the beat relative to the start of the song
    public double GetBeatTimeFromStart(int beat)
    {
        return beat * QuarterNoteInterval;
    }
}
