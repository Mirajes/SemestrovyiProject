using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class HomeLogic
{
    private List<NPC> _ownedNPC = new();

    public async UniTask<Item_SO> FarmTask(Item_SO item)
    {
        await UniTask.Delay(10000);
        return item;
    }

    private void IsItemEnough()
    {

    }

    private void CraftItem()
    {

    }
}
