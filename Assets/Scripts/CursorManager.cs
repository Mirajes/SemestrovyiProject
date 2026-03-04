using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour // для Raycast
{
    public Vector2 CursorPos => _cursorPos;

    private Camera _mainCamera;
    private Vector2 _cursorPos;

    public StateManager StateManager;

    public void Init(Camera mainCamera)
    {
        _mainCamera = mainCamera;
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
    }
}

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [Header("Settings")]
    [SerializeField] private float _ortographicSize = 5.0f;

    public float OrtographicSize => _ortographicSize;

    private Transform _target;

    public void CameraZoon(float zoomOrthographicSize)
    {
        _mainCamera.orthographicSize = zoomOrthographicSize;
    }
}