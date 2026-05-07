using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public static Entity CreateEntity(Entity entityPrefab)
    {
        Entity entity = Instantiate(entityPrefab);
        return entity;
    }
}