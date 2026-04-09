using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public EntityHealthBar HeathBar => _entityHealthBar;

    [Header("healthBar")]
    [SerializeField] private EntityHealthBar _entityHealthBar;
    [Header("PopUp")]
    [SerializeField] private RectTransform _warnPopUp;

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
    [SerializeField] private RectTransform _craftContainer;
    [SerializeField] private CraftPanelLogic _craftPanelPrefab;

    [Header("Buttons")]
    [SerializeField] private RectTransform _buttons;

    [Header("Prefabs")]
    [SerializeField] private InventorySlot _slotPrefab;
 
    public void InitCraftWindow(List<ItemData> itemDatas)
    {
        foreach (ItemData item in itemDatas)
        {
            if (item.IsCanBeCrafted)
            {
                var newCraftPanel = Instantiate(_craftPanelPrefab, _craftContainer);
                newCraftPanel.Init(item);
            }
        }
    }

    public void InitCycleOrder(int slotCount)
    {

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

    public async UniTask ShowPopUp(CancellationToken token)
    {
        _warnPopUp.gameObject.SetActive(true);

        token.ThrowIfCancellationRequested();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

        _warnPopUp.gameObject.SetActive(false);
    }

    public void UpdateMainInventory(Dictionary<ItemData, int> items)
    {
        foreach (ItemData item in items.Keys)
        {
            if (_mainInventory.ContainsKey(item))
            {
                InventorySlot slot = _mainInventory[item];
                slot.UpdateData(items[item]); // add item amount
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
        if (_cycleInventory.ContainsKey(itemData))
        {
            InventorySlot slot = _cycleInventory[itemData];
            slot.UpdateData(count); // add item amount
        }
        else
        {
            InventorySlot newSlot = Instantiate(_slotPrefab, _cycleInventoryContainer);
            newSlot.Init(itemData, count);
            _cycleInventory.Add(itemData, newSlot);
        }
    }

    public void ResetCycleInventory()
    {
        _cycleInventory.Clear();
        foreach (Transform item in _cycleInventoryContainer)
        {
            Destroy(item.gameObject);
        }
    }
}