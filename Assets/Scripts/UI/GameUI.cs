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

    public void InitCycleOrder(int slotCount)
    {

    }

    public void UpdateCycleOrder(int index, ItemData itemToChange)
    {

    }

    public List<ItemData> SaveCycleOrder()
    {
        return null;
    }
}
