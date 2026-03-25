using UnityEngine;

public class GambleLogic
{

    public EntityData RollEntity(ItemData item)
    {
        if (item.ChanceMaker.Count <= 0) { Debug.Log($"a kak rollit' {item}"); return null; }

        float totalChance = 0f;
        float random = Random.value;

        foreach (var itemData in item.ChanceMaker.Keys)
        {
            totalChance += item.ChanceMaker[itemData];
            if (random <= totalChance)
                return itemData;
        }

        return null;
    }
}