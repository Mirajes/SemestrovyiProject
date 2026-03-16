using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<ItemData> CycleOrder => _cycleOrder;
    public List<ItemData> FightOrder => _fightOrder;
    public Dictionary<ItemData, int> MainInventory => _mainInventory;
    public Dictionary<ItemData, int> CycleInventory => _cycleInventory;

    [SerializedDictionary("Main Inventory", "Count")]
    [SerializeField] private SerializedDictionary<ItemData, int> _mainInventory;
    [SerializedDictionary("Cycle Inventory", "Count")]
    [SerializeField] private SerializedDictionary<ItemData, int> _cycleInventory;

    [SerializeField] private List<ItemData> _cycleOrder = new();
    [SerializeField] private int _cycleOrderMaxSize = new();
    [SerializeField] private List<ItemData> _fightOrder = new();
    [SerializeField] private int _fightOrderMaxSize = new();

    public void ModifyCycleInventory(ItemData item,  int count)
    {
        if (_cycleInventory.ContainsKey(item))
            _cycleInventory[item] += count; // для отрицательных -- вычитает
        else 
            _cycleInventory.Add(item, count);
    }

    public void ModifyMainInventory(ItemData item, int count)
    {
        if (_cycleInventory.ContainsKey(item))
            _cycleInventory[item] += count; // okak
        else
            _cycleInventory.Add(item, count);
    }
}