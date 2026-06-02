using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField] private float _iqCapacity = 100;
    [SerializeField] private float _iqCurrent;

    [SerializeField] private PlayerState _playerState = PlayerState.InLoop;

    // replaced A_Item => SO_Item cuz i cant debug it when abstract
    [SerializeField] private List<A_SO_Item> _loopOrder = new();
    [SerializeField] private List<A_SO_Item> _combatOrder = new();

    public Action<float> ChangedIQ;

    public List<A_SO_Item> LoopOrder => _loopOrder;
    public List<A_SO_Item> CombatOrder => _combatOrder;

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

    public void OnLauch()
    {
        _iqCurrent = _iqCapacity;
    }
}
