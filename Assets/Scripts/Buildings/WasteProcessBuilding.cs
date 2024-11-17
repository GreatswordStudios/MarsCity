using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteProcessBuilding : Building
{
    public BuildingType GetBuildingType() {return BuildingType.WASTEPROCESS;}

    public void Tick() {
        Dictionary<string, float> drain = SceneMgr.gameDesignValues[BuildingType.WASTEPROCESS];
        SceneMgr sceneMgr = SceneMgr.singleton;

        //check electricity, workforce, and waste
        if (sceneMgr.elec >= drain["elec"]  && sceneMgr.waste >= drain["waste"] && sceneMgr.availableWorkforce >= drain['populationCost']) {
            
            sceneMgr.water -= drain["water"];
            sceneMgr.oxygen -= drain["oxygen"];
            sceneMgr.biomass -= drain["biomass"];
            sceneMgr.elec -= drain["elec"];
            sceneMgr.buildingMats -= drain["buildingMats"];
            sceneMgr.waste -= drain["waste"];
            sceneMgr.availableWorkforce -= drain['populationCost'];
            return;
        }

        //placeholder to raise resource shortfall alert
    }
}
