using UnityEngine;

public class FactoryMachine : MonoBehaviour
{
    public static A_Entity CreateEntity(SO_Entity entityData, Vector3 position)
    {
        A_Entity newEntity = Instantiate(entityData.EntityObj, position, Quaternion.identity);
        newEntity.Spawn();

        return newEntity;
    }

    public static void DestroyEntity(A_Entity entity)
    {
        Destroy(entity.gameObject);
    }
}