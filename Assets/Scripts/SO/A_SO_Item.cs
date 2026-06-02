using UnityEngine;

public abstract class A_SO_Item : ScriptableObject
{
    [Header("Gamble")]
    [SerializeField] protected float _Roll_IQCost;

    [Header("Combat")]
    [SerializeField] protected float _Combat_IQCost;
    [SerializeField] protected float _Damage;
    //list?


    public float Roll_IQCost => _Roll_IQCost;
    public float Combat_IQCost => _Combat_IQCost;

    public abstract void Use(A_Entity entity);
}
