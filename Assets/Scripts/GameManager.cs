using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class GameManager : A_Singleton<GameManager>
{
    [Header("Variables")]
    [SerializeField] private GameVariables _gameVariables;
    [SerializeField] private float _sanityRemaining;

    [Header("Core")]
    [SerializeField] private States _state;
    [SerializeField] private bool _inSleep;
    [SerializeField] private Player _player;
    [SerializeField] private PlayerData _playerData = new();
    [SerializeField] private CameraManager _cameraManager;
    CancellationTokenSource _cts;
    [SerializeField] private CycleLogic _cycleLogic = new();
    [SerializeField] private HomeLogic _homeLogic = new();

    [Header("Pos")]
    [SerializeField] private Transform _homePos;
    [SerializeField] private Transform _cyclePos;

    public float SanityRemaining
    {
        get => _sanityRemaining;
        set
        {
            _sanityRemaining = value;
            GameUI.UpdateSanityBar?.Invoke(_gameVariables.SanityCapacity_BASE, value);
        }
    }
    public PlayerData PlayerData => _playerData;

    public static Action<float> SanityUse;
    public static Action<States> StateChange;

    #region Debug
    [Header("debug")]
    [SerializeField] private Entity _debug_bolvan;
    public void debug_ToHome() => OnStateChange(States.Home);
    public void debug_ToCycle() => OnStateChange(States.Cycle);
    #endregion

    protected override void Awake()
    {
        base.Awake();

        _cts = new();

        StateChange += OnStateChange;
    }

    private void Start()
    {
        // init
        _sanityRemaining = _gameVariables.SanityCapacity_BASE;
        SanityRecoveryTask().Forget();
        OnStateChange(_state);
    }

    private void OnEnable()
    {
        SanityUse += OnSanityUse;
    }

    private void OnDisable()
    {
        SanityUse -= OnSanityUse;
    }

    private void OnDestroy()
    {
        StateChange -= OnStateChange;
    }

    private void OnStateChange(States state)
    {
        _cts?.Cancel();
        _cts?.Dispose();

        _cts = new();

        switch (state)
        {
            case States.Cycle:
                ToCycle();
                break;
            case States.Home:
                ToHome();
                break;
            default:
                Debug.Log($"where {state}");
                return;
        }

        _state = state;
    }

    private void OnSanityUse(float amount)
    {
        _sanityRemaining -= amount;
    }

    private async UniTask SanityRecoveryTask()
    {
        while (true)
        {
            await UniTask.WaitWhile(() => _sanityRemaining < _gameVariables.SanityCapacity_BASE);

            await UniTask.Delay(TimeSpan.FromSeconds(_gameVariables.SanityRecoveryCD));
            _sanityRemaining += _gameVariables.SamplingRecoveryAmount;
        }
    }


    private void ToCycle()
    {
        _player.transform.position = _cyclePos.position;
        _cameraManager.MainCamera.transform.position = _cameraManager.CyclePos.position;

        _cycleLogic.CycleTask(_cts.Token, _playerData.CycleOrder, _playerData.BattleOrder).Forget();
    }

    private void ToHome()
    {
        _player.transform.position = _homePos.position;
        _cameraManager.MainCamera.transform.position = _cameraManager.HomePos.position;
    }
}
