using UnityEngine;

[CreateAssetMenu(fileName = "Item_SO", menuName = "Scriptable Objects/Item_SO")]
public class Item_SO : ScriptableObject
{
    public string Name => _name;
    public Sprite Icon => _icon;
    public int ID => _id;
    public Item_SO ReversedItem => _reversedItem;

    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;
    [SerializeField] private Item_SO _reversedItem;
}
