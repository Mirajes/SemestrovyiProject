using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class Loop
{
    [SerializeField] private static A_Entity _currentEntity;

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
    public static A_Entity CurrentEntity => _currentEntity; // STATIC <=
    
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

        if (_currentEntity != null)
        {
            FactoryMachine.DestroyEntity(_currentEntity);
            _currentEntity = null;
        }
    }

    private async UniTask LoopTask(CancellationToken coreToken)
    {
        var itemOrder = _playerData.LoopOrder;

        while (!coreToken.IsCancellationRequested)
        {
            await UniTask.Yield();

            if (itemOrder.Count == 0)
            {
                await EmptyWalkTask(coreToken);
                continue;
            }

            foreach (var item in itemOrder)
            {
                _fight_cts = new();
                SO_Entity entitySO = Gamble.RollEntity(item);

                if (entitySO == null) { Debug.LogWarning($"[Loop] - no entity in {item}"); continue; }
                _currentEntity = FactoryMachine.CreateEntity(entitySO, _entityTransformStart.position);

                if (_isAutoWalking)
                {
                    _currentEntity.MoveTo(_entityTransformEnd, 1f);
                    await WalkTask(coreToken);
                }
                else
                {
                    // ??? CompletionSource?
                }

                _entity_CS = new();

                GameManager.UseIQ?.Invoke(item.Roll_IQCost);
                _combatLogic.FightTask(_fight_cts.Token, _currentEntity).Forget();

                await _entity_CS.Task.AttachExternalCancellation(coreToken);
                _fight_cts?.Cancel();
                _fight_cts?.Dispose();
            }

            _countCompletedLoops++;
        }
    }

    private async UniTask EmptyWalkTask(CancellationToken token)
    {
        Debug.Log("[Loop] -- EmptyOrder");
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