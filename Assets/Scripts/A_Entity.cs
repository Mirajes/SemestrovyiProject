using DG.Tweening;
using UnityEngine;

public abstract class A_Entity : MonoBehaviour
{
    [SerializeField] protected SO_Entity _EntityData;
    [SerializeField] protected float _CurrentHealth;

    public virtual void Spawn()
    {
        _CurrentHealth = _EntityData.BaseHealth;

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

    protected virtual void OnDestroy()
    {
        DOTween.Kill(this.transform);

    }

    public virtual void Explore() { }
}
