using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenButton : MonoBehaviour
{
    public Canvas PauseScreenCanvas;
    
    void Start()
    {
        PauseScreenCanvas.enabled = false;
    }

    public void OnClickResumeButton()
    {
        GameManager.Instance.ChangeState(GameManager.Instance.PlayingState);
    }

    public void OnClickReturnToMenuButton()
    {
        if (AudioManager.Instance!=null) AudioManager.Instance.StopMusic(); // pause music
        if (GameManager.Instance!=null) GameManager.Instance.ChangeState(GameManager.Instance.PlayingState);

        SceneManager.LoadScene("Menu"); // main menu
    }
}
