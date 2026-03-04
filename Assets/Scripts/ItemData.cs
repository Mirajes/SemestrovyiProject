using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public int Count => _count;

    [Header("Main")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;
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

        //float totalChance = 0f;
        //int changeableCount = _chanceMaker.Count;

        //foreach (var entity in _chanceMaker.Keys)
        //{
        //    if (_isCanChanceBeChanged[entity]) { changeableCount--; continue; }

        //    totalChance += _chanceMaker[entity];
        //}

        //foreach (var item in _chanceMaker.Keys)
        //{
        //    if (_isCanChanceBeChanged[item]) continue;

        //    _chanceMaker[item] = totalChance / changeableCount;
        //}
    }
}