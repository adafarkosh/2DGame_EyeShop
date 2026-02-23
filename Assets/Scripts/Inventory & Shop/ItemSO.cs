//tutorial https://www.youtube.com/watch?v=FqeaEzot3_4&list=PLSR2vNOypvs7sV_ks7h42F7hZ7DmGJqU6&index=2
using UnityEngine;

[CreateAssetMenu(fileName = "New Item")]

public class ItemSO : ScriptableObject
{

    public string itemName;
    [TextArea] public string itemDescription; //[TextArea] makes a field in Insp bigger
    public Sprite icon;

    public bool isGold;
    //public int stackSize = 3; // Allows to custumize the stack size for each item independantly. IF IN THE FUTURE stock is limited -> work in Inventorymanager.cs as well
}
