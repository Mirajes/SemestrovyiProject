using UnityEngine;

public class GambleLogic
{
    public EntityData RollEntity(ItemData item)
    {
        if (item.ChanceMaker.Count <= 0) { Debug.Log($"a kak rollit' {item}"); return null; }

        float random = Random.value;
        float totalChance = 0;

        foreach (var itemData in item.ChanceMaker.Keys)
        {
            totalChance += item.ChanceMaker[itemData];
            if (random < totalChance)
                return itemData;
            else
                totalChance += item.ChanceMaker[itemData];
        }

        return null;
    }
}