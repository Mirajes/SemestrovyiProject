using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SO", menuName = "Scriptable Objects/Item_SO")]
public class Item_SO : ScriptableObject
{
    public string Name => _name;
    public Sprite Sprite => _sprite;
    public int ID => _id;
    public Item_SO ReversedItem => _reversedItem;
    public SerializedDictionary<Entity_SO, float> EntityRoll => _entityRoll;

    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _id;
    [SerializeField] private Item_SO _reversedItem;

    // gamble list
    [SerializeField]
    [SerializedDictionary()]
    SerializedDictionary<Entity_SO, float> _entityRoll;
}
