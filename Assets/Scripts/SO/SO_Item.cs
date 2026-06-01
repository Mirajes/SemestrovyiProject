using UnityEngine;

[CreateAssetMenu(fileName = "Item_SO", menuName = "Scriptable Objects/Item_SO")]
public class SO_Item : ScriptableObject
{
    [SerializeField] private float _iqCost;
    [SerializeField] private float _damage;

    public float IQCost => _iqCost;
    public float Damage => _damage;
}