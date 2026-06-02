using UnityEngine;

public class Gamble
{
    public static A_Entity RollEntity(A_SO_Item item)
    {
        A_Entity entity = null;
        Debug.Log($"[Gabmle] - DEBUG SINGLE ITEM {item}");
        foreach (A_Entity debug_entity in item.GambleList.Keys)
        {
            entity = debug_entity;
        } 

        return entity;
    }
}
