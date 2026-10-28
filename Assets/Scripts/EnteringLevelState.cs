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
    }

    public void SetupLoadedLevel()
    {
        if (levelSetupComplete) return;

        levelSetupComplete = true;
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
