public class CycleInventory : A_Inventory 
{
    public void ConvertCycleToMain(ref MainInventory mainInventory)
    {
        foreach (var item in _Inventory.Keys)
        {
            mainInventory.AddToInventory(item, _Inventory[item]);
        }
    }
}

//public void ModifyInventory(ref SerializedDictionary<Item_SO, int> inventory, Item_SO item, int count)
//{
//    Debug.Log($"modified {item} - [{count}] to {inventory}");
//    inventory[item] += count;
//}