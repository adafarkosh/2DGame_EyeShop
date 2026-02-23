using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] itemSlots;

    public int gold;
    public TMP_Text goldText;

    public GameObject lootPrefab;
    public Transform player;


    private void Start()
    {
        // Refresh every slot at game start so UI matches data
        foreach (var slot in itemSlots)
            slot.UpdateUI();
    }

    private void OnEnable()
    {
        // Subscribe to loot event (when loot is collected, this method will run)
        Loot.OnItemLooted += AddItem;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid duplicate calls / memory leaks
        Loot.OnItemLooted -= AddItem;
    }

    public void AddItem(ItemSO itemSO, int quantity)
    {
        InventoryEntry entry = new InventoryEntry(itemSO, quantity);
        Debug.Log(entry.ToString()); // it logs for every pickup= constructor + ToString() usage here

        // 1) Gold is not stored in slots (later I want NPC to but with gold items looted = different category & behaves differently).
        if (itemSO.isGold)
        {
            gold += quantity;
            if (goldText != null) goldText.text = gold.ToString();
            return;
        }

        // 2) If item already exists in a slot, STACK it there
        for (int i = 0; i < itemSlots.Length; i++)
        {
            InventorySlot slot = itemSlots[i];

            if (slot.ItemSO == itemSO)
            {
                slot.AddQuantity(quantity); 
                return;
            }
        }

        // 3) Otherwise, put into FIRST empty slot
        for (int i = 0; i < itemSlots.Length; i++)
        {
            InventorySlot slot = itemSlots[i];

            if (slot.ItemSO == null)
            {
                slot.SetItem(itemSO, quantity); // IMPORTANT: use method
                return; // IMPORTANT: stop after placing
            }
        }


    

}
    /// LATER I might add "Drop loot", now it didn't work 
        //public void DropItem(InventorySlot slot) // 
        //{
        //    DropLoot(slot.itemSO, 1);
        //    slot.quantity--;
        //    if (slot.quantity <= 0)
        //    {
        //        slot.itemSO = null;
        //    }
        //    slot.UpdateUI();
        //}

    // 4) If we get here, no empty slot exists
    //private void DropLoot(ItemSO itemSO, int quantity) {

    //    if (lootPrefab == null || player == null)
    //    {
    //        Debug.LogWarning("[Inventory] DropLoot failed: assign lootPrefab + player in Inspector.");
    //        return;
    //    }

    //    Loot loot = Instantiate(lootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();

    //    // Code2 assumes Loot has Initialize().
    //    loot.Initialize(itemSO, quantity);
    

}

