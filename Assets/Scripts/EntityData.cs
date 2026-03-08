using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/EntityData")]
public class EntityData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;

    [SerializeField] private List<ItemData> _dropResource; // поменять на Dictionary<ItemData, int> для указания количества ресурсов в дропе

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
    public List<ItemData> DropResource => _dropResource;
    public Entity EntityPrefab => _entityPrefab;
    public EntityData ReversedEntity => _reversedEntity;
}
