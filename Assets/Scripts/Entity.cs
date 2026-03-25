using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Entity : MonoBehaviour
{
    public EntityData EntityData => _EntityData;

    [SerializeField] protected EntityData _EntityData;
    [SerializeField] protected float _MaxHealth;
    [SerializeField] protected float _CurrentHealth;

    private bool _isPassing = false;

    private void OnDestroy()
    {
        // anim
    }

    private void OnMouseEnter()
    {
        if (GameManager.Instance.CursorManager.IsAttacking)
            _isPassing = true;
        else
            _isPassing = false;
    }

    private void OnMouseExit()
    {
        var cursorManager = GameManager.Instance.CursorManager;
        print(cursorManager.CursorSpeed);
        if (cursorManager.IsAttacking && _isPassing)
            TakeDamage(cursorManager.CursorSpeed);
        else
            _isPassing = false;
    }


    public virtual void Init(EntityData data)
    {
        _EntityData = data;
        _MaxHealth = data.BaseHP;
        _CurrentHealth = _MaxHealth;

        if (_EntityData.IsNPC)
        {
            //Debug.Log("this is NPC");
        }

        UpdateHealthAmount(_MaxHealth, _CurrentHealth);
    }

    private void TakeDamage(float damage, float mult = 0.01f)
    {
        _CurrentHealth -= damage * mult;

        if (_CurrentHealth <= 0)
        {
            _CurrentHealth = 0;
            GameManager.EntityDie?.Invoke(this);
        }

        UpdateHealthAmount(_MaxHealth, _CurrentHealth);
    }

    private void UpdateHealthAmount(float maxHealth, float currentHealth)
    {
        var healthBar = UIService.Instance.Get<EntityHealthBar>();
        healthBar.UpdateAmount(maxHealth, currentHealth);
    }
}