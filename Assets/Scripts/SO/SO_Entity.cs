using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity_SO", menuName = "Scriptable Objects/Entity_SO")]
public class SO_Entity : ScriptableObject
{
    [SerializeField] private A_Entity _entityObj;
    [SerializeField] private float _HEALTH_BASE;

    [SerializedDictionary("Item", "Count")] [SerializeField]
    private SerializedDictionary<A_SO_Item, int> _drop = new();

    public A_Entity EntityObj => _entityObj;
    public float BaseHealth => _HEALTH_BASE;
    public SerializedDictionary<A_SO_Item, int> Drop => _drop;
}