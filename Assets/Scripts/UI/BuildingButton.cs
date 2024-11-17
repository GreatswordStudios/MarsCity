using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingButton : MonoBehaviour
{
    public BuildingType buildingType;

    public void SpawnBuilding() {
        GridManager.singleton.CarryBuilding(buildingType);
    }
}
