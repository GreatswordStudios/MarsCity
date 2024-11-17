using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject greenhouseInventory;
    public GameObject solarInventory;
    public GameObject wasteInventory;
    public GameObject nuclearInventory;
    public GameObject miningInventory;
    public GameObject housingInventory;
    public GameObject researchInventory;

    public static InventoryManager singleton;

    void Start() {
        singleton = this;
    }


    public static void OpenPanel(string buildingType) {
        CloseAllPanel();

        switch(buildingType) {
            case "GREENHOUSE":
                InventoryManager.singleton.greenhouseInventory.SetActive(true);
                break;
            case "SOLAR":
                InventoryManager.singleton.solarInventory.SetActive(true);
                break;
            case "WASTE":
                InventoryManager.singleton.wasteInventory.SetActive(true);
                break;
            case "NUCLEAR":
                InventoryManager.singleton.nuclearInventory.SetActive(true);
                break;
            case "MINING":
                InventoryManager.singleton.miningInventory.SetActive(true);
                break;
            case "HOUSING":
                InventoryManager.singleton.housingInventory.SetActive(true);
                break;
            case "RESEARCH":
                InventoryManager.singleton.researchInventory.SetActive(true);
                break;
        }
    }

    public static void CloseAllPanel() {
        InventoryManager.singleton.greenhouseInventory.SetActive(false);
        InventoryManager.singleton.solarInventory.SetActive(false);
        InventoryManager.singleton.wasteInventory.SetActive(false);
        InventoryManager.singleton.nuclearInventory.SetActive(false);
        InventoryManager.singleton.miningInventory.SetActive(false);
        InventoryManager.singleton.housingInventory.SetActive(false);
        InventoryManager.singleton.researchInventory.SetActive(false);
    }
}
