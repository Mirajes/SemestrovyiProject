using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[System.Serializable]
public class Fight
{

    /*
    plus na buduschee. Dlya togo chtobi mozhono bilo menyat' order vo vremya zabega
    neobhodimo brat' iz obschego dostupa PlayerData
    poka eto tol'ko copiya
    */
    public async UniTask FightTask(CancellationToken token, A_Entity entity, List<A_SO_Item> combatOrder)
    {
        while (true)
        {
            await UniTask.Yield(token);
            if (token.IsCancellationRequested || !entity)
                break;

            foreach (var item in combatOrder)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(item.FillingTime), cancellationToken: token);
                item.Use(entity); // eto gavno potomushto esli buget [Player => heal] pridetca iz drugovo mesta brat'
            }
        }
    }
}
