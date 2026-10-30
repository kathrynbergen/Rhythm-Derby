using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnteringLevelState : GameState
{
    private bool levelSetupComplete;

    public EnteringLevelState(GameManager manager) : base(manager) { }

    public override void OnMyStateEntered()
    {
        SceneManager.LoadScene("Game");
        AudioListener.volume = 0; // mute all audio
    }

    public override void OnMyStateExited()
    {
        AudioListener.volume = 1;
        levelSetupComplete = false;
    }

    public void SetupLoadedLevel()
    {
        if (levelSetupComplete) return;

        levelSetupComplete = true;
        PlayerData playerData = Object.FindFirstObjectByType<PlayerData>();
        if (playerData != null) playerData.ResetRun();
        gameManager.StartCoroutine(CountIntoLevel());
    }

    private IEnumerator CountIntoLevel()
    {
        Debug.Log("3...");
        yield return new WaitForSeconds(1f);
        Debug.Log("2...");
        yield return new WaitForSeconds(1f);
        Debug.Log("1...");
        yield return new WaitForSeconds(1f);
        Debug.Log("Start!");
        gameManager.ChangeState(gameManager.PlayingState);
    }
}
