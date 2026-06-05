using AYellowpaper.SerializedCollections;
using UnityEngine;

public abstract class A_Inventory
{
    [SerializeField] [SerializedDictionary("Item","Count")] 
    protected SerializedDictionary<A_SO_Item, int> _Inventory = new();

    public SerializedDictionary<A_SO_Item, int> Inventory => _Inventory;

    public void AddItem(A_SO_Item item, int count)
    {
        if (_Inventory.ContainsKey(item))
        {
            _Inventory[item] += count;
        }
        else
        {
            _Inventory.Add(item, count);
        }
    }

    public void RemoveItem(A_SO_Item item, int count)
    {
        if (_Inventory.ContainsKey(item))
        {
            _Inventory[item] -= count;
        }
        else
        {
            _Inventory.Add(item, count);
        }
    }
}
