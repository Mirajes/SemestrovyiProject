using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected EntityData _entityData;
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;

    public virtual void Init(EntityData data)
    {
        _entityData = data;
        _maxHealth = data.BaseHP;
        _currentHealth = _maxHealth;

        if (_entityData.IsNPC)
        {
            //Debug.Log("this is NPC");
        }
    }

    private void OnDestroy()
    {
        // anim
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {

    }
}