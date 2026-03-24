using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private List<ItemData> _itemDatas;
    private CancellationTokenSource _cts;
    private InputSystem_Actions _inputMap;

    public static GameManager Instance => _instance;
    private static GameManager _instance;

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
    [SerializeField] private CameraManager _cameraManager;
    private GambleLogic _gamble = new();

    [Header("Vars")]
    [SerializeField] private Statement _state;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

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
        _inputMap.Player.testUpdateAll.started += OnTestUpdate;
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

        _inputMap.Player.testUpdateAll.started -= OnTestUpdate;
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    private void OnTestUpdate(InputAction.CallbackContext callback)
    {
        OnStateChanged(_state);
    }

    private void OnReturnFromCycle(Entity entity)
    {
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

        switch (state)
        {
            case Statement.Home:
                _player.transform.position = _homeManager.PlayerHomePos.position;
                _cameraManager.FollowTarget(_cameraManager.HomePos).Forget();
                break;
            case Statement.Cycle:
                _cts = new();
                _cycleManager.EntitySpawnTask(_cts.Token).Forget();

                _player.transform.position = _cycleManager.PlayerSpawnPos.position;
                _cameraManager.FollowTarget(_cameraManager.CyclePos).Forget();
                break;
            default:
                Debug.LogWarning("where state");
                break;
        }
    }
}