using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CursorManager))]
public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _playerPos;

    [Header("Entity")]
    [SerializeField] private Transform _entitySpawnPos;
    private Entity _currentEntity;
    private float _entitySpawnDelay = 0.5f;
    [SerializeField] private Dictionary<int, Entity> _entities = new(); // id + entity

    [Header("Core")]
    private InputSystem_Actions _inputMap;
    private CursorManager _cursorManager;

    [Header("Test")]
    public Action EveryFrame;

    [Header("Inventory")]
    [SerializeField] private List<ItemData> _itemInventory = new();

    private void Awake()
    {
        _cursorManager = GetComponent<CursorManager>();
        _cursorManager.Init();

        EveryFrame += _cursorManager.CheckForInteract;

        InitInputs();
    }

    private void OnEnable()
    {
        _inputMap.Enable();
    }

    private void Update()
    {
        //EveryFrame?.Invoke();
    }

    private void OnDisable()
    {
        _inputMap?.Disable();
    }

    private void OnDestroy()
    {
        EveryFrame -= _cursorManager.CheckForInteract;
    }

    private void InitInputs()
    {
        _inputMap = new();

        _inputMap.Player.Cursor.performed += callback => _cursorManager.KnowMousePos(callback);
    }
    
    private void Game()
    {

    }
}

// zalupa
//[RequireComponent(typeof(Image), typeof(TMP_Text))]
//public class ItemSlot : MonoBehaviour
//{
//    public ItemData ItemData => _item;

//    private ItemData _item;
//    private Image _image;
//    private TMP_Text _tmpTextCount;

//    public void Init(ItemData item)
//    {
//        _item = item; 
//        _image = GetComponent<Image>();
//        _tmpTextCount = GetComponent<TMP_Text>();

//        _image.sprite = _item.Sprite;
//        _tmpTextCount.text = _item.Count.ToString();
//    }
//}

public class EntityData : ScriptableObject
{

}

public class Entity : MonoBehaviour
{
    [SerializeField] private bool _isAlive = false;
}