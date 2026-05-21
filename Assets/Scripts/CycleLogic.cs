using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[Serializable]
public class CycleLogic
{
    private List<Entity> _entities = new();
    [SerializeField] private Entity _currentEntity;

    [SerializeField] private Transform _entitySpawn;
    [SerializeField] private bool _isAutoWalk = false;
    [SerializeField] private bool _isAttacking = false;
    [SerializeField] private float _walkTime = 1f;

    private int _emptyWalkCount = 0;
    private int _loopCount = 0;

    private UniTaskCompletionSource<bool> _walkActionSource;
    private CancellationTokenSource _battleCTS;

    public void OnPlayerWalkAction()
    {
        _walkActionSource?.TrySetResult(true);
        _walkActionSource = null;
    }

    public async UniTask CycleTask(CancellationToken token, List<Item_SO> cycleOrder, List<Item_SO> fightOrder)
    {
        OnCycleCancel(token).Forget();

        while (true)
        {
            // no items
            if (cycleOrder.Count == 0)
            {
                OnEmptyWalk();
                await WalkTask();
                continue;
            }

            // have items
            foreach (var item in cycleOrder)
            {
                Entity newEntity = GambleMachine.RollEntity(item);
                _currentEntity = FactoryMachine.CreateEntity(newEntity, _entitySpawn.position);

                await WalkTask();

                GameManager.SanityUse?.Invoke(item.SanityUsage);
                GameManager.SanityUse?.Invoke(newEntity.Entity_SO.SanityUsage);

                if (_isAttacking && fightOrder.Count != 0)
                {
                    _battleCTS = new();
                    FightLogic.BattleOrderTask(_battleCTS.Token, fightOrder, newEntity).Forget();
                }

                await UniTask.WaitUntil(() => _currentEntity != null);
                _battleCTS?.Cancel();
                _battleCTS?.Dispose();
            }

            _loopCount += 1;
        }
    }

    private void OnEmptyWalk()
    {
        _emptyWalkCount += 1;
    }

    private async UniTask WalkTask()
    {
        if (_isAutoWalk)
        {
            await UniTask.Delay(1000);
        }
        else
        {

        }
    }

    private void Reset()
    {
        _emptyWalkCount = 0;
        _loopCount = 0;
        _currentEntity = null;

        _battleCTS?.Cancel();
        _battleCTS.Dispose();
    }

    //public async UniTask WalkActionTask(CancellationToken token)
    //{
    //    if (_isAutoWalk)
    //    {
    //        await UniTask.Delay(TimeSpan.FromSeconds(_walkTime), cancellationToken: token);
    //        return;
    //    }
        
    //    _walkActionSource = new UniTaskCompletionSource<bool>();
    //    await _walkActionSource.Task.AttachExternalCancellation(token);
    //    // TODO: wait for action to walk 
    //}

    public async UniTask OnCycleCancel(CancellationToken token)
    {
        await UniTask.WaitUntilCanceled(token);

        if (_currentEntity != null)
        {
            FactoryMachine.DestroyEntity(_currentEntity);
        }

        Reset();
        Debug.Log("[CycleLogic] -- Cancelled");
    }

    public UniTask CycleTick() { return UniTask.CompletedTask; }
}
