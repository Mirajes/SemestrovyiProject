using AYellowpaper.SerializedCollections;

public abstract class A_Inventory
{
    public SerializedDictionary<Item_SO, int> Inventory => _Inventory;
    protected SerializedDictionary<Item_SO, int> _Inventory;

    public virtual void AddToInventory(Item_SO item, int count)
    {
        if (IsItemExists(item))
        {
            _Inventory[item] += count;
        }
        else
        {
            _Inventory.Add(item, count);
        }
    }

    public virtual void RemoveFromInventory(Item_SO item, int count)
    {
        if (IsItemExists(item))
        {
            _Inventory[item] -= count;
        }
    }

    private bool IsItemExists(Item_SO item) => _Inventory.ContainsKey(item);
}

//public void ModifyInventory(ref SerializedDictionary<Item_SO, int> inventory, Item_SO item, int count)
//{
//    Debug.Log($"modified {item} - [{count}] to {inventory}");
//    inventory[item] += count;
//}