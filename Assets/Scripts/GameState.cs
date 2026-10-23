public class GameState
{
    public enum EventType
    {
        GoToMainMenu,
        PauseGame,
        StartGame
    }
    protected GameManager gameManager;

    public GameState(GameManager manager)
    {
        gameManager = manager;
    }

    public virtual void OnMyStateEntered() { }
    public virtual void OnMyStateExited() { }
    public virtual void StateUpdate(){} // called every frame from update() method in gamemanager

    public virtual void OnEventReceived(EventType eventType) { }
}
