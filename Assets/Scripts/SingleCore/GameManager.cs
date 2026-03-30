using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : A_Singleton<GameManager>
{
    private List<ItemData> _itemDatas;
    private CancellationTokenSource _cts;
    private InputSystem_Actions _inputMap;

    private static GameManager _instance; // ?

    [Header("Actions")]
    public static Action<Entity> ReturnFromCycle;
    public static Action<Statement> ChangeState;
    public static Action<Entity> EntityDie;
    public static Action<SelectAction> DoAction;

    [Header("Public Signleton")]
    public Player Player => _player;
    public CycleManager CycleManager => _cycleManager;
    public HomeManager HomeManager => _homeManager;
    public CursorManager CursorManager => _cursorManager;
    public GambleLogic Gamble => _gamble;
    public Statement State => _state;

    [Header("Links")]
    [SerializeField] private Player _player;
    [SerializeField] private GameManager _gameManager; // ?
    [SerializeField] private CycleManager _cycleManager;
    [SerializeField] private HomeManager _homeManager;
    [SerializeField] private CursorManager _cursorManager;
    [SerializeField] private CameraManager _cameraManager;
    [SerializeField] private GameUI _gameUI;
    private GambleLogic _gamble = new();

    [Header("Vars")]
    [SerializeField] private Statement _state;

    protected override void Awake()
    {
        base.Awake();
      
        _itemDatas = Resources.LoadAll<ItemData>("Items").ToList(); // zachem

        UIService.Instance.Register(_gameUI);
        UIService.Instance.Register(_gameUI.HeathBar);
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        _inputMap = new();
        _inputMap.Player.Attack.started += _cursorManager.OnAttackInput;
        _inputMap.Player.Attack.canceled += _cursorManager.OnAttackInput;
        _inputMap.Player.CursorPosition.performed += _cursorManager.OnCursorPos;
        _inputMap.Enable();

        ChangeState += OnStateChanged;
        ReturnFromCycle += OnReturnFromCycle;
        EntityDie += OnEntityDie;
        DoAction += OnDoAction;

        #region Test
        _inputMap.Player.testUpdateAll.started += OnTestUpdate;
        GameManager.ChangeState?.Invoke(_state);
        #endregion
    }

    private void OnDisable()
    {
        _inputMap.Player.Attack.started -= _cursorManager.OnAttackInput;
        _inputMap.Player.Attack.canceled -= _cursorManager.OnAttackInput;
        _inputMap.Player.CursorPosition.performed -= _cursorManager.OnCursorPos;
        _inputMap.Dispose();

        ChangeState -= OnStateChanged;
        ReturnFromCycle -= OnReturnFromCycle;
        EntityDie -= OnEntityDie; 
        DoAction -= OnDoAction;

        _inputMap.Player.testUpdateAll.started -= OnTestUpdate;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIService.Instance.Clear();
    }

    /*
    */

    private void OnTestUpdate(InputAction.CallbackContext callback)
    {
        OnStateChanged(_state);
    }

    private void OnReturnFromCycle(Entity entity)
    {
        if (entity != null)
            Destroy(entity.gameObject);
    }

    private void OnStateChanged(Statement state)
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }

        _cts = new();

        switch (state)
        {
            case Statement.Home:
                _player.transform.position = _homeManager.PlayerHomePos.position;
                _cameraManager.FollowTarget(_cts.Token, _cameraManager.HomePos).Forget();

                _player.ConvertCycleToMain();
                _gameUI.UpdateMainInventory(_player.MainInventory);
                break;
            case Statement.Cycle:
                _player.transform.position = _cycleManager.PlayerSpawnPos.position;
                _cameraManager.FollowTarget(_cts.Token, _cameraManager.CyclePos).Forget();

                _cycleManager.EntitySpawnTask(_cts.Token).Forget();
                break;
            default:
                Debug.LogWarning("where state");
                break;
        }
    }

    private void OnEntityDie(Entity entity)
    {
        if (entity == null) return;

        var entityData = entity.EntityData;
        foreach (var item in entityData.Drop.Keys)
        {
            int count = entityData.Drop[item]; // * multiplier
            _player.ModifyCycleInventory(item, count);
        }

        Destroy(entity.gameObject);
    }

    private void OnDoAction(SelectAction action)
    {
        switch (action)
        {
            case SelectAction.ToCycle:
                _state = Statement.Cycle;
                ChangeState?.Invoke(_state);
                break;
            case SelectAction.ToHome:
                _state = Statement.Home;
                ChangeState?.Invoke(_state);
                break;
            case SelectAction.OpenInventory:
                _gameUI.ShowMainInventory();
                break;
            case SelectAction.OpenMind:
                break;
            default:
                Debug.Log($"how {action}");
                break;
        }
    }
}