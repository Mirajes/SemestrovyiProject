using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
    podpisat' bi craftPanel'ki k delegatu chtobi ne obnovlyat' kazhduyu po ocheredi
*/

public class CraftPanelLogic : MonoBehaviour
{
    [SerializeField] private ItemData _data;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Image _icon;
    [SerializeField] private ReceiptSlotLogic _receiptSlotPrefab;
    [SerializeField] private RectTransform _receiptContainer;

    [SerializeField] private Button _craftButton;

    public ItemData ItemData => _data;

    public void Init(ItemData data)
    {
        _data = data;

        _name.text = _data.Name;
        _icon.sprite = _data.Sprite;

        foreach (ItemData receiptItem in _data.ItemReceipt.Keys)
        {
            var newReceiptSlot = Instantiate(_receiptSlotPrefab, _receiptContainer);
            newReceiptSlot.Icon.sprite = receiptItem.Sprite;
            newReceiptSlot.CountText.text = _data.ItemReceipt[receiptItem].ToString();
        }

        _craftButton.onClick.AddListener(() => GameManager.CraftItem?.Invoke(_data));
    }
}
