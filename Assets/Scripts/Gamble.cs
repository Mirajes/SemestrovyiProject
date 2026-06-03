using UnityEngine;

public class Gamble
{

    // maybe add UpgradeList here
    // weight System to future
    public static SO_Entity RollEntity(A_SO_Item item)
    {
        if (item.GambleList.Count == 0) { Debug.Log($"[Gamble] - no entity in list [{item}]"); return null; }

        float totalChance = 0;
        float currentChance = 0;

        foreach (var chance in item.GambleList.Values)
            totalChance += chance;

        float random = Random.Range(0, totalChance);

        foreach (var entity in item.GambleList.Keys)
        {
            float entityChance = item.GambleList[entity];
            currentChance += entityChance;

            if (random <= currentChance) 
                return entity;
        }

        Debug.Log($"[Gamble] - Issue with - [{item}]");
        return null;
    }
}
