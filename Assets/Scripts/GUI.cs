using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private CanvasGroup _interfaceCanvas;
    [SerializeField] private Texture2D _cursorSprite;

    [Header("Inventory")]
    [SerializeField] private CanvasGroup _inventoryCanvas;
    [SerializeField] private Transform _invContainer;
    [SerializeField] private Image _invSlotPrefab;

    [Header("Craft")]
    [SerializeField] private RectTransform _craftContainer;
    [SerializeField] private CraftPanelLogic _craftPanelPrefab;

    #region Main
    public void Init()
    {
        //Cursor.SetCursor(_cursorSprite, Vector2.zero, CursorMode.ForceSoftware);
    }
    #endregion

    #region MainInventory
    public void InitInventoryCanvas(List<ItemData> itemDatas)
    {
        foreach(ItemData item in itemDatas)
        {
            Image newSlot = Instantiate(_invSlotPrefab, _invContainer);
            newSlot.name = item.Name;
            newSlot.sprite = item.Sprite;
        }
    }

    public void ShowSlot(ItemData item)
    {
        
    }
    #endregion

    #region CraftWindow
    public void InitCraftContainer(List<ItemData> itemDatas)
    {
        int craftingItemCount = 0;
        Vector2 craftPanelSize = _craftPanelPrefab.GetComponent<RectTransform>().sizeDelta;

        foreach (ItemData item in itemDatas)
        {
            if (!item.IsCanBeCrafted) continue; // низя делать

            craftingItemCount++;
            CraftPanelLogic newCraftPanel = Instantiate<CraftPanelLogic>(_craftPanelPrefab, _craftContainer);
            newCraftPanel.name = item.Name;
            newCraftPanel.Init(item);
            newCraftPanel.gameObject.SetActive(true);
        }

        _craftContainer.sizeDelta = new Vector2(_craftContainer.sizeDelta.x, craftPanelSize.y * craftingItemCount);
    }
    #endregion
}