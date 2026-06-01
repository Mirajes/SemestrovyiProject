using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField] private float _iqCapacity = 100;
    [SerializeField] private float _iqCurrent;

    [SerializeField] private PlayerState _playerState = PlayerState.InLoop;

    private List<A_Item> _loopOrder = new();
    private List<A_Item> _fightOrder = new();

    public Action<float> ChangedIQ;

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
