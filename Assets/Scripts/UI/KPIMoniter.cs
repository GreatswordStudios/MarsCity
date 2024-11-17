using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum KPI{
    WATER,
    POPULATION,
    OXYGEN,
    AGRICULTURE,
    WASTE,
    POWER,
    BUILDINGMATERIALS
}
public class KPIMoniter : MonoBehaviour
{
    public KPI moniterType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float curAmount = 0;
        float changeAmount = 0;
        switch(moniterType) {
            case KPI.WATER:
                curAmount = SceneMgr.singleton.water;
                changeAmount = SceneMgr.singleton.totalDrainRateWater;
                break;
            case KPI.POPULATION:
                curAmount = SceneMgr.singleton.population;
                //changeAmount = SceneMgr.singleton.totalDrainRateWater;
                break;
            case KPI.OXYGEN:
                curAmount = SceneMgr.singleton.oxygen;
                changeAmount = SceneMgr.singleton.totalDrainRateOxygen;
                break;
            case KPI.AGRICULTURE:
                curAmount = SceneMgr.singleton.biomass;
                changeAmount = SceneMgr.singleton.biomass;
                break;
            case KPI.WASTE:
                curAmount = SceneMgr.singleton.waste;
                changeAmount = SceneMgr.singleton.totalDrainRateWaste;
                break;
            case KPI.POWER:
                curAmount = SceneMgr.singleton.elec;
                changeAmount = SceneMgr.singleton.totalDrainRateElec;
                break;
            case KPI.BUILDINGMATERIALS:
                curAmount = SceneMgr.singleton.buildingMats;
                changeAmount = SceneMgr.singleton.totalDrainRateBuildingMats;
                break;    
        }

        GetComponent<TMP_Text>().text = curAmount + "(" + changeAmount + ")";
        
    }
}
