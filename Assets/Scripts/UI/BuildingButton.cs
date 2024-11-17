using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingButton : MonoBehaviour
{
    public BuildingType buildingType;
    public GameObject objectToSpawn;

    public void SpawnBuilding() {
        if(GridManager.singleton.carriedBuilding != null) {
            Destroy(GridManager.singleton.carriedBuilding);
        }
        GridManager.singleton.carriedBuilding = Instantiate(objectToSpawn);
        GridManager.singleton.carriedBuildingType = buildingType;
    }
}
