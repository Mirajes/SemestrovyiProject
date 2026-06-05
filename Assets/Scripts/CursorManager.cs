using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private float _minPassDistance = 2f;
    [SerializeField] private float _maxPassDistance = 5f;

    private bool _isHolding = false;
    private float _cursorSpeed = 0f;

    // previous viewport position used for speed calculation
    private Vector2 _previousViewportPos;
    private Vector2 _cursorPos = Vector2.zero;

    public bool IsHolding => _isHolding;
    public float CursorSpeed => _cursorSpeed;

    public static event Action<bool> IsMouseHolding;
    public static event Action<float> CursorSpeedCalc;

    private void Update()
    {
        if (!_isHolding)
        {
            _cursorSpeed = 0f;

            IsMouseHolding?.Invoke(IsHolding);
            CursorSpeedCalc?.Invoke(CursorSpeed);

            return;
        }

        Vector2 viewport = GetViewportPoint();
        float delta = Mathf.Max(Time.unscaledDeltaTime, 1e-6f);
        _cursorSpeed = (viewport - _previousViewportPos).magnitude / delta;
        _previousViewportPos = viewport;

        if (_cursorSpeed < _minPassDistance)
            _cursorSpeed = 0;
        else if (_cursorSpeed  > _maxPassDistance)
            _cursorSpeed = _maxPassDistance;

        IsMouseHolding?.Invoke(IsHolding);
        CursorSpeedCalc?.Invoke(CursorSpeed);
    }

    public void OnCursorInput(InputAction.CallbackContext context)
    {
        _cursorPos = context.ReadValue<Vector2>();
    }

    public void OnHoldInput(InputAction.CallbackContext context)
    {
        bool isHolding = context.ReadValueAsButton();

        // when starting newHolding, drop prevViewport to avoid a large jump
        if (isHolding && !_isHolding)
        {
            _previousViewportPos = GetViewportPoint();
            _cursorSpeed = 0f;
        }

        _isHolding = isHolding;
    }

    private Vector2 GetViewportPoint()
    {
        return (Vector2)Camera.main.ScreenToViewportPoint(_cursorPos);
    }
}
