using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr singleton;

    Building[] buildings;

    public float water = 0;
    public float elec = 0;
    public float oxygen = 0;
    public float biomass = 0;
    public float buildingMats = 0;

    public float totalDrainRateWater = 0;
    public float totalDrainRateElec = 0;
    public float totalDrainRateOxygen = 0;
    public float totalDrainRateBiomass = 0;
    public float totalDrainRateBuildingMats = 0;

    double tickTime = 1; // time in sec between ticks at normal speed
    double timeSinceLastTick = 0;
    double tickMultiplier = 1.0; 

    // GAME DESIGN VARIABLES
    public static Dictionary<string, float> greenhouseDrain = new Dictionary<string, float>(){
            { "water", 10 },
            { "elec", 5 },
            { "oxygen", -20 },
            { "biomass", -20 },
            { "buildingMats", 0 }
        };

    public static Dictionary<string, float> solarDrain = new Dictionary<string, float>(){
            { "water", 0 },
            { "elec", -20 },
            { "oxygen", 0 },
            { "biomass", 0 },
            { "buildingMats", 0 }
        };
     
    public static Dictionary<string, float> wasteProcessDrain = new Dictionary<string, float>(){
            { "water", -20 },
            { "elec", 10 },
            { "oxygen", 0 },
            { "biomass", 0 },
            { "buildingMats", 0 }
        }; 
     
    public static Dictionary<string, float> nuclearDrain = new Dictionary<string, float>(){
            { "water", 10 },
            { "elec", 5 },
            { "oxygen", -20 },
            { "biomass", -20 },
            { "buildingMats", 0 }
        }; 
     
    public static Dictionary<string, float> minerDrain = new Dictionary<string, float>(){
            { "water", 10 },
            { "elec", 5 },
            { "oxygen", -20 },
            { "biomass", -20 },
            { "buildingMats", 0 }
        }; 
     
    public static Dictionary<string, float> housingDrain = new Dictionary<string, float>(){
            { "water", 10 },
            { "elec", 5 },
            { "oxygen", -20 },
            { "biomass", -20 },
            { "buildingMats", 0 }
        }; 
     
    public static Dictionary<string, float> researchDrain = new Dictionary<string, float>(){
            { "water", 10 },
            { "elec", 5 },
            { "oxygen", -20 },
            { "biomass", -20 },
            { "buildingMats", 0 }
        }; 
     
    public static Dictionary<string, float> landingDrain = new Dictionary<string, float>(){
            { "water", 0 },
            { "elec", 0 },
            { "oxygen", 0 },
            { "biomass", 0 },
            { "buildingMats", 0 }
        }; 
     
    
    // Start is called before the first frame update
    void Start()
    {   
        singleton = this;
        water = 500;
        elec = 0;
        buildingMats = 50;
        oxygen = 50;
        biomass = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastTick += Time.deltaTime;
        if(timeSinceLastTick * tickMultiplier > tickTime) {
            TickAll();

            timeSinceLastTick = 0;
        }
    }

    void TickAll() {
        float initialWater = water;
        float initialElec = elec;
        float initialOxygen = oxygen;
        float initialBiomass = biomass;
        float initialBuildingMats = buildingMats;

        foreach (Building building in buildings) {
            building.Tick();
        }

        float totalDrainRateWater = initialWater - water;
        float totalDrainRateElec = initialElec - elec;
        float totalDrainRateOxygen = initialOxygen - oxygen;
        float totalDrainRateBiomass = initialBiomass - biomass;
        float totalDrainRateBuildingMats = initialBuildingMats - buildingMats;
    }
}
