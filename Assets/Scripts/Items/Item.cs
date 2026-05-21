using UnityEngine;

[System.Serializable]
public abstract class Item
{
    protected Item_SO _ItemData;
    public Item_SO Data => _ItemData;

    public virtual void Activate(Entity entity)
    {
        Debug.Log($"[{this._ItemData.Name}] - Active [{entity}]");
    }
}
