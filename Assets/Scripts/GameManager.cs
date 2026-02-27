using UnityEngine;

public class GameManager : MonoBehaviour
{
    private InputSystem_Actions _inputMap;
    private CursorManager _cursor;

    private void Awake()
    {
        _cursor = new(Camera.main);

        InitControlls();
    }

    private void OnDestroy()
    {
        RemoveControlls();
    }

    private void InitControlls()
    {
        _inputMap = new();

        _inputMap.Player.Cursor.performed += _cursor.KnowMousePos;

        _inputMap.Enable();
    }

    private void RemoveControlls()
    {
        _inputMap.Disable();
        _inputMap.Dispose();
    }
}
