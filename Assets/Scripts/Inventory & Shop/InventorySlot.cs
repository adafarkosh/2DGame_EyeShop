using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private ItemSO itemSO; // ENCAPSULATED PROPERTIES
    [SerializeField] private int quantity;

    public ItemSO ItemSO => itemSO;
    public int Quantity => quantity;


    public Image itemImage;
    public TMP_Text quantityText;


    private InventoryManager inventoryManager;


    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }

    // Unity calls this automatically when you click this UI object
    public void OnPointerClick(PointerEventData eventData)
    {
        // Ignore clicks if slot is empty
        if (itemSO == null || quantity <= 0) return;

    }

    public void UpdateUI()
    {
        // If we have an icon, show it
        if (itemSO != null) // Only tries to update the slot if its actually had an item
        {

            itemImage.sprite = itemSO.icon;
            itemImage.gameObject.SetActive(true); //If there is an item, turn on the slot's Image Game object
            quantityText.text = quantity.ToString();
        }

        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }
    public void SetItem(ItemSO newItem, int newQuantity) //ENCAPSULATED PROPERTY :)))
    {
        itemSO = newItem;
        quantity = newQuantity;
        UpdateUI();
    }

    public void AddQuantity(int amount)
    {
        quantity += amount;
        UpdateUI();
    }

    public void Clear()
    {
        itemSO = null;
        quantity = 0;
        UpdateUI();
    }
    public override string ToString() // TOSTRING
    {
        string itemName = itemSO != null ? itemSO.itemName : "Empty";
        return $"InventorySlot(Item={itemName}, Quantity={quantity})";
    }


}
