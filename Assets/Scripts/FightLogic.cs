using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

public class FightLogic
{
    public static async UniTask BattleOrderTask(CancellationToken token, List<Item_SO> fightOrder, Entity entity)
    {
        foreach (var item in fightOrder)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(item.ActivationCD));

            item.ItemLogic.Activate(entity);
            GameManager.SanityUse?.Invoke(item.SanityUsage);
        }
    }
}
