using UnityEngine;

public abstract class A_Item
{
    [SerializeField] protected SO_Item _ItemData;
    [SerializeField] protected int _count;

    public SO_Item Data => _ItemData;
    public int Count => _count;

    public abstract void Use(A_Entity entity);

    public void AddItem(int count) => _count += count;
    public void RemoveItem(int count) => _count -= count;
}
