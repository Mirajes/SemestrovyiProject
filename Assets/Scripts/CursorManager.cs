using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour // для Raycast
{
    private Camera _mainCamera;
    private Vector2 _cursorPos;

    public void Init()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _mainCamera = Camera.main;
    }

    public void KnowMousePos(InputAction.CallbackContext callback)
    {
        _cursorPos = callback.ReadValue<Vector2>();
        //Debug.Log(_cursorPos);
    }

    public void CheckForInteract()
    {
        Vector2 worldPos = _mainCamera.ScreenToWorldPoint(_cursorPos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        Debug.Log(hit.collider);
    }
}