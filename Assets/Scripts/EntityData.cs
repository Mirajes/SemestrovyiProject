using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/EntityData")]
public class EntityData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;

    [SerializedDictionary("Item to drop", "Count")]
    [SerializeField] private SerializedDictionary<ItemData, int> _dropResource;

    [SerializeField] private Entity _entityPrefab;
    [SerializeField] private EntityData _reversedEntity;

    private void OnValidate()
    {
        if (_reversedEntity == null) _reversedEntity = this;
    }

    private void OnEnable()
    {
        //_entityPrefab.Init(this);
    }

    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public Dictionary<ItemData, int> DropResource => _dropResource;
    public Entity EntityPrefab => _entityPrefab;
    public EntityData ReversedEntity => _reversedEntity;
}
