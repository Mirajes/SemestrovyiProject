using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Item_SO _item;
    private Transform _originParent;

    [SerializeField] private Image _iconImage;
    [SerializeField] private Outline _highligh;


    public Item_SO Item
    {
        get { return _item; }
        set { _item = value; UpdateVisuals(); }
    }

    private void UpdateVisuals()
    {
        if (_item != null)
        {
            _iconImage.sprite = _item.Sprite;
            //_iconImage.color = Color.white;
        }
        else
        {
            _iconImage.sprite = null;
            //_iconImage.color = Color.clear;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}