using UnityEngine;

public class FactoryMachine : MonoBehaviour
{
    public static Entity CreateEntity(Entity entityPrefab, Vector3 position)
    {
        Entity entity = Instantiate(entityPrefab, position, Quaternion.identity);
        return entity;
    }

    public static void DestroyEntity(Entity entity)
    {
        Destroy(entity.gameObject);
        Debug.Log("destroyed");
    }
}