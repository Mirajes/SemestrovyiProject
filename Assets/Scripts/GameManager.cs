using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class GameManager : A_Singleton<GameManager>
{
    // v drugoe mesto
    [Header("Sanity")]
    [SerializeField] private float _sanityCapacity_BASE = 100f;
    [SerializeField] private float _sanityRemaining;
    [SerializeField] private float _sanityRecoverySpeed = 3f;
    [SerializeField] private float _sanityRecoveryAmount = 10f;

    [Header("Core")]
    [SerializeField] private States _state;
    [SerializeField] private bool _inSleep;
    [SerializeField] private Player _player;
    [SerializeField] private CameraManager _cameraManager;

    [Header("Pos")]
    [SerializeField] private Transform _homePos;
    [SerializeField] private Transform _cyclePos;

    private PlayerData _playerData = new();
    private CycleLogic _cycleLogic = new();
    private HomeLogic _homeLogic = new();

    public PlayerData PlayerData => _playerData;

    public static Action<float> SanityUse;
    public static Action<States> StateChange;

    protected override void Awake()
    {
        base.Awake();

        StateChange += OnStateChange;
    }

    private void Start()
    {
        // init
        _sanityRemaining = _sanityCapacity_BASE;

    }

    private void OnDestroy()
    {
        StateChange -= OnStateChange;
    }

    private void OnStateChange(States state)
    {
        switch (state)
        {
            case States.Cycle:
                _player.transform.position = _cyclePos.position;
                _cameraManager.MainCamera.transform.position = _cameraManager.CyclePos.position;

                
                break;
            case States.Home:
                _player.transform.position = _homePos.position;
                _cameraManager.MainCamera.transform.position = _cameraManager.HomePos.position;


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
            await UniTask.WaitWhile(() => _sanityRemaining < _sanityCapacity_BASE);

            await UniTask.Delay(TimeSpan.FromSeconds(_sanityRecoverySpeed));
            _sanityRemaining += _sanityRecoveryAmount;
        }
    }

    #region Debug
    public void debug_ToHome() => OnStateChange(States.Home);
    public void debug_ToCycle() => OnStateChange(States.Cycle);
    #endregion
}
