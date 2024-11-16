using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarBuilding : Building
{
    public void Tick() {
        Dictionary<string, float> drain = SceneMgr.gameDesignValues[BuildingType.SOLAR];
        SceneMgr sceneMgr = SceneMgr.singleton;

        sceneMgr.water -= drain["water"] * sceneMgr.population;
        sceneMgr.oxygen -= drain["oxygen"] * sceneMgr.population;
        sceneMgr.biomass -= drain["biomass"] * sceneMgr.population;
        sceneMgr.elec -= drain["elec"] * sceneMgr.population;
        sceneMgr.buildingMats -= drain["buildingMats"] * sceneMgr.population;
        sceneMgr.waste -= drain["waste"] * sceneMgr.population;
    }
}
