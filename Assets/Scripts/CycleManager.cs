/*
Если без монобеха то куда выводить Instatiate и Destroy?
делать для них отдельные методы в другом Монобехе? или как?
*/

using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class CycleManager : MonoBehaviour 
{
    public Entity CurrentEntity => _currentEntity;

    [SerializeField] private Entity _currentEntity;

    public void OnEntityDeath(EntityData entity)
    {
        foreach (var item in entity.DropResource.Keys)
        {
            var value = entity.DropResource[item];
            GameManager.Instance.Player.AddToInventory(item, value);

        }
    }

    // TODO: может возникнуть проблема когда у предмета в цикле слетели Entity и Unity зависает наху
    // TODO: если CycleOrder пуст - unity смерт
    public async UniTask CycleTick(CancellationToken token)
    {
        // зачем try catch

        OnCycleCancel(token).Forget();

        while (true)
        {
            await UniTask.Delay(1000); // TODO: чтобы намертво не замирало

            foreach (ItemData itemData in GameManager.Instance.Player.CycleOrder)
            {
                token.ThrowIfCancellationRequested();

                EntityData newEntity = GameManager.Instance.Gamble.RollEntity(itemData);

                if (newEntity == null || newEntity.EntityPrefab == null) // на плохой случай
                {
                    Debug.LogWarning(""); 
                    await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);
                    continue;
                }

                //await UniTask.Delay(TimeSpan.FromSeconds(1)); // промежуток на хотьбу

                if (_currentEntity != null) 
                    Destroy(_currentEntity.gameObject);

                _currentEntity = Instantiate(newEntity.EntityPrefab, LINKS.Instance.CM_EntitySpawnPos.position, LINKS.Instance.CM_EntitySpawnPos.rotation);
                _currentEntity.Init(newEntity);
                // setActive(true) // todo

                await UniTask.WaitWhile(() =>  _currentEntity != null, cancellationToken: token);
            }
        }
    }

    private async UniTask OnCycleCancel(CancellationToken token)
    {
        await UniTask.WaitUntilCanceled(token);
        // сбросить всё
        if (_currentEntity != null)
            Destroy(_currentEntity.gameObject);
    }

    public void MoveToCycle()
    {
        print("moved to cycle");
        GameManager.Instance.Player.transform.position = LINKS.Instance.CM_PlayerCyclePos.position;
        GameManager.Instance.CameraController.MainCamera.transform.position = GameManager.Instance.CameraController.CameraCyclePos;
    }
}
