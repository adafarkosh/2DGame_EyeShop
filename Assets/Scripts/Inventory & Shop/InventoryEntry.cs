// constructor + encapsulated properties + ToString HERE :))))))))
using System;

[Serializable]
public class InventoryEntry
{
    public ItemSO Item { get; private set; }
    public int Quantity { get; private set; }

    public InventoryEntry(ItemSO item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public void Add(int amount)
    {
        Quantity += amount;
    }

    public void Remove(int amount)
    {
        Quantity -= amount;
        if (Quantity < 0) Quantity = 0;
    }

    public override string ToString()
    {
        string itemName = Item != null ? Item.itemName : "None";
        return $"InventoryEntry(Item={itemName}, Quantity={Quantity})";
    }
}


