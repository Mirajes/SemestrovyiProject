using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Main")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _count;
    public void AddItem(int amount)
    {
        _count += amount;
    }

    public void RemoveItem(int amount)
    {
        _count -= amount;
    }


    // SerializedDictionary
    [SerializedDictionary("EntityData", "Chance")]
    public SerializedDictionary<EntityData, float> _chanceMaker = new();

    [SerializedDictionary("EntityData", "IsCanChangeChance")]
    public SerializedDictionary<EntityData, bool> _isCanChanceBeChanged = new();

    private void OnValidate()
    {
        foreach (var entity in _chanceMaker.Keys)
        {
            if (_isCanChanceBeChanged.ContainsKey(entity)) continue;

            _isCanChanceBeChanged.Add(entity, false);
        }
    }
}