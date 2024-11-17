using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchBuilding : Building
{
    public BuildingType GetBuildingType() {return BuildingType.RESEARCH;}

    public void Tick() {
        Dictionary<string, float> drain = SceneMgr.gameDesignValues[BuildingType.RESEARCH];
        SceneMgr sceneMgr = SceneMgr.singleton;

        if(sceneMgr.oxygen > drain["oxygen"] && sceneMgr.availableWorkforce >= drain["populationCost"]) {
            
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
