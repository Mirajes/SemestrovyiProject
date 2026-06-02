using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData = new();

    [SerializeField] private Loop _loopLogic = new();
    [SerializeField] private Home _homeLogic = new();

    public static Action<float> EarnIQ;
    public static Action<float> UseIQ;

    [Header("States")]
    [SerializeField] private bool _isTutorialCompleted = false;
    [SerializeField] private bool _isAFK = false;
    [SerializeField] private bool _isInDialogue = false;

    private void Awake()
    {
        if (!_isTutorialCompleted)
        {
            _playerData.OnLauch();

        }

        _loopLogic.Start(_playerData);
    }

    private void OnEnable()
    {
        EarnIQ += _playerData.OnEarnIQ;
        UseIQ += _playerData.OnUseIQ;
    }

    private void OnDisable()
    {
        EarnIQ -= _playerData.OnEarnIQ;
        UseIQ -= _playerData.OnUseIQ;
    }
}

public class FabricMachine : MonoBehaviour
{

}