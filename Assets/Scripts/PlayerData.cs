using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    private MainInventory _mainInventory = new();
    private CycleInventory _cycleInventory = new();

    [SerializeField] private List<Item_SO> _cycleOrder = new();
    [SerializeField] private List<Item_SO> _battleOrder = new();

    public MainInventory MainInventory => _mainInventory;
    public CycleInventory CycleInventory => _cycleInventory;

    public List<Item_SO> CycleOrder => _cycleOrder;
    public List<Item_SO> BattleOrder => _battleOrder;
}

//public void ModifyInventory(ref SerializedDictionary<Item_SO, int> inventory, Item_SO item, int count)
//{
//    Debug.Log($"modified {item} - [{count}] to {inventory}");
//    inventory[item] += count;
//}