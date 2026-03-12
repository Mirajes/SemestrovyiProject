using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/EntityData")]
public class EntityData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Entity _entityPrefab;

    [SerializedDictionary("Item to drop", "Count")]
    [SerializeField] private SerializedDictionary<ItemData, int> _drop;

    public string Name => _name;
    public string Description => _description;
    public Entity Entity => _entityPrefab;


    private void OnEnable()
    {
        _entityPrefab.Init(this);
    }
}
