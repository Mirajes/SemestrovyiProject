using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(CursorManager))]
public class GameManager : MonoBehaviour
{
    [Header("Main")]
    private InputSystem_Actions _inputMap;
    private CursorManager _cursorManager;
    //private StateManager _stateManager = new();
    private GambleManager _gambleManager = new();
    private CancellationTokenSource _cts;
    private Camera _mainCamera;
    [SerializeField] private EntityProgressBar _progressBar;

    [Header("Player")]
    [SerializeField] private Player _player;
    private Statement _state;
    public static Action<Statement> StateChange;

    [Header("UI")]
    public static bool IsInUI = false;
    [SerializeField] private GUI _gameUI;

    [Header("Poses")]
    [SerializeField] private Transform _playerHomePos;
    [SerializeField] private Transform _playerCyclePos;
    [SerializeField] private Transform _entitySpawnPos;
    [SerializeField] private Vector3 _cameraHomePos;
    [SerializeField] private Vector3 _cameraCyclePos;

    [Header("Entity")]
    private Entity _currentEntity;
    public static Action<EntityData> EntityDeath;

    [Header("Inventory")]
    private List<ItemData> _itemsDatas; // Все предметы в списке
    private List<ItemData> _inventoryMain = new();

    private List<ItemData> _cycleOrder = new();
    private int _maxCycleOrderCapacity = 3;

    private List<ItemData> _fightOrder = new();
    private int _maxFightOrder = 3;

    private void Awake()
    {
        _cursorManager = GetComponent<CursorManager>();

        InitControlls();

        _itemsDatas = Resources.LoadAll<ItemData>("Items").ToList();

        _progressBar.Init();

        if (_gameUI == null) { Debug.LogWarning("Gde GUI"); return; }
        _gameUI.Init();
        _gameUI.InitInventoryCanvas(_itemsDatas);
        _gameUI.InitCraftContainer(_itemsDatas);
    }

    private void Start()
    {
        _mainCamera = Camera.main;

        StartGame();
    }

    private void OnEnable()
    {
        StateChange += OnStateChange;
        EntityDeath += OnEntityDeath;
    }

    private void OnDisable()
    {
        StateChange -= OnStateChange;
        EntityDeath -= OnEntityDeath;
    }

    private void OnDestroy()
    {
        // stop input and cancel any running async loops
        RemoveControlls();
        DeleteCTS();
    }

    #region Controlls
    private void InitControlls()
    {
        _inputMap = new();

        _inputMap.Player.Cursor.performed += _cursorManager.KnowMousePos;

        _inputMap.Player.Attack.started += _cursorManager.OnHoldInput;
        _inputMap.Player.Attack.canceled += _cursorManager.OnHoldInput;

        _inputMap.Enable();
    }
    private void RemoveControlls()
    {
        _inputMap.Disable();
        _inputMap.Dispose();
    }
    #endregion

    private void StartGame()
    {
        ItemData someItem = Resources.Load<ItemData>("Items/Instrument");
        _cycleOrder.Add(someItem);

        _state = Statement.Cycle;

        OnStateChange(_state);
    }

    private void OnStateChange(Statement newState)
    {
        print(newState);
        switch (newState)
        {
            case Statement.Cycle:
                DeleteCTS();

                _cts = new();
                CycleTick(_cts.Token).Forget();
                MoveToCycle();
                break;
            case Statement.Home:
                DeleteCTS();
                MoveToHome();
                break;
            case Statement.Menu:
                _gameUI.ShowInventory();
                break;
            default:
                Debug.Log("where");
                break;
        }
    }

    private void MoveToCycle()
    {
        _player.transform.position = _playerCyclePos.transform.position;
        _mainCamera.transform.position = _cameraCyclePos;
    }

    private void MoveToHome()
    {
        _player.transform.position = _playerHomePos.transform.position;
        _mainCamera.transform.position = _cameraHomePos;
    }

    private void DeleteCTS()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }

    private async UniTask OnCycleCancel(CancellationToken token)
    {
        await UniTask.WaitUntilCanceled(token);
        if (_currentEntity != null)
            Destroy(_currentEntity.gameObject);
    }

    // copilot помог
    // TODO: может возникнуть проблема когда у предмета в цикле слетели Entity и Unity зависает наху
    private async UniTask CycleTick(CancellationToken token)
    {
        try
        {
            OnCycleCancel(token).Forget();

            // keep running until cancelled
            while (true)
            {
                // iterate over a snapshot so the collection can be modified elsewhere
                foreach (ItemData item in _cycleOrder.ToArray())
                {
                    token.ThrowIfCancellationRequested();

                    EntityData entity = _gambleManager.RollEntity(item);
                    if (entity == null || entity.EntityPrefab == null)
                        { Debug.Log("no entity or prefab"); await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token); continue; }

                    //await UniTask.Delay(TimeSpan.FromSeconds(1)); // delay TODO

                    if (_currentEntity != null) Destroy(_currentEntity.gameObject);
                    _currentEntity = Instantiate(entity.EntityPrefab, _entitySpawnPos.position, _entitySpawnPos.rotation);
                    _currentEntity.Init(entity);

                    // wait until the spawned object is destroyed OR the token is cancelled
                    // UnityEngine.Object == null works for destroyed objects
                    await UniTask.WaitWhile(() => _currentEntity != null, cancellationToken: token);
                }
                // when finished all items, the outer while(true) will restart the foreach
            }
    }
        catch (OperationCanceledException)
        {
            // cancellation requested - exit gracefully
            //Debug.LogError("смотри проблема 0_0");
        }
    }

    private void OnEntityDeath(EntityData entityData)
    {
        foreach (var item in entityData.DropResource)
        {
            ItemData inventoryItem = _itemsDatas.Find(x => item);
            if (inventoryItem != null)
            {
                inventoryItem.AddItem(1);
            }
        }
    }
}
