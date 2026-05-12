using UnityEngine;

public class GambleMachine
{
    public static Entity RollEntity(Item_SO item)
    {
        if (item.EntityRoll.Count == 0) { Debug.Log("[GambleMachine] - No Entities"); return null; }

        float totalChance = 0, currentChance = 0;
        foreach (var chance in item.EntityRoll.Values)
            totalChance += chance;

        float random = Random.Range(0, totalChance);

        foreach (var entitySO in item.EntityRoll.Keys)
        {
            float entityChance = item.EntityRoll[entitySO];
            currentChance += entityChance;

            if (random <= currentChance)
                return entitySO.Entity;
        }

        Debug.Log("[GambleMachine] - No Entity");
        return null;
    }
}