using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/EntityData")]
public class EntityData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private float _baseHP = 1f;
    [SerializeField] private Entity _entityPrefab;
    [SerializeField] private bool _isNPC = false;

    [SerializedDictionary("Item to drop", "Count")]
    [SerializeField] private SerializedDictionary<ItemData, int> _drop;

    public string Name => _name;
    public string Description => _description;
    public float BaseHP => _baseHP;
    public Entity EntityPrefab => _entityPrefab;
    public bool IsNPC => _isNPC;
    public SerializedDictionary<ItemData, int> Drop => _drop;
}
