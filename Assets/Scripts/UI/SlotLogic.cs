using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotLogic : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private ItemData _data;

    [SerializeField] private Image _sprite;
    [SerializeField] private TMP_Text _countField;

    public void Init(ItemData data)
    {
        _data = data;
        _sprite.sprite = _data.Sprite;
    }

    public void UpdateSlot()
    {
        _countField.text = _data.Count.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("ai");
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("trogaut");
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        print("perestali");
    }
}
