using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager // для Raycast
{
    private Camera _mainCamera;
    private Vector2 _cursorPos;

    public void KnowMousePos(InputAction.CallbackContext callback)
    {
        _cursorPos = callback.ReadValue<Vector2>();
        //Debug.Log(_cursorPos);
    }

    public void CheckForInteract()
    {
        Vector2 worldPos = _mainCamera.ScreenToWorldPoint(_cursorPos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
    }

    public CursorManager(Camera mainCamera)
    {
        _mainCamera = mainCamera;
    }
}