using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class Loop
{
    [SerializeField] private float _countCompletedLoops = 0;

    CancellationTokenSource _cts;

    public void Start()
    {
        _cts = new();

        _countCompletedLoops = 0;
        LoopTask(_cts.Token, new List<A_Item>()).Forget();
    }

    public void End()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private async UniTask LoopTask(CancellationToken token, List<A_Item> itemOrder)
    {
        while (true)
        {
            await UniTask.Yield(); // no lag pls

            if (itemOrder.Count == 0)
            {
                await WalkTask();
                continue;
            }

            foreach (var item in itemOrder)
            {
                GameManager.UseIQ?.Invoke(item.Data.IQCost);
            }

            _countCompletedLoops++;
        }
    }

    private UniTask WalkTask()
    {
        _countCompletedLoops++;
        return UniTask.CompletedTask;
    }

    private async UniTask FightTask(CancellationToken token) { }
}

[System.Serializable]
public class Fight
{

}
