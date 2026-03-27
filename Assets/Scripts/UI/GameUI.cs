using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public EntityHealthBar HeathBar => _entityHealthBar;

    [Header("healthBar")]
    [SerializeField] private EntityHealthBar _entityHealthBar;

    [Header("Inventories")]
    [SerializeField] private RectTransform _mainInventoryTransform;
    [SerializeField] private RectTransform _mainInventoryContainer;
    [SerializeField] private RectTransform _cycleInventoryContainer;
    [SerializeField] private RectTransform _cycleOrderTransform;
    [SerializeField] private RectTransform _cycleOrderContainer;
    [SerializedDictionary("MainItem", "Slot")]
    private SerializedDictionary<ItemData, InventorySlot> _mainInventory = new();
    [SerializedDictionary("CycleItem", "Slot")]
    private SerializedDictionary<ItemData, InventorySlot> _cycleInventory = new();

    [Header("Buttons")]
    [SerializeField] private RectTransform _buttons;

    [Header("Prefabs")]
    [SerializeField] private InventorySlot _slotPrefab;
 
    public void InitCycleOrder(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            InventorySlot newSlot = Instantiate(_slotPrefab, _cycleOrderContainer);
        }
    }

    public void ShowMainInventory()
    {
        _mainInventoryTransform.gameObject.SetActive(true);
        ShowButtonList();
    }

    public void ShowCycleInventory()
    {
        _cycleOrderTransform.gameObject.SetActive(true);
        ShowButtonList();
    }

    public void ShowButtonList()
    {
        _buttons.gameObject.SetActive(true);
    }

    public void UpdateMainInventory(Dictionary<ItemData, int> items)
    {
        foreach (ItemData item in items.Keys)
        {
            if (_mainInventory.ContainsKey(item))
            {
                _mainInventory[item].UpdateData(items[item]); // add item amount
            }
            else
            {
                InventorySlot newSlot = Instantiate(_slotPrefab, _mainInventoryContainer);
                newSlot.Init(item, items[item]);
                _mainInventory.Add(item, newSlot);
            }
        }
    }

    public void UpdateCycleInventory(ItemData itemData, int count)
    {

    }
}