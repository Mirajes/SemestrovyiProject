using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SO", menuName = "Scriptable Objects/Item_SO")]
public class Item_SO : ScriptableObject
{
    [Header("Main")]
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Item _itemFightLogic;
    [SerializeField] private Item_SO _reversedItem;

    [Header("Properties")]
    [SerializeField] private int _id;
    [SerializeField] private Tier _tier; // ?
    [SerializedDictionary("EntityData", "Chance")][SerializeField] private SerializedDictionary<Entity_SO, float> _entityRoll;

    // TODO: move to another script
    [Header("Activate")]
    [SerializeField] private float _sanityUsage;
    [SerializeField] private float _activationCD;

    public string Name => _name;
    public Sprite Sprite => _sprite;
    public Item ItemLogic => _itemFightLogic;
    public Item_SO ReversedItem => _reversedItem;
    public int Id => _id;
    public Tier Tier => _tier;
    public float SanityUsage => _sanityUsage;
    public float ActivationCD => _activationCD;
    public SerializedDictionary<Entity_SO, float> EntityRoll => _entityRoll;
}
