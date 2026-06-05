using DG.Tweening;
using UnityEngine;

public abstract class A_Entity : MonoBehaviour
{
    [SerializeField] protected SO_Entity _EntityData;
    [SerializeField] protected float _CurrentHealth;

    [SerializeField] protected bool _IsMouseHolding;
    [SerializeField] protected float _CursorSpeed;

    protected virtual void OnEnable()
    {
        CursorManager.CursorSpeedCalc += OnCursorSpeedCalc;
        CursorManager.IsMouseHolding += OnMouseHolding;
    }

    protected virtual void OnDisable()
    {
        CursorManager.CursorSpeedCalc -= OnCursorSpeedCalc;
        CursorManager.IsMouseHolding -= OnMouseHolding;
    }

    protected virtual void OnDestroy()
    {
        DOTween.Kill(this.transform);
    }

    private void OnMouseHolding(bool isMouseHolding) => _IsMouseHolding = isMouseHolding;
    private void OnCursorSpeedCalc(float speed) => _CursorSpeed = speed;

    public virtual void Spawn()
    {
        _CurrentHealth = _EntityData.BaseHealth;

        HealthBar.BarChange?.Invoke(_CurrentHealth, _EntityData.BaseHealth);
        HealthBar.Spawn?.Invoke();

        // + sound?
    }

    public virtual void MoveTo(Transform newTransform, float time)
    {
        this.transform.DOMove(newTransform.position, time);
        this.transform.rotation = newTransform.rotation;
    }

    public virtual void TakeDamage(float damage)
    {
        _CurrentHealth -= damage;

        if (_CurrentHealth <= 0)
        {
            Kill();
            Destroy(this.gameObject);
        }

        HealthBar.BarChange?.Invoke(_CurrentHealth, _EntityData.BaseHealth);
    }

    public virtual void Heal(float amount) // mb rewrite
    {
        _CurrentHealth += amount;
        if (_CurrentHealth > _EntityData.BaseHealth)
            _CurrentHealth = _EntityData.BaseHealth;
    }

    protected virtual void Kill() 
    {
        Loop.OnEntityCS?.Invoke();
    }

    //public virtual void Explore() { } // to future
}
