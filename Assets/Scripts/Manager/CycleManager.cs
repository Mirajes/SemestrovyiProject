using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class CycleManager : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPos;
    [SerializeField] private Transform _entitySpawnPos;

    private Entity _currentEntity;

    public Transform PlayerSpawnPos => _playerSpawnPos;
    public Transform EntitySpawnPos => _entitySpawnPos;
    public Entity CurrentEntity => _currentEntity;

    private async UniTask OnCycleCancel(CancellationToken token)
    {
        await UniTask.WaitUntilCanceled(token);
        GameManager.ReturnFromCycle?.Invoke(_currentEntity);
    }

    // TODO: может возникнуть проблема когда у предмета в цикле слетели EntityPrefab и Unity зависает наху
    // TODO: если CycleOrder пуст - unity смерт
    public async UniTask EntitySpawnTask(CancellationToken token)
    {
        var gameManager = GameManager.Instance;
        var cycleOrder = gameManager.Player.CycleOrder;

        OnCycleCancel(token).Forget();

        while (true)
        {
            if (cycleOrder.Count == 0)
            {
                Debug.Log("null");
                await UniTask.Delay(TimeSpan.FromSeconds(1)); // против зависаний
                continue;
            }

            foreach (var item in cycleOrder)
            {
                token.ThrowIfCancellationRequested();

                EntityData entityData = gameManager.Gamble.RollEntity(item);
                if (entityData == null || entityData.EntityPrefab == null) { Debug.LogWarning("no"); continue; }

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token); // walking

                _currentEntity = Instantiate(entityData.EntityPrefab, _entitySpawnPos.position, _entitySpawnPos.rotation);
                _currentEntity.Init(entityData);

                await UniTask.WaitWhile(() => _currentEntity != null, cancellationToken: token); // wait until kill
            }
        }
    }

    public async UniTask ExtractHelperTask(CancellationToken token) {
        OnCycleCancel(token).Forget();

        float cd = 3f;

        token.ThrowIfCancellationRequested();
        await UniTask.Delay(TimeSpan.FromSeconds(cd));
    }
}
