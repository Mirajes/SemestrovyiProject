using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private CursorManager _cursorManager;

    [Header("Player")]
    [SerializeField] private PlayerEntity _playerEntity;
    [SerializeField] private PlayerData _playerData = new();
    [Header("Loop")]
    [SerializeField] private Loop _loopLogic = new();
    [Header("Home")]
    [SerializeField] private Home _homeLogic = new();

    private SaveLogic _saveLogic = new();
    private InputHandler _inputHandler = new();

    public static Action<float> EarnIQ;
    public static Action<float> UseIQ;
    public static Action<PlayerState> ChangeState;

    [Header("States")]
    [SerializeField] private bool _isTutorialCompleted = false;
    //[SerializeField] private bool _isAFK = false;
    //[SerializeField] private bool _isInDialogue = false;

    private void Awake()
    {
        if (!_isTutorialCompleted)
        {
            _playerData.OnLauch();

        }
        #region Debug
        _uiManager.UpdateButton_debug(_playerData.PlayerState);
        #endregion

        _inputHandler.Init();
        _inputHandler.InitInputs(_cursorManager);
        _inputHandler.Inputs.Enable();

        _loopLogic.Start(_playerData);
    }

    private void OnEnable()
    {
        EarnIQ += _playerData.OnEarnIQ;
        UseIQ += _playerData.OnUseIQ;
        ChangeState += OnChangeState;
        ChangeState += _playerData.OnChangeState;
        ChangeState += _loopLogic.OnChangeState;
    }

    private void OnDisable()
    {
        EarnIQ -= _playerData.OnEarnIQ;
        UseIQ -= _playerData.OnUseIQ;
        ChangeState -= OnChangeState;
        ChangeState -= _playerData.OnChangeState;
        ChangeState -= _loopLogic.OnChangeState;
    }

    private void OnDestroy()
    {
        _inputHandler.RemoveInputs(_cursorManager);
        _inputHandler.Inputs?.Disable();
        _inputHandler.Inputs?.Dispose();
    }

    private void OnChangeState(PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.InLoop:
                _loopLogic.Start(_playerData);
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
