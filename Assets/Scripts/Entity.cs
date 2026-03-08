using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Entity : MonoBehaviour
{
    public EntityData Data => _data;

    private EntityData _data;
    private float _passedDistance = 0f; // рудимент
    [SerializeField] private float _maxHealth = 10f;
    [SerializeField] private float _health = 3f;
    
    [Header("Cursor Hit Settings")]
    [SerializeField] private float _minPassDistance = 0.02f; // in viewport units (0..1)
    [SerializeField] private float _minPassSpeed = 0.5f; // viewport units per second

    // debounce so one hold can't hit multiple times
    private bool _hitDebounce = false;
    private Vector2 _enterViewportPos;
    private float _maxSpeedDuringPass = 0f;



    private void OnDestroy()
    {
        // animation only
    }

    private void OnMouseDown()
    {
        print("show info");
    }

    private void OnMouseUp()
    {
        if (CursorManager.IsAttacking)
        {
            // reset per-hold tracking
            _hitDebounce = false;
            _maxSpeedDuringPass = 0f;
        }
        print("stopped");
    }

    private void OnMouseDrag()
    {
        if (!CursorManager.IsAttacking) return;
        // track maximum cursor speed while dragging over this entity
        _maxSpeedDuringPass = Mathf.Max(_maxSpeedDuringPass, CursorManager.CursorSpeed);
    }

    private void OnMouseEnter()
    {
        if (!CursorManager.IsAttacking) return;
        // record entry position in normalized viewport coords
        if (Camera.main != null)
            _enterViewportPos = (Vector2)Camera.main.ScreenToViewportPoint(CursorManager.CursorPos);
        _maxSpeedDuringPass = CursorManager.CursorSpeed;
        print("on on entity");
    }

    private void OnMouseExit()
    {
        if (!CursorManager.IsAttacking) return; // stopping there _passedDistance = 0f;

        // compute passing distance and decide whether to apply damage
        if (!_hitDebounce)
        {
            if (Camera.main != null)
            {
                Vector2 exitVp = (Vector2)Camera.main.ScreenToViewportPoint(CursorManager.CursorPos);
                float passDistance = (exitVp - _enterViewportPos).magnitude;
                float passSpeed = Mathf.Max(_maxSpeedDuringPass, CursorManager.CursorSpeed);

                if (passDistance >= _minPassDistance && passSpeed >= _minPassSpeed)
                {
                    OnCursorStrike(passSpeed);
                    _hitDebounce = true;
                }
            }
        }

        print("ouch");
    }
    public void Init(EntityData data) { _data = data; }

    public void Death()
    {
        GameManager.EntityDeath?.Invoke(_data);
        Destroy(gameObject);
    }

    private void TakeDamage(float amount)
    {
        _health -= amount;
        ProgressBar.UpdateAmount(_maxHealth, _health);

        if (_health <= 0)
            Death();
    }

    private void OnCursorStrike()
    {
        float damage = CursorManager.CursorDamage * Mathf.Clamp01(CursorManager.CursorSpeed);
        TakeDamage(damage);
    }

    // overload to accept measured speed
    private void OnCursorStrike(float measuredSpeed)
    {
        float damage = CursorManager.CursorDamage * Mathf.Clamp01(measuredSpeed);
        TakeDamage(damage);
    }
}
