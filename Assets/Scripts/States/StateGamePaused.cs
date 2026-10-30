using UnityEngine;
using UnityEngine.InputSystem;

public class StateGamePaused : GameState
{
    private UnityEngine.InputSystem.PlayerInput playerInput;
    private PauseScreenButton pauseScreen;

    public StateGamePaused(GameManager manager) : base(manager) { }

    public override void OnMyStateEntered()
    {
        pauseScreen = Object.FindFirstObjectByType<PauseScreenButton>();
        playerInput = Object.FindFirstObjectByType<UnityEngine.InputSystem.PlayerInput>();

        if (pauseScreen != null && pauseScreen.PauseScreenCanvas != null)
            pauseScreen.PauseScreenCanvas.enabled = true; // show pause buttons
        SetGameplayInputsEnabled(false); // disables pitcher/batter inputs

        Time.timeScale = 0; // freeze time
        AudioListener.pause = true; // pauses all audio
    }

    public override void OnMyStateExited()
    {
        if (pauseScreen != null && pauseScreen.PauseScreenCanvas != null)
            pauseScreen.PauseScreenCanvas.enabled = false; // hide pause buttons
        SetGameplayInputsEnabled(true); // enable pitcher/batter inputs

        // Clear scene-owned references so this persistent state can't retain
        // objects from an unloaded game scene.
        pauseScreen = null;
        playerInput = null;

        Time.timeScale = 1; // continue time
        AudioListener.pause = false; //unpauses all audio
    }

    private void SetGameplayInputsEnabled(bool enabled)
    {
        if (playerInput == null || playerInput.currentActionMap == null)
            return;

        // disables/enables pitch & swing inputs
        SetActionEnabled("Pitch", enabled);
        SetActionEnabled("Swing", enabled);
    }

    private void SetActionEnabled(string actionName, bool enabled)
    {
        InputAction action = playerInput.currentActionMap.FindAction(actionName);
        if (action == null)
            return;

        if (enabled)
            action.Enable();
        else
            action.Disable();
    }
}
