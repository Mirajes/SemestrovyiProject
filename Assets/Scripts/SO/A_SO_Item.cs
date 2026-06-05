using AYellowpaper.SerializedCollections;
using UnityEngine;

public abstract class A_SO_Item : ScriptableObject
{
    [Header("About")]
    [SerializeField] protected string _Name;
    [SerializeField] protected Sprite _Sprite;

    [Header("Gamble")]
    [SerializeField] protected float _Roll_IQCost;
    [SerializedDictionary("Entity", "Chance")] [SerializeField]
    private SerializedDictionary<SO_Entity, float> _gambleList;

    [Header("Combat")]
    [SerializeField] protected float _Combat_IQCost;
    [SerializeField] protected float _FillingTime;
    [SerializeField] protected A_Entity _CurrentEntity;

    public string Name => _Name;
    public Sprite Sprite => _Sprite;

    public float Roll_IQCost => _Roll_IQCost;
    public SerializedDictionary<SO_Entity, float> GambleList => _gambleList;
    public float Combat_IQCost => _Combat_IQCost;
    public float FillingTime => _FillingTime;

    private void OnEntitySpawn(A_Entity entity)
    {
        _CurrentEntity = entity;
    }

    private void OnEnable()
    {
        Loop.EntitySpawn += OnEntitySpawn;
    }

    private void OnDisable()
    {
        Loop.EntitySpawn -= OnEntitySpawn;
    }

    public virtual void Use()
    {
        Debug.Log($"[Item] -- Base {this}");
    }


}
