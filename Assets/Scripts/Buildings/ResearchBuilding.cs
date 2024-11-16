using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchBuilding : Building
{
    public BuildingType GetBuildingType() {return BuildingType.RESEARCH;}

    public void Tick() {
        Dictionary<string, float> drain = SceneMgr.gameDesignValues[BuildingType.RESEARCH];
        SceneMgr sceneMgr = SceneMgr.singleton;

        sceneMgr.water -= drain["water"] * sceneMgr.population;
        sceneMgr.oxygen -= drain["oxygen"] * sceneMgr.population;
        sceneMgr.biomass -= drain["biomass"] * sceneMgr.population;
        sceneMgr.elec -= drain["elec"] * sceneMgr.population;
        sceneMgr.buildingMats -= drain["buildingMats"] * sceneMgr.population;
        sceneMgr.waste -= drain["waste"] * sceneMgr.population;
    }
}
