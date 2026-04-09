using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<ItemData> CycleOrder => _cycleOrder;
    public int CycleOrderMaxSize => _cycleOrderMaxSize;
    public List<ItemData> FightOrder => _fightOrder;
    public int FightOrderMaxSize => _fightOrderMaxSize;
    public Dictionary<ItemData, int> MainInventory => _mainInventory;
    public Dictionary<ItemData, int> CycleInventory => _cycleInventory;

    [SerializedDictionary("Main Inventory", "Count")]
    [SerializeField] private SerializedDictionary<ItemData, int> _mainInventory;
    [SerializedDictionary("Cycle Inventory", "Count")]
    [SerializeField] private SerializedDictionary<ItemData, int> _cycleInventory;

    [SerializeField] private List<ItemData> _cycleOrder = new();
    [SerializeField] private int _cycleOrderMaxSize = 3;
    [SerializeField] private List<ItemData> _fightOrder = new();
    [SerializeField] private int _fightOrderMaxSize = 3;

    public void ModifyCycleInventory(ItemData item,  int count)
    {
        if (_cycleInventory.ContainsKey(item))
            _cycleInventory[item] += count; // для отрицательных -- вычитает
        else 
            _cycleInventory.Add(item, count);

        var gameUI = UIService.Instance.Get<GameUI>();
        gameUI.UpdateCycleInventory(item, _cycleInventory[item]);
    }

    public void ModifyMainInventory(ItemData item, int count)
    {
        if (_mainInventory.ContainsKey(item))
            _mainInventory[item] += count; // okak
        else
            _mainInventory.Add(item, count);

        var gameUI = UIService.Instance.Get<GameUI>();
        gameUI.UpdateMainInventory(_mainInventory);
    }

    public void ConvertCycleToMain()
    {
        foreach (ItemData item in _cycleInventory.Keys)
        {
            if (_mainInventory.ContainsKey(item))
                _mainInventory[item] += _cycleInventory[item];
            else
                _mainInventory.Add(item, _cycleInventory[item]);
        }

        _cycleInventory.Clear();

        var gameUI = UIService.Instance.Get<GameUI>();
        gameUI.ResetCycleInventory();
    }
}