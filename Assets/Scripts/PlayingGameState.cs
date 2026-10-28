using UnityEngine;

public class PlayingGameState : GameState
{
    public PlayingGameState(GameManager manager) : base(manager) { }

    public override void OnMyStateEntered()
    {
        LevelData levelData = gameManager.CurrentLevelData;
        if (levelData == null)
        {
            Debug.LogError("Cannot start playing because no level is selected.");
            return;
        }

        TempoManager tempoManager = Object.FindFirstObjectByType<TempoManager>();
        if (tempoManager == null)
        {
            Debug.LogError("Couldn't find TempoManager in PlayingGameState.");
            return;
        }

        PlayerData playerData = Object.FindFirstObjectByType<PlayerData>();
        if (playerData != null) playerData.ResetRun();

        tempoManager.ResetForNewRun();
        tempoManager.UpdateBPM(levelData.BPM);

        double startTime = AudioSettings.dspTime + 0.1;
        if (AudioManager.Instance == null)
        {
            Debug.LogError("Couldn't find AudioManager in PlayingGameState.");
            return;
        }

        AudioManager.Instance.PlayMusic(levelData.Music, startTime);
        tempoManager.StartMetronome(startTime);
    }
}
