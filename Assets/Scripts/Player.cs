using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializedDictionary("Main Inventory", "Count")]
    [SerializeField] private SerializedDictionary<ItemData, int> _mainInventory;

    [SerializeField] private List<ItemData> _cycleOrder;
    [SerializeField] private int _cycleOrderMaxSize;
    [SerializeField] private List<ItemData> _fightOrder;
    [SerializeField] private int _fightOrderMaxSize;

    public List<ItemData> CycleOrder => _cycleOrder;
    public List<ItemData> FightOrder => _fightOrder;
}
