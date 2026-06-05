using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] private A_SO_Item _itemData;
    [SerializeField] private bool _isInLoopOrder;

    [Header("Links")]
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _countField;
    [SerializeField] private CanvasGroup _canvasGroup;

    public A_SO_Item ItemData => _itemData;
    public bool IsInLoopOrder => _isInLoopOrder;

    public void SetInvisible(bool isInvisible)
    {
        if (isInvisible)
        {
            _canvasGroup.alpha = 0.5f;
            _canvasGroup.blocksRaycasts = false;
        }
        else
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }
    }

    public void UpdateSlot()
    {
        _icon.sprite = _itemData.Sprite;

        if (!_isInLoopOrder)
        {
            _countField.gameObject.SetActive(true);
        }
        else
        {
            _countField.gameObject.SetActive(false);
        }
    }

    public void SetToLoop(bool toLoop) 
    {
        _isInLoopOrder = toLoop; 
    }
}
