using UnityEngine;

[CreateAssetMenu(fileName = "Stick_ItemSO", menuName = "Item SO/Stick_ItemSO")]
public class Stick_ItemSO : A_SO_Item
{
    public override void Use(A_Entity entity)
    {
        entity.TakeDamage(_Damage);
    }
}