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

    [SerializeField] private States _state;
    [SerializeField] private bool _inSleep;

    [SerializeField] private Player _player;

    private PlayerData _playerData = new();
    private CycleLogic _cycleLogic = new();
    private HomeLogic _homeLogic = new();

    public PlayerData PlayerData => _playerData;

    public static Action<float> SanityUse;

    protected override void Awake()
    {
        base.Awake();

        
    }

    private void Start()
    {
        // init
        _sanityRemaining = _sanityCapacity_BASE;

    }

    private void OnSanityUse(float amount)
    {
        _sanityRemaining -= amount;
    }

    private async UniTask SanityRecoveryTask()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_sanityRecoverySpeed));
        _sanityRemaining += _sanityRecoveryAmount;
    }
}