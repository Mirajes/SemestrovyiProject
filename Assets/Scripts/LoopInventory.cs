[System.Serializable]
public class LoopInventory : A_Inventory
{
    public void GetItemFromInventory(ref A_Inventory inventory, A_SO_Item item, int count)
    {
        AddItem(item, count);
        inventory.RemoveItem(item, count);
    }
}