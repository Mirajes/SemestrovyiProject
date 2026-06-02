using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;

    [Header("Player")]
    [SerializeField] private PlayerEntity _playerEntity;
    [SerializeField] private PlayerData _playerData = new();
    [Header("Loop")]
    [SerializeField] private Loop _loopLogic = new();
    [Header("Home")]
    [SerializeField] private Home _homeLogic = new();

    [SerializeField] private SaveLogic _saveLogic = new();

    public static Action<float> EarnIQ;
    public static Action<float> UseIQ;
    public static Action<PlayerState> ChangeState;

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
        #region Debug
        _uiManager.UpdateButton_debug(_playerData.PlayerState);
        #endregion
        _loopLogic.Start(_playerData);
    }

    private void OnEnable()
    {
        EarnIQ += _playerData.OnEarnIQ;
        UseIQ += _playerData.OnUseIQ;
        ChangeState += _playerData.OnChangeState;
        ChangeState += OnChangeState;
    }

    private void OnDisable()
    {
        EarnIQ -= _playerData.OnEarnIQ;
        UseIQ -= _playerData.OnUseIQ;
        ChangeState -= _playerData.OnChangeState;
        ChangeState -= OnChangeState;
    }

    private void OnChangeState(PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.InLoop:
                _playerEntity.MoveTo(_loopLogic.PlayerTransform);
                break;
            case PlayerState.InHome:
                _playerEntity.MoveTo(_homeLogic.PlayerTransform);
                break;
            default:
                Debug.Log($"[GameManager] - where are we [{newState}]");
                break;
        }
    }
}
