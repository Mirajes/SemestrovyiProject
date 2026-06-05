[System.Serializable]
public class MainInventory : A_Inventory
{
    public void ConvertInventoryToMain(ref A_Inventory inventory)
    {
        foreach (var item in inventory.Inventory.Keys)
        {
            AddItem(item, inventory.Inventory[item]);
        }
    }
}
