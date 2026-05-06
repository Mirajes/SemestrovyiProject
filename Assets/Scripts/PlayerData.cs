using AYellowpaper.SerializedCollections;
using UnityEngine;

public class PlayerData
{
    private SerializedDictionary<Item_SO, int> _cycleInv;
    private SerializedDictionary<Item_SO, int> _mainInv;

    public SerializedDictionary<Item_SO, int> CycleInv => _cycleInv;
    public SerializedDictionary<Item_SO, int> MainInv => _mainInv;

    public void ModifyInventory(ref SerializedDictionary<Item_SO, int> inventory, Item_SO item, int count)
    {
        Debug.Log($"modified {item} - [{count}] to {inventory}");
        inventory[item] += count;
    }
}
