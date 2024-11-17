using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerBuilding: Building
{
    public BuildingType GetBuildingType() {return BuildingType.MINER;}

    public void Tick() {
        Dictionary<string, float> drain = SceneMgr.gameDesignValues[BuildingType.MINER];
        SceneMgr sceneMgr = SceneMgr.singleton;

        if(sceneMgr.elec >= drain["elec"]  && sceneMgr.availableWorkforce >= drain['populationCost']) {
            
            sceneMgr.water -= drain["water"];
            sceneMgr.oxygen -= drain["oxygen"];
            sceneMgr.biomass -= drain["biomass"];
            sceneMgr.elec -= drain["elec"];
            sceneMgr.buildingMats -= drain["buildingMats"];
            sceneMgr.waste -= drain["waste"];
            sceneMgr.availableWorkforce -= drain['populationCost'];
            return;
        }


    }
}
