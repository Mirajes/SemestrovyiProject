using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;


[RequireComponent(typeof(CursorManager), typeof(CycleManager), typeof(HomeManager))]
[RequireComponent(typeof(CameraController), typeof(GameUI))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool IsInUI => _isInUI;
    public bool IsPaused => _isPaused;
    public Player Player => _player;
    public GambleMachine Gamble => _gamble;
    public CameraController CameraController => _cameraController;
    

    [Header("Core")]
    private InputSystem_Actions _inputMap;

    private CancellationTokenSource _cts = new();
    private GambleMachine _gamble = new();

    [SerializeField] private Player _player;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private CursorManager _cursor;
    [SerializeField] private CycleManager _cycleManager;
    [SerializeField] private HomeManager _homeManager;
    [SerializeField] private CameraController _cameraController;

    [Header("Statement")]
    [SerializeField] private bool _isInUI;
    [SerializeField] private bool _isPaused;
    [SerializeField] private Statement _statement;

    [Header("Event")]
    public static Action<Statement> StateChange;
    public static Action<EntityData> EntityDeath;

    [Header("Data")]
    private List<ItemData> _itemDatas;

    [Header("Test")]
    private EntityData _testTree;
    private EntityData _testRock;

    private void Awake()
    {
        if (Instance  == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    private void Start()
    {
        if (_player == null || _gameUI == null || _cursor == null) { Debug.LogWarning("check links"); return; }

        _itemDatas = Resources.LoadAll<ItemData>("Items").ToList();

        _cameraController.Init();

        _gameUI.Init(_itemDatas);
        _gameUI.ProgressBar.Init();

        InitInputs();

        #region Test
        _testTree = Resources.Load<EntityData>("Entities/Tree");
        _testRock = Resources.Load<EntityData>("Entities/Rock");
        Test();
        #endregion
    }

    private void OnEnable()
    {
        EntityDeath += _cycleManager.OnEntityDeath;
        StateChange += OnStateChange;
    }

    private void OnDisable()
    {
        EntityDeath -= _cycleManager.OnEntityDeath;
        StateChange -= OnStateChange;
    }

    private void OnDestroy()
    {
        _inputMap.Disable();
        _inputMap.Dispose();

        DeleteCTS();
    }

    #region Test
    private void Test()
    {
        EntityDeath.Invoke(_testTree);
        EntityDeath.Invoke(_testTree);
        EntityDeath.Invoke(_testRock);

        StartGame();
    }
    private void StartGame()
    {
        _statement = Statement.Cycle;

        OnStateChange(_statement);
    }
    #endregion

    private void InitInputs() // todo: в отдельный наверно
    {
        _inputMap = new();

        _inputMap.Player.CursorPosition.performed += _cursor.KnowMousePos;
        _inputMap.Player.Hold.started += _cursor.OnHoldInput;
        _inputMap.Player.Hold.canceled += _cursor.OnHoldInput;

        _inputMap.Enable();
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

    private void OnStateChange(Statement newState)
    {
        print(newState);
        switch (newState)
        {
            case Statement.Cycle:
                DeleteCTS();

                _cts = new();
                _cycleManager.CycleTick(_cts.Token).Forget();
                _cycleManager.MoveToCycle();
                break;
            case Statement.Home:
                DeleteCTS();
                _homeManager.MoveToHome();
                break;
            case Statement.Menu:
                _gameUI.ShowInventory();
                break;
            default:
                Debug.Log("where");
                break;
        }
    }
}






