using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftSlotLogic : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;

    [SerializeField] private TMP_Text _craftItemName;
    [SerializeField] private Image _craftItemSprite;
    [SerializeField] private TMP_Text _craftItemCount;
    [SerializeField] private Button _craftButton;

    [SerializeField] private Transform _receiptContainer;
    [SerializeField] private GameObject _receiptSlotPanelPrefab;

    public void Init(ItemData itemData)
    {
        _itemData = itemData;

        _craftItemName.text = itemData.Name;
        _craftItemSprite.sprite = itemData.Sprite;
        _craftItemCount.text = "Owned: " + itemData.Count.ToString();

        _craftButton.onClick.RemoveAllListeners();
        _craftButton.onClick.AddListener(() => Debug.Log($"delaem {_itemData.Name}"));

        InitReceipt();
    }

    public void UpdatePanel()
    {
        
    }

    private void InitReceipt()
    {
        foreach (var item in _itemData.ItemReceipt.Keys)
        {
            GameObject newReceiptPanel = Instantiate<GameObject>(_receiptSlotPanelPrefab, _receiptContainer);
            Image sprite = newReceiptPanel.GetComponentInChildren<Image>();
            TMP_Text countText = newReceiptPanel.GetComponentInChildren<TMP_Text>();

            sprite.sprite = item.Sprite;
            countText.text = _itemData.ItemReceipt[item].ToString();

            newReceiptPanel.SetActive(true);
        }
    }
}
