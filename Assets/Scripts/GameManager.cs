using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public void Pitch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("pitch");
        }
    }

    public void Swing(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            print("swing");
        }
    }
}
