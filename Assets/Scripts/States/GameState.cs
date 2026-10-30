public class GameState
{
    protected GameManager gameManager;

    public GameState(GameManager manager)
    {
        gameManager = manager;
    }

    public virtual void OnMyStateEntered() { }
    public virtual void OnMyStateExited() { }
    public virtual void StateUpdate(){} // called every frame from update() method in gamemanager
    
}
