using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenhouseBuilding: Building
{
    public BuildingType GetBuildingType() {return BuildingType.GREENHOUSE;}

    public void Tick() {
        Dictionary<string, float> drain = SceneMgr.gameDesignValues[BuildingType.GREENHOUSE];
        SceneMgr sceneMgr = SceneMgr.singleton;

        if(sceneMgr.water > drain["water"] && sceneMgr.elec >= drain["elec"]  && sceneMgr.waste >= drain["waste"] && sceneMgr.availableWorkforce >= drain["populationCost"]) {
            
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
