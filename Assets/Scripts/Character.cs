using UnityEngine;

public class Character : MonoBehaviour, IAlive
{
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public bool IsAlive => _currentHealth > 0;

    public virtual void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth < 0)
        { 
            _currentHealth = 0;
        }

    }

    public virtual void HealYourself(float amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth; 
        }

    }

}

public class Player : Character
{

}

public class NPC : Character
{

}