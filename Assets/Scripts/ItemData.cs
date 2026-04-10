using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private bool _isCanBeCrafted;
    [SerializeField] private List<e_ItemClass> _itemClasses;

    [SerializedDictionary("Enitity to spawn", "Chance")]
    [SerializeField] private SerializedDictionary<EntityData, float> _chanceMaker;

    [SerializedDictionary("Item in receipt", "Count")]
    [SerializeField] private SerializedDictionary<ItemData, int> _itemReceipt;

    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public bool IsCanBeCrafted => _isCanBeCrafted;
    public List<e_ItemClass> ItemClasses => _itemClasses;
    public SerializedDictionary<EntityData, float> ChanceMaker => _chanceMaker;
    public SerializedDictionary<ItemData, int> ItemReceipt => _itemReceipt;
}
