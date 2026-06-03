using AYellowpaper.SerializedCollections;
using UnityEngine;

public abstract class A_SO_Item : ScriptableObject
{
    [Header("Gamble")]
    [SerializeField] protected float _Roll_IQCost;
    [SerializedDictionary("Entity", "Chance")] [SerializeField]
    private SerializedDictionary<SO_Entity, float> _gambleList;

    [Header("Combat")]
    [SerializeField] protected float _Combat_IQCost;
    [SerializeField] protected float _FillingTime;

    public float Roll_IQCost => _Roll_IQCost;
    public SerializedDictionary<SO_Entity, float> GambleList => _gambleList;
    public float Combat_IQCost => _Combat_IQCost;
    public float FillingTime => _FillingTime;

    public abstract void Use(A_Entity entity);
}
