using UnityEngine;

[CreateAssetMenu(fileName = "Stick_ItemSO", menuName = "Item SO/Stick_ItemSO")]
public class Stick_ItemSO : A_SO_Item
{
    [SerializeField] private float _damage;

    public override void Use()
    {
        if (_CurrentEntity != null)
            _CurrentEntity.TakeDamage(_damage);
    }
}
