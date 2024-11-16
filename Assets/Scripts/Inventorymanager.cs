using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryPanel; // Reference to the panel
    public GameObject inventorySlotPrefab; // Prefab for each slot
    public List<Sprite> itemSprites; // List of item icons
    
    private List<GameObject> inventorySlots = new List<GameObject>();

    void Start()
    {
        PopulateInventory();
    }

    void PopulateInventory()
    {
        foreach (Sprite itemSprite in itemSprites)
        {
            // Instantiate a new slot
            GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryPanel.transform);

            // Set the item's image
            newSlot.GetComponent<Image>().sprite = itemSprite;

            // Add the slot to the list
            inventorySlots.Add(newSlot);
        }
    }
}
