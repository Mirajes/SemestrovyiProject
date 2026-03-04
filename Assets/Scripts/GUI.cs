using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _interfaceCanvas;
    [SerializeField] private CanvasGroup _inventoryCanvas;

    [SerializeField] private Transform _invContainer;
    [SerializeField] private Image _invSlotPrefab;

    [SerializeField] private Texture2D _cursorSprite;

    public void Init()
    {
        //Cursor.SetCursor(_cursorSprite, Vector2.zero, CursorMode.ForceSoftware);
    }

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

}