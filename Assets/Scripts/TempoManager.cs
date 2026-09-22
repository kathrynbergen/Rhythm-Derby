using System.Collections;
using UnityEngine;

public class TempoManager : MonoBehaviour
{
    private int BPM = 68; // should be changed based on the song
    private int quarterNoteCount; // measures tracked in 8th notes due to accuracy being compared by the 8th note.
    public AudioSource audioSource;
    public AudioClip soundEffect;

    public void Start()
    {
        quarterNoteCount = 0;
        startMetronome();
    }

    private void startMetronome()
    {
        StartCoroutine(beatInterval());
    }
    // Called every interval when a "tick" is (every 8th note)
    public void UpdateMetronomeTick()
    {
        quarterNoteCount++;
        //print("beatCount: " + beatCount);
        audioSource.PlayOneShot(soundEffect);
        print("measure:" + quarterNoteCount/4 + ", quarter note: " + quarterNoteCount%4);
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
