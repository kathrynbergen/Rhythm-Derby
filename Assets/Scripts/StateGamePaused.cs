using UnityEngine;
using UnityEngine.InputSystem;

public class StateGamePaused : GameState
{
    private UnityEngine.InputSystem.PlayerInput playerInput;

    public StateGamePaused(GameManager manager) : base(manager) { }

    public override void OnMyStateEntered()
    {
        playerInput = Object.FindFirstObjectByType<UnityEngine.InputSystem.PlayerInput>();
        SetGameplayInputsEnabled(false); // disables pitcher/batter inputs

        Time.timeScale = 0; // freeze time
        AudioListener.pause = true; // pauses all audio
    }

    public override void OnMyStateExited()
    {
        SetGameplayInputsEnabled(true); // enable pitcher/batter inputs

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
