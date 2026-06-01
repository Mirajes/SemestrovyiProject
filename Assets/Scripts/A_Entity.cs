using UnityEngine;

public abstract class A_Entity : MonoBehaviour
{
    [SerializeField] protected SO_Entity _EntityData;
    [SerializeField] protected float _CurrentHealth;

    public virtual void Spawn()
    {
        _CurrentHealth = _EntityData.BaseHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        _CurrentHealth -= damage;

        if (_CurrentHealth <= 0)
            Death();
    }

    public virtual void Death()
    {

    }
}

public class Player : A_Entity
{

}

public class Object : A_Entity
{

}

public class NPC : A_Entity
{

}