using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearBuilding : Building
{
    public BuildingType GetBuildingType() {return BuildingType.NUCLEAR;}

    public void Tick() {
        Dictionary<string, float> drain = SceneMgr.gameDesignValues[BuildingType.NUCLEAR];
        SceneMgr sceneMgr = SceneMgr.singleton;

        if(sceneMgr.water > drain["water"] && sceneMgr.availableWorkforce >= drain['populationCost']) {
            
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
