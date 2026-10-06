using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Level")]
public class LevelData : ScriptableObject
{
    public string LevelName;
    public AudioClip Music;
    public float BPM;
}