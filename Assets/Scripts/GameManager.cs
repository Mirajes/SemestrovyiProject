using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<ItemData> _itemDatas;

    private void Awake()
    {
        _itemDatas = Resources.LoadAll<ItemData>("Items").ToList();
        
    }

    private void Start()
    {

    }
}

public class StateManager : MonoBehaviour
{
    public void MoveToCycle()
    {

    }
}

public class CycleLogic
{
    public async UniTask OnCycleCancel(CancellationToken token)
    {
        await UniTask.WaitUntilCanceled(token);
        // throw reset event
    }

    // TODO: может возникнуть проблема когда у предмета в цикле слетели Entity и Unity зависает наху
    // TODO: если CycleOrder пуст - unity смерт
    public async UniTask EntitySpawnTask(CancellationToken token)
    {
        OnCycleCancel(token).Forget();

        while (true)
        {
            await UniTask.Delay(1000); // против зависаний


        }
    }

    public async UniTask ExtractHelperTask(CancellationToken token) { }
}

public class HomeLogic
{

}

public class GameUI : MonoBehaviour
{

}