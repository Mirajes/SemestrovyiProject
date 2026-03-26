using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public EntityHealthBar HeathBar => _entityHealthBar;

    [Header("healthBar")]
    [SerializeField] private EntityHealthBar _entityHealthBar;

    [Header("Inventories")]
    [SerializeField] private RectTransform _mainInventoryTransform;
    [SerializeField] private RectTransform _mainInventoryContainer;
    [SerializeField] private RectTransform _cycleOrderTransform;
    [SerializeField] private RectTransform _cycleOrderContainer;

    [Header("Prefabs")]
    [SerializeField] private InventorySlot _slotPrefab;
 
    public void InitCycleOrder(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            InventorySlot newSlot = Instantiate(_slotPrefab, _cycleOrderContainer);
        }
    }
}
