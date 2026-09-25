using System.Collections;
using UnityEngine;

public class TempoManager : MonoBehaviour
{
    private int BPM = 68; // should be changed based on the song
    
    // Plays metronome
    public AudioSource audioSource;
    public AudioClip soundEffect;

    private double songStartTime;
    
    public void Start()
    {
        startSong();
        startMetronome();
    }
    
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
        return AudioSettings.dspTime - songStartTime; // current time since game launch - time the song was started
    }
    // Called every interval when a "tick" is (every 8th note)
    public void UpdateMetronomeTick()
    {
        audioSource.PlayOneShot(soundEffect);
        print("beat = "+ GetCurrentBeat());
    }
    private void startSong()
    {
        // play song: songAudioSource.PlayOneShot(song); once we have a song, put it here
        setSongStartTime();
    }
    private void setSongStartTime()
    {
        songStartTime = AudioSettings.dspTime;
    }
    private void startMetronome()
    {
        StartCoroutine(beatInterval());
    }
    // Time between beats (every 8th note)
    private IEnumerator beatInterval()
    {
        yield return new WaitForSeconds(getBeatsPerSecond());
        UpdateMetronomeTick();
        StartCoroutine(beatInterval());
    }
    private float getBeatsPerSecond()
    {
        return 60f / BPM;
    }
    
}
