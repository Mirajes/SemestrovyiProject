using UnityEngine;

public class GambleManager
{
    public EntityData RollEntity(ItemData item)
    {
        float random = Random.value;
        float totalChance = 0;

        foreach (var itemData in item.ChanceMaker.Keys)
        {
            totalChance += item.ChanceMaker[itemData];
            if (random <  totalChance)
                return itemData;
            else
                totalChance += item.ChanceMaker[itemData];
        }

        return null;
    }

    public void RollEvent() { }
    public void RollUnexpected() { }
    public void RollError() { }
}
