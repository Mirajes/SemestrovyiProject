using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour // для Raycast
{
    [Header("Public")]
    public static Vector2 CursorPos => _cursorPos;
    public static bool IsAttacking => _isHolding;
    public static float CursorSpeed => _cursorSpeed;
    public static float CursorDamage => _cursorDamage;

    [Header("Main")]
    private static Vector2 _cursorPos;
    private static bool _isHolding = false;
    private static float _cursorSpeed = 0f;
    
    [SerializeField] private float _inspectorCursorDamage = 5f;
    private static float _cursorDamage = 5f;

    // previous viewport position used for speed calculation
    private static Vector2 _prevViewport;

    public void KnowMousePos(InputAction.CallbackContext context)
    {
        _cursorPos = context.ReadValue<Vector2>();
        //Debug.Log(_cursorPos);
    }

    public void Raycast() // прст
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(_cursorPos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        Debug.Log(hit.collider);
    }

    public void OnHoldInput(InputAction.CallbackContext context)
    {
        bool now = context.ReadValueAsButton();
        // when starting to hold - initialize previous viewport to avoid a large jump
        if (now && !_isHolding)
        {
            if (Camera.main != null)
                _prevViewport = (Vector2)Camera.main.ScreenToViewportPoint(_cursorPos);
            _cursorSpeed = 0f;
        }
        _isHolding = now;
    }

    private void Awake()
    {
        // transfer inspector value into static field so other classes can read it via CursorDamage
        _cursorDamage = _inspectorCursorDamage;
    }

    private void Update()
    {
        // only calculate cursor speed while holding to reduce unnecessary work
        if (!_isHolding)
        {
            _cursorSpeed = 0f;
            return;
        }

        if (Camera.main == null)
            return;

        var viewport = (Vector2)Camera.main.ScreenToViewportPoint(_cursorPos);
        float dt = Mathf.Max(Time.unscaledDeltaTime, 1e-6f);
        _cursorSpeed = (viewport - _prevViewport).magnitude / dt;
        _prevViewport = viewport;
    }
}
