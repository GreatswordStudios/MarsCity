using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class genericInvButton : MonoBehaviour
{
    public GameObject itemPrefab; // Prefab of the item to spawn
    public Transform spawnLocation; // Location to spawn the item in the game
    public Button itemButton; // Reference to the button

    void Start()
    {
        // Ensure the button has an onClick listener
        if (itemButton != null)
        {
            itemButton.onClick.AddListener(SpawnItem);
        }
        else
        {
            Debug.LogWarning("Item button is not assigned!");
        }
    }

    void SpawnItem()
    {
        if (itemPrefab != null && spawnLocation != null)
        {
            // Instantiate the item at the specified spawn location
            Instantiate(itemPrefab, spawnLocation.position, spawnLocation.rotation);
            Debug.Log($"{itemPrefab.name} has been added to the game!");
        }
        else
        {
            Debug.LogWarning("Item prefab or spawn location is not assigned!");
        }
    }
}



