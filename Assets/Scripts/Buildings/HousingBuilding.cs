using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousingBuilding : Building
{
    public BuildingType GetBuildingType() {return BuildingType.HOUSING;}

    public void Tick() {
        Dictionary<string, float> drain = SceneMgr.gameDesignValues[BuildingType.HOUSING];
        SceneMgr sceneMgr = SceneMgr.singleton;

        if(sceneMgr.oxygen > drain["oxygen"] && sceneMgr.elec >= drain["elec"] && sceneMgr.biomass >= drain["biomass"] && sceneMgr.availableWorkforce >= drain["populationCost"]) {
            
            sceneMgr.water -= drain["water"];
            sceneMgr.oxygen -= drain["oxygen"];
            sceneMgr.biomass -= drain["biomass"];
            sceneMgr.elec -= drain["elec"];
            sceneMgr.buildingMats -= drain["buildingMats"];
            sceneMgr.waste -= drain["waste"];
            sceneMgr.availableWorkforce -= drain["populationCost"];
            return;
        }
    }
}
