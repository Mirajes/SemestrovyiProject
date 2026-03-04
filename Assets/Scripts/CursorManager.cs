using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour // для Raycast
{
    public static Vector2 CursorPos => _cursorPos;

    private static Vector2 _cursorPos;

    public void KnowMousePos(InputAction.CallbackContext callback)
    {
        _cursorPos = callback.ReadValue<Vector2>();
        //Debug.Log(_cursorPos);
    }
}
