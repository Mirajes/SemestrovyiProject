using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    [Header("Public")]
    public bool IsAttacking => _isAttacking;
    public Vector2 CursorPos => _cursorPos;
    public float MinPassDistance => _minPassDistance;
    public float MaxPassDistance => _maxPassDistance;
    public float CursorSpeed => _cursorSpeed;

    [Header("Vars")]
    [SerializeField] private bool _isAttacking;
    [SerializeField] private Vector2 _cursorPos;
    private Vector3 _prevViewport;

    [Header("Cursor Hit Settings")]
    [SerializeField] private float _minPassDistance = 1f;
    [SerializeField] private float _maxPassDistance = 2f;
    [SerializeField] private float _cursorSpeed = 0f;

    public void OnAttackInput(InputAction.CallbackContext context) { _isAttacking = context.ReadValueAsButton(); }
    public void OnCursorPos(InputAction.CallbackContext context) { _cursorPos = context.ReadValue<Vector2>(); }

    private void Update()
    {
        if (_isAttacking)
        {
            Vector3 viewport = (Vector2)Camera.main.ScreenToViewportPoint(_cursorPos);
            float deltaTime = Mathf.Max(Time.unscaledDeltaTime, 1e-6f); // chto eto?
            _cursorSpeed = (viewport - _prevViewport).magnitude / deltaTime;
            _prevViewport = viewport;
        }
        else
        {
            _cursorSpeed = 0f; // stoit li? mozhno zhe onulirovat' v input.cancelled
        }
    }
}
/*


    // previous viewport position used for speed calculation
    private static Vector2 _prevViewport;


    public void Raycast() // ïðñò
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
 */