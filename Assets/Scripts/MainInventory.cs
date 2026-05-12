public class MainInventory : A_Inventory
{
    public void GetItemsFromCycle(ref CycleInventory cycleInventory, Item_SO keyItem) { }
}

//public void ModifyInventory(ref SerializedDictionary<Item_SO, int> inventory, Item_SO item, int count)
//{
//    Debug.Log($"modified {item} - [{count}] to {inventory}");
//    inventory[item] += count;
//}