using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

public class CycleLogic
{
    List<Entity> _entities = new();

    public async UniTask CycleTick(CancellationToken token, List<Item_SO> itemFlow)
    {
        while (true)
        {
            await UniTask.Delay(1000, cancellationToken: token);

            _entities.Add(FactoryManager.CreateEntity(new Entity()));
        }
    }
}
