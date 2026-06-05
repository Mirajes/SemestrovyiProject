using UnityEngine;
using UnityEngine.EventSystems;

public class UI_InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] private UI_ItemSlot _itemSlot;
    //[SerializeField] private bool _inLoopSlot = false; 

    [SerializeField] private Transform _parentBeforeDrag;
    [SerializeField] private Canvas _mainParent;
    [SerializeField] private RectTransform _thisRectTransform;

    public UI_ItemSlot ItemSlot => _itemSlot;

    private void OnEnable()
    {
        if (_itemSlot != null)
            _itemSlot.UpdateSlot();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!_itemSlot.ItemData!) return;

        _parentBeforeDrag = this.transform;
        _itemSlot.transform.SetParent(_mainParent.transform);

        _itemSlot.SetInvisible(true);

        eventData.Use();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_itemSlot.ItemData!) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _thisRectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPosition
            );

        _itemSlot.transform.position = _thisRectTransform.TransformPoint(localPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_itemSlot.ItemData!) return;

        _itemSlot.SetInvisible(false);

        if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent<UI_InventorySlot>(out var targetSlot)
            && targetSlot != this)
        {
            SwapItems(targetSlot);
        }
        else
        {
            ReturnItem();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    private void SwapItems(UI_InventorySlot targetSlot)
    {
        var tempSlot = targetSlot.ItemSlot;
        targetSlot._itemSlot = this._itemSlot;
        this._itemSlot = tempSlot;

        this._itemSlot.transform.SetParent(this.transform);
        targetSlot._itemSlot.transform.SetParent(targetSlot.transform);
    }

    private void ReturnItem()
    {
        _itemSlot.transform.SetParent(_parentBeforeDrag);
    }
}
