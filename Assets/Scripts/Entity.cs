using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Entity : MonoBehaviour
{
    [Header("Public")]
    public EntityData Data => _data;

    [Header("Main")]
    private EntityData _data;
    [SerializeField] private float _maxHealth = 10f;
    [SerializeField] private float _currentHealth;

    [Header("Cursor Hit Settings")]
    private float _minPassDistance = .5f;
    private float _minPassSpeed = 1f;

    private float _passedDistance;

    private void OnDestroy()
    {
        // anim
    }

    private void OnMouseUp()
    {
        _passedDistance = 0f;
    }

    private void OnMouseDown()
    {
        // show info
        _passedDistance = 0f;
    }

    private void OnMouseEnter()
    {
        if (!CursorManager.IsAttacking) return;

        // start count
    }

    private void OnMouseDrag() // почему оно считает только тогда когда курсор начинает отсюда?
    {
        
    }

    private void OnMouseExit()
    {
        if (!CursorManager.IsAttacking) return;

        float damage = CursorManager.CursorSpeed * GameManager.Instance.Player.BaseDamage;
        TakeDamage(damage);

        _passedDistance = 0f;
    }

    public void Init(EntityData data)
    {
        _data = data; 
        EntityProgressBar.UpdateAmount(_maxHealth, _currentHealth); // todo: fix
    }

    public void Die()
    {
        GameManager.EntityDeath?.Invoke(_data);
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        EntityProgressBar.UpdateAmount(_maxHealth, _currentHealth); // todo: fix

        if (_currentHealth <= 0)
            Die();
    }
}

/*


    // debounce so one hold can't hit multiple times
    private Vector2 _enterViewportPos;
    private float _maxSpeedDuringPass = 0f;


    private void OnMouseDown()
    {
        // start of a new click/hold — reset per-hold tracking so debounce from previous hold
        // doesn't block this new attempt

        _maxSpeedDuringPass = 0f;

        print("show info");
    }

    private void OnMouseUp()
    {
        // Always reset per-hold tracking on mouse release so debounce does not
        // remain true if attacking state changed while the cursor was held.

        _maxSpeedDuringPass = 0f;

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

        if (Camera.main != null)
        {
            Vector2 exitVp = (Vector2)Camera.main.ScreenToViewportPoint(CursorManager.CursorPos);
            float passDistance = (exitVp - _enterViewportPos).magnitude;
            float passSpeed = Mathf.Max(_maxSpeedDuringPass, CursorManager.CursorSpeed);

            if (passDistance >= _minPassDistance && passSpeed >= _minPassSpeed)
            {
                OnCursorStrike(passSpeed);
            }

        }

        print("ouch");
    }
    public void Init(EntityData data) { _data = data; EntityProgressBar.UpdateAmount(_maxHealth, _currentHealth); }

    private void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        EntityProgressBar.UpdateAmount(_maxHealth, _currentHealth);

        if (_currentHealth <= 0)
            Death();
    }


    // overload to accept measured speed
    private void OnCursorStrike(float measuredSpeed)
    {
        float damage = CursorManager.CursorDamage * Mathf.Clamp01(measuredSpeed);
        TakeDamage(damage);
    }
*/