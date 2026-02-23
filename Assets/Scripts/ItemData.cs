using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;

    [SerializeField] private int _count;
    [SerializeField] private Entity _entity;
    [SerializeField] private Entity _reversedEntity;
    //private int _index;

    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public int Count => _count;
    public Entity Entity => _entity;
    public Entity ReversedEntity => _reversedEntity;

    private void OnEnable()
    {
        if (_reversedEntity == null) _reversedEntity = _entity;
    }
}
