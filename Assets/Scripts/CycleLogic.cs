using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

public class CycleLogic
{
    private List<Entity> _entities = new();
    private Entity _currentEntity;

    public async UniTask CycleTick(CancellationToken token, List<Item_SO> cycleOrder)
    {
        while (true)
        {
            await UniTask.Delay(1000, cancellationToken: token);

            _entities.Add(FactoryManager.CreateEntity(new Entity()));
        }
    }
}
