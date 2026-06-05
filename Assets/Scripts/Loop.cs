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

    private PlayerData _playerData;
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
    //public static Action OnWalkCS;

    CancellationTokenSource _loop_cts;
    CancellationTokenSource _fight_cts;
    UniTaskCompletionSource _walk_CS;
    UniTaskCompletionSource _entity_CS; // CS - CompletionSource

    public void OnLaunch(PlayerData playerData)
    {
        _playerData = playerData;
        _combatLogic.OnLaunch(playerData);
    }

    public void Start()
    {
        OnEntityCS += OnTriggerEntityCS;

        _loop_cts = new();

        _countCompletedLoops = 0;
        LoopTask(_loop_cts.Token).Forget();
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

    private async UniTask LoopTask(CancellationToken coreToken)
    {
        var itemOrder = _playerData.LoopOrder;

        while (_playerData.PlayerState == PlayerState.InLoop)
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

                _fight_cts = new();

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
                _currentEntity.MoveTo(_entityTransformEnd, 1f);

                GameManager.UseIQ?.Invoke(item.Roll_IQCost);

                _combatLogic.FightTask(_fight_cts.Token, _currentEntity).Forget();

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
