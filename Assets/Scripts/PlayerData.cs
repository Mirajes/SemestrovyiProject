using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField] private float _iqCapacity = 100;
    [SerializeField] private float _iqCurrent;

    [SerializeField] private PlayerState _playerState = PlayerState.InLoop;

    [SerializeField] private List<A_SO_Item> _loopOrder = new();
    [SerializeField] private List<A_SO_Item> _combatOrder = new();

    public static event Action<float> ChangedIQ;
    public static event Action<PlayerState> ChangedPlayerState;

    public List<A_SO_Item> LoopOrder => _loopOrder;
    public List<A_SO_Item> CombatOrder => _combatOrder;
    public PlayerState PlayerState => _playerState;

    public void OnLaunch()
    {
        _iqCurrent = _iqCapacity;
    }

    public void OnEarnIQ(float iqAmount)
    {
        _iqCurrent += iqAmount;

        ChangedIQ?.Invoke(_iqCurrent);
    }

    public void OnUseIQ(float iqAmount)
    {
        _iqCurrent -= iqAmount;

        ChangedIQ?.Invoke(_iqCurrent);
    }

    public void OnChangeState(PlayerState state)
    {
        _playerState = state;

        ChangedPlayerState?.Invoke(_playerState);
    }
}