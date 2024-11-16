using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenhouseBuilding: Building
{
    public void Tick() {
        Dictionary<string, float> drain = SceneMgr.gameDesignValues[BuildingType.GREENHOUSE];
        SceneMgr sceneMgr = SceneMgr.singleton;

        if(sceneMgr.water > drain["water"]) {
            sceneMgr.water -= drain["water"];
            sceneMgr.oxygen -= drain["oxygen"];
            sceneMgr.biomass -= drain["biomass"];
            sceneMgr.elec -= drain["elec"];
            sceneMgr.buildingMats -= drain["buildingMats"];
            sceneMgr.waste -= drain["waste"];
        }
    }
}
