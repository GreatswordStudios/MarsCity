using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr singleton;
    public static Vector2 gridSize = new Vector2(64, 64);

    Building[,] buildings = new Building[(int) gridSize.y, (int) gridSize.x];

    public float water = 0;
    public float elec = 0;
    public float oxygen = 0;
    public float biomass = 0;
    public float buildingMats = 0;
    public float waste = 0;
    public float population = 0;

    public float totalDrainRateWater = 0;
    public float totalDrainRateElec = 0;
    public float totalDrainRateOxygen = 0;
    public float totalDrainRateBiomass = 0;
    public float totalDrainRateBuildingMats = 0;
    public float totalDrainRateWaste = 0;

    double tickTime = 1; // time in sec between ticks at normal speed
    double timeSinceLastTick = 0;
    double tickMultiplier = 0; 

    // GAME DESIGN VARIABLES - loaded by CSV in CSVReader
    public static Dictionary<string, float> greenhouseDrain;
    public static Dictionary<string, float> solarDrain;
    public static Dictionary<string, float> wasteProcessDrain;
    public static Dictionary<string, float> nuclearDrain;
    public static Dictionary<string, float> minerDrain;
    public static Dictionary<string, float> housingDrain; 
    public static Dictionary<string, float> researchDrain;
    public static Dictionary<string, float> landingDrain;
    
    // Start is called before the first frame update
    void Start()
    {   
        singleton = this;
        water = 500;
        elec = 0;
        buildingMats = 50;
        oxygen = 50;
        biomass = 1;
        CSVReader.LoadDrainCSV();
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
        float initialWaste = waste;

        foreach (Building building in buildings) {
            building.Tick();
        }

        float totalDrainRateWater = initialWater - water;
        float totalDrainRateElec = initialElec - elec;
        float totalDrainRateOxygen = initialOxygen - oxygen;
        float totalDrainRateBiomass = initialBiomass - biomass;
        float totalDrainRateBuildingMats = initialBuildingMats - buildingMats;
        float totalDrainRateWaste = initialWaste - waste;
    }
}
