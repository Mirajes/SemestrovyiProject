using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private ItemData _data;
    [SerializeField] private Image _image;

    private Transform _parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        _parentAfterDrag = transform.parent;
        //transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        transform.parent = _parentAfterDrag;
    }

    private void OnEnable()
    {
        _image.sprite = _data.Sprite;
    }
}
