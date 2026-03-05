using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public int Count => _count;
    public bool IsCanBeCrafted => _isCanBeCrafted;

    [Header("Main")]
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _count;
    [SerializeField] private bool _isCanBeCrafted;

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
    public SerializedDictionary<EntityData, float> ChanceMaker = new();

    [SerializedDictionary("EntityData", "IsCanChangeChance")]
    public SerializedDictionary<EntityData, bool> IsCanChanceBeChanged = new();

    [SerializedDictionary("ItemResource", "RequiredCount")]
    public SerializedDictionary<ItemData, int> ItemReceipt = new();

    //private void OnValidate()
    //{
    //    if (!_isCanBeCrafted) // очистка рецепта если нельзя скрафтить
    //    {
    //        ItemReceipt.Clear();
    //    }

    //    //foreach (var entity in ChanceMaker.Keys)
    //    //{
    //    //    if (IsCanChanceBeChanged.ContainsKey(entity)) continue;

    //    //    IsCanChanceBeChanged.Add(entity, false);
    //    //}

    //    //float totalChance = 0f;
    //    //int changeableCount = ChanceMaker.Count;

    //    //foreach (var entity in ChanceMaker.Keys)
    //    //{
    //    //    if (IsCanChanceBeChanged[entity]) { changeableCount--; continue; }

    //    //    totalChance += ChanceMaker[entity];
    //    //}

    //    //foreach (var item in ChanceMaker.Keys)
    //    //{
    //    //    if (IsCanChanceBeChanged[item]) continue;

    //    //    ChanceMaker[item] = totalChance / changeableCount;
    //    //}
    //}
}