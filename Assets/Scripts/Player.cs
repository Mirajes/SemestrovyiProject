using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SerializedDictionary<ItemData, int> MainInventory => _mainInventory;
    public SerializedDictionary<ItemData, int> CycleInventory => _cycleInventory;
    public List<ItemData> CycleOrder => _cycleOrder;
    public List<ItemData> FightOrder => _fightOrder;
    public float BaseDamage => _baseDamage;

    [Header("Inventory")]
    [SerializedDictionary("Item", "Count")]
    [SerializeField] private SerializedDictionary<ItemData, int> _mainInventory = new();
    [SerializedDictionary("Item", "Count")]
    [SerializeField] private SerializedDictionary<ItemData, int> _cycleInventory = new();

    [SerializeField] private List<ItemData> _cycleOrder = new();
    [SerializeField] private List<ItemData> _fightOrder = new();

    [Header("Settings")]
    [SerializeField] private float _baseDamage = 1f;
    [SerializeField] private int _cycleOrderMaxSize = 3;
    [SerializeField] private int _fightOrderMaxSize = 4;

    [SerializeField] private GameObject _contextMenu;

    public void AddToInventory(ItemData item, int count)
    {
        if (_mainInventory.ContainsKey(item))
            _mainInventory[item] += count;
        else
            _mainInventory[item] = count;
    }

    private void OnMouseDown()
    {
        _contextMenu.SetActive(true);
    }

    private void OnMouseUp() // todo
    {

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(CursorManager.CursorPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            hit.collider.TryGetComponent<Interactable>(out Interactable actionButton);

            if (actionButton != null)
            {
                actionButton.Invoke();

            }
        }
        _contextMenu.SetActive(false);
    }

    private void OnMouseDrag()
    {

    }
}

