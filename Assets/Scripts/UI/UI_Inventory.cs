using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [SerializeField] private Button _inventoryButton;
    [SerializeField] private RectTransform _inventoryWindow;
    [SerializeField] private UI_InventorySlot _inventorySlotPrefab;

    [SerializeField] private RectTransform _mainInventoryContainer;
    //private List<UI_InventorySlot> _inventorySlots;

    [SerializeField] private RectTransform _loopInventoryContainer;

    public void OnEnable()
    {
        _inventoryButton.onClick.AddListener(OnOffInventory);
    }

    public void OnDisable()
    {
        _inventoryButton.onClick.RemoveAllListeners();
    }

    private void OnOffInventory()
    {
        bool isActive = !_inventoryWindow.gameObject.activeSelf;
        _inventoryWindow.gameObject.SetActive(isActive);
    }

    private void UpdateMainInventory()
    {

    }

    private void UpdateLoopInventory()
    {

    }
}
