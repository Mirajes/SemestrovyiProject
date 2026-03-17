using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class GameManager : A_Singleton<GameManager>
{
    private List<ItemData> _itemDatas;
    private CancellationTokenSource _cts;
    private InputSystem_Actions _inputMap;

    public static Action<Entity> ReturnFromCycle;
    public static Action<Statement> ChangeState;

    [Header("Public Signleton")]
    public Player Player => _player;
    public CycleManager CycleManager => _cycleManager;
    public HomeManager HomeManager => _homeManager;
    public CursorManager CursorManager => _cursorManager;
    public GambleLogic Gamble => _gamble;
    public Statement State => _state;

    [Header("Links")]
    [SerializeField] private Player _player;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CycleManager _cycleManager;
    [SerializeField] private HomeManager _homeManager;
    [SerializeField] private CursorManager _cursorManager;
    private GambleLogic _gamble = new();

    [Header("Vars")]
    [SerializeField] private Statement _state;

    protected override void Awake()
    {
        base.Awake();
        _itemDatas = Resources.LoadAll<ItemData>("Items").ToList(); // zachem
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        _inputMap = new();
        _inputMap.Player.Attack.started += _cursorManager.OnAttackInput;
        _inputMap.Player.Attack.canceled += _cursorManager.OnAttackInput;
        _inputMap.Enable();

        ChangeState += OnStateChanged;
        ReturnFromCycle += OnReturnFromCycle;

        #region Test
        GameManager.ChangeState?.Invoke(_state);
        #endregion
    }

    private void OnDisable()
    {
        _inputMap.Player.Attack.started -= _cursorManager.OnAttackInput;
        _inputMap.Player.Attack.canceled -= _cursorManager.OnAttackInput;
        _inputMap.Dispose();

        ChangeState -= OnStateChanged;
        ReturnFromCycle -= OnReturnFromCycle;
    }

    private void OnDestroy()
    {

    }

    private void OnReturnFromCycle(Entity entity)
    {
        Destroy(entity.gameObject);
    }

    private void OnStateChanged(Statement state)
    {
        switch (state)
        {
            case Statement.Home:
                _cts.Cancel();
                _cts.Dispose();

                _player.transform.position = _homeManager.PlayerHomePos.position;
                break;
            case Statement.Cycle:
                _cts = new();
                _cycleManager.EntitySpawnTask(_cts.Token).Forget();

                _player.transform.position = _cycleManager.PlayerSpawnPos.position;
                break;
            default:
                Debug.LogWarning("where state");
                break;
        }
    }
}