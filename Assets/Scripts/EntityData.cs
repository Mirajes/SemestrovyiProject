using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Objects/EntityData")]
public class EntityData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;

    [SerializeField] private List<ItemData> _dropResource;

    [SerializeField] private GameObject _entityPrefab;
    [SerializeField] private EntityData _reversedEntity;
}
