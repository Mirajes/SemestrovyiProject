using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public EntityProgressBar ProgressBar => _progressBar;

    [Header("Main")]
    [SerializeField] private CanvasGroup _interfaceCanvas;
    [SerializeField] private CanvasGroup _inventoryCanvas;
    [SerializeField] private EntityProgressBar _progressBar;

    [Header("Inventory")]
    [SerializeField] private Transform _inventoryMainEmpty;
    [SerializeField] private Transform _invContainer;
    [SerializeField] private SlotLogic _invSlotPrefab;
    [SerializeField] private Button _closeButton;

    [Header("Craft")]
    [SerializeField] private RectTransform _craftContainer;
    [SerializeField] private CraftSlotLogic _craftPanelPrefab;

    [Header("kuda")]
    [SerializeField] private Texture2D _cursorSprite;

    #region Main
    public void Init(List<ItemData> itemDatas)
    {
        //Cursor.SetCursor(_cursorSprite, Vector2.zero, CursorMode.ForceSoftware);
        InitInventoryCanvas(itemDatas);
        InitCraftContainer(itemDatas);
    }
    #endregion

    #region MainInventory
    public void InitInventoryCanvas(List<ItemData> itemDatas)
    {
        foreach(ItemData item in itemDatas)
        {
            SlotLogic newSlot = Instantiate(_invSlotPrefab, _invContainer);
            newSlot.name = item.Name;
            newSlot.Init(item);
            newSlot.UpdateSlot();
        }
    }

    public void ShowSlot(ItemData item)
    {
        
    }
    #endregion

    public void ShowInventory()
    {
        for (int i = 0; i < _invContainer.transform.childCount; i++)
        {
            SlotLogic slot = _invContainer.transform.GetChild(i).GetComponent<SlotLogic>();
            slot.UpdateSlot();
        }

        _inventoryMainEmpty.gameObject.SetActive(true);
    }

    #region CraftWindow
    public void InitCraftContainer(List<ItemData> itemDatas)
    {
        int craftingItemCount = 0;
        Vector2 craftPanelSize = _craftPanelPrefab.GetComponent<RectTransform>().sizeDelta;

        foreach (ItemData item in itemDatas)
        {
            if (!item.IsCanBeCrafted) continue; // низя делать

            craftingItemCount++;
            CraftSlotLogic newCraftPanel = Instantiate<CraftSlotLogic>(_craftPanelPrefab, _craftContainer);
            newCraftPanel.name = item.Name;
            newCraftPanel.Init(item);
            newCraftPanel.gameObject.SetActive(true);
        }

        _craftContainer.sizeDelta = new Vector2(_craftContainer.sizeDelta.x, craftPanelSize.y * craftingItemCount);
    }
    #endregion
}