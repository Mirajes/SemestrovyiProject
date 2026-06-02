using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class Loop
{
    [SerializeField] private A_Entity _currentEntity;

    [SerializeField] private float _countCompletedLoops = 0;

    [Header("Poses")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _entityTransformStart;
    [SerializeField] private Transform _entityTransformEnd;

    public Transform PlayerTransform => _playerTransform;
    public Transform CameraTransform => _cameraTransform;

    CancellationTokenSource _cts;

    public void Start(PlayerData playerData)
    {
        _cts = new();

        _countCompletedLoops = 0;
        LoopTask(_cts.Token, playerData.LoopOrder).Forget();
    }

    public void End()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private async UniTask LoopTask(CancellationToken token, List<A_SO_Item> itemOrder)
    {
        while (true)
        {
            await UniTask.Yield(); // no lag pls
            token.ThrowIfCancellationRequested();

            if (itemOrder.Count == 0)
            {
                Debug.Log("[Loop] - EmptyOrder");
                await EmptyWalkTask();
                continue;
            }

            foreach (var item in itemOrder)
            {
                await WalkTask();
                GameManager.UseIQ?.Invoke(item.Roll_IQCost);

                _currentEntity = Gamble.RollEntity(item);
            }

            _countCompletedLoops++;
        }
    }

    private async UniTask EmptyWalkTask()
    {
        await UniTask.WaitForSeconds(1);
        _countCompletedLoops++;
    }

    private async UniTask WalkTask()
    {
        await UniTask.WaitForSeconds(1);
    }

    private async UniTask FightTask(CancellationToken token)
    {
        await UniTask.CompletedTask;
    }
}

[System.Serializable]
public class Fight
{

}
