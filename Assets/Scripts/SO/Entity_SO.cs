using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity_SO", menuName = "Scriptable Objects/Entity_SO")]
public class Entity_SO : ScriptableObject
{
    [SerializeField]
    [SerializedDictionary("Item", "Count")] private SerializedDictionary<Item_SO, int> _itemDrop;
    [SerializeField] private Entity _entity;

    public SerializedDictionary<Item_SO, int> ItemDrop => _itemDrop;
    public Entity Entity => _entity;
}
