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
            _playerData.OnLaunch();

        }
        #region Debug
        _uiManager.UpdateButton_debug(_playerData.PlayerState);
        #endregion

        _loopLogic.OnLaunch(_playerData);

        _inputHandler.Init();
        _inputHandler.InitInputs(_cursorManager);
        _inputHandler.Inputs.Enable();

        _loopLogic.Start();
    }

    private void OnEnable()
    {
        EarnIQ += _playerData.OnEarnIQ;
        UseIQ += _playerData.OnUseIQ;
        ChangeState += _playerData.OnChangeState;
        ChangeState += _loopLogic.OnChangeState;
        ChangeState += OnChangeState;
    }

    private void OnDisable()
    {
        EarnIQ -= _playerData.OnEarnIQ;
        UseIQ -= _playerData.OnUseIQ;
        ChangeState -= _playerData.OnChangeState;
        ChangeState -= _loopLogic.OnChangeState;
        ChangeState -= OnChangeState;
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
                _loopLogic.Start();
                _playerEntity.MoveTo(_loopLogic.PlayerTransform, 0.3f);
                break;
            case PlayerState.InHome:
                _playerEntity.MoveTo(_homeLogic.PlayerTransform, 0.3f);
                break;
            default:
                Debug.Log($"[GameManager] - where are we [{newState}]");
                break;
        }
    }
}
