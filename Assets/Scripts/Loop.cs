using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class Loop
{
    [SerializeField] private A_Entity _currentEntity;

    [SerializeField] private float _countCompletedLoops = 0;
    [SerializeField] private Fight _combatLogic = new();

    [SerializeField] private bool _isAutoWalking = false;
    [SerializeField] private bool _isAttacking = false;

    [Header("Poses")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _entityTransformStart;
    [SerializeField] private Transform _entityTransformEnd;

    public Transform PlayerTransform => _playerTransform;
    public Transform CameraTransform => _cameraTransform;

    public static Action OnEntityCS;

    CancellationTokenSource _loop_cts;
    CancellationTokenSource _fight_cts;
    UniTaskCompletionSource _walk_CS;
    UniTaskCompletionSource _entity_CS; // CS - CompletionSource

    public void Start(PlayerData playerData)
    {
        OnEntityCS += OnTriggerEntityCS;

        _loop_cts = new();

        _countCompletedLoops = 0;
        LoopTask(_loop_cts.Token, playerData.LoopOrder, playerData.CombatOrder).Forget();
    }

    public void End()
    {
         OnEntityCS -= OnTriggerEntityCS;

        _loop_cts?.Cancel();
        _loop_cts?.Dispose();

        _fight_cts?.Cancel();
        _fight_cts?.Dispose();

        FactoryMachine.DestroyEntity(_currentEntity);
        _currentEntity = null;
    }

    private async UniTask LoopTask(CancellationToken coreToken, List<A_SO_Item> itemOrder, List<A_SO_Item> combatOrder)
    {
        while (true)
        {
            await UniTask.Yield(); // no lag pls
            if (coreToken.IsCancellationRequested)
                break;
            //coreToken.ThrowIfCancellationRequested();

            if (itemOrder.Count == 0)
            {
                Debug.Log("[Loop] - EmptyOrder");
                await EmptyWalkTask(coreToken);
                continue;
            }

            foreach (var item in itemOrder)
            {
                if (coreToken.IsCancellationRequested)
                    break;
                //coreToken.ThrowIfCancellationRequested();

                SO_Entity entityData = Gamble.RollEntity(item);
                _currentEntity = FactoryMachine.CreateEntity(entityData, _entityTransformStart.position);
                
                if (_isAutoWalking)
                {
                    await WalkTask(coreToken);
                }
                else
                {
                    _walk_CS = new();
                    await _walk_CS.Task.AttachExternalCancellation(coreToken);
                }
                _currentEntity.MoveTo(_entityTransformEnd);

                GameManager.UseIQ?.Invoke(item.Roll_IQCost);

                _fight_cts = new();
                _combatLogic.FightTask(_fight_cts.Token, _currentEntity, combatOrder).Forget();

                _entity_CS = new();
                await _entity_CS.Task.AttachExternalCancellation(coreToken);

                _fight_cts?.Cancel();
                _fight_cts?.Dispose();
            }

            _countCompletedLoops++;
        }
    }

    private async UniTask EmptyWalkTask(CancellationToken token)
    {
        await UniTask.WaitForSeconds(1, cancellationToken: token);
        _countCompletedLoops++;
    }

    private async UniTask WalkTask(CancellationToken token)
    {
        await UniTask.WaitForSeconds(1, cancellationToken: token);
    }

    private void OnTriggerEntityCS()
    {
        _entity_CS?.TrySetResult();
        _entity_CS = null;
    }

    private void OnTriggerWalkCS()
    {
        _walk_CS?.TrySetResult();
        _walk_CS = null;
    }

    public void OnChangeState(PlayerState state)
    {
        if (state == PlayerState.InLoop) return;

        Debug.Log($"[Loop] - Cancelled");
        End();        
    }
}
