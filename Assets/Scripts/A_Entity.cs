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

    public virtual void TakeDamage(float damage)
    {
        _CurrentHealth -= damage;

        if (_CurrentHealth <= 0)
            Death();
    }

    public virtual void Heal(float amount) // mb rewrite
    {
        _CurrentHealth += amount;
        if (_CurrentHealth > _EntityData.BaseHealth)
            _CurrentHealth = _EntityData.BaseHealth;
    }

    public virtual void Death() { }

    public virtual void Explore() { }
}
