using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public bool IsOrdered => _isOrdered; // for set item to order

    [SerializeField] private ItemData _data;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private bool _isOrdered = false;
    [SerializeField] private Outline _outline;

    private Transform _parentBeforeDrag;

    public void Init(ItemData data = null, int count = 0)
    {
        if (data == null)
        {
            _isOrdered = true;
            SetCountText(count);
            return;
        }

        _data = data;

        _image.sprite = _data.Sprite;
        gameObject.name = _data.Name;

        SetCountText(count);
    }

    public void OnOffOutline(bool isActive)
    {
        _outline.gameObject.SetActive(isActive);
    }

    private void SetCountText(int count)
    {
        if (count == 0)
            _countText.gameObject.SetActive(false);
        else
            _countText.text = count.ToString();
    }

    public void UpdateData(int count = 0)
    {
        _countText.text = count.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _parentBeforeDrag = transform.parent;
        transform.SetParent(transform.parent.root);
        _image.raycastTarget = false;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.5f); // invisible
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

        /*
        nado zdelat' podrugomy chtobi tenevaya kopiya vstavala v mesto
         */

        //// Получаем родительский контейнер
        //var parent = transform.parent as RectTransform;

        //// Перебираем все дочерние объекты (слоты)
        //int newSiblingIndex = parent.childCount; // по умолчанию вставить в конец

        //for (int i = 0; i < parent.childCount; i++)
        //{
        //    var sibling = parent.GetChild(i);
        //    Vector3 siblingPosition = sibling.position;

        //    // Проверяем, где курсор относительно слота
        //    if (eventData.position.y > siblingPosition.y)
        //    {
        //        newSiblingIndex = i;
        //        break;
        //    }
        //}
        //// Устанавливаем индекс
        //transform.SetSiblingIndex(newSiblingIndex);
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        eventData.pointerDrag.TryGetComponent<InventorySlot>(out InventorySlot slot);
        if (slot != null && slot.IsOrdered)
        {


        }
        else
        {
            transform.SetSiblingIndex(0); // sibling - dochernyi v parent'e
            transform.SetParent(_parentBeforeDrag);
            _image.raycastTarget = true;
        }

        _image.color = Color.white;
    }
}