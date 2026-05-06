using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private Entity_SO _entity_SO;

    public Entity_SO Entity_SO => _entity_SO;
}
