using UnityEngine;

public class Entity : MonoBehaviour
{
    private EntityData _entityData;

    public void Init(EntityData data)
    {
        _entityData = data;
    } 
}