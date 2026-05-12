using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CycleLogic
{
    private List<Entity> _entities = new();
    private Entity _currentEntity;

    public async UniTask CycleTask(CancellationToken token, List<Item_SO> cycleOrder)
    {
        OnCycleCancel(token).Forget();

        while (true)
        {
            await UniTask.Delay(1000, cancellationToken: token);

            if (cycleOrder.Count == 0)
            {
                Debug.Log("[CycleLogic] - Empty Order");
                continue;
            }

            foreach (var item in cycleOrder)
            {
                if (_currentEntity == null) // stupid
                {
                    Entity newEntity = GambleMachine.RollEntity(item);

                    _entities.Add(FactoryMachine.CreateEntity(newEntity, Vector3.zero));
                    _currentEntity = _entities[0];
                }

                await UniTask.WaitUntil(() => _currentEntity != null);
            }
        }
    }

    public async UniTask OnCycleCancel(CancellationToken token)
    {
        await UniTask.WaitUntilCanceled(token);
        FactoryMachine.DestroyEntity(_currentEntity);


        Debug.Log("[CycleLogic] -- Cancelled");
    }

    public UniTask CycleTick() { return UniTask.CompletedTask; }
}
