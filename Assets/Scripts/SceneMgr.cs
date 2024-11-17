using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SceneMgr : MonoBehaviour
{
    public static System.Random rng = new System.Random();
    public static SceneMgr singleton;
    public static Vector2 gridSize = new Vector2(64, 64);

    public Building[,] buildings = new Building[(int) gridSize.y, (int) gridSize.x];

    public float water = 0;
    public float elec = 0;
    public float oxygen = 0;
    public float biomass = 0;
    public float buildingMats = 0;
    public float waste = 0;
    public float population = 0;
    public float availableWorkforce = 0;
    public DateTime curDate = new DateTime(2100, 1, 1);

    public float totalDrainRateWater = 0;
    public float totalDrainRateElec = 0;
    public float totalDrainRateOxygen = 0;
    public float totalDrainRateBiomass = 0;
    public float totalDrainRateBuildingMats = 0;
    public float totalDrainRateWaste = 0;

    double tickTime = 1; // time in sec between ticks at normal speed
    double timeSinceLastTick = 0;
    public double tickMultiplier = 1; 

    // GAME DESIGN VARIABLES - loaded by CSV in CSVReader
    public static Dictionary<BuildingType, Dictionary<string, float>> gameDesignValues = new Dictionary<BuildingType, Dictionary<string, float>>();
    
    // Start is called before the first frame update
    void Start()
    {   
        singleton = this;
        water = 500;
        elec = 200;
        population = 100;
        waste = 100;
        buildingMats = 50;
        oxygen = 50;
        biomass = 1;
        CSVReader.LoadDrainCSV();

        GridManager.singleton.PlaceBuilding(gridSize / 2, BuildingType.LANDING, true);
        GridManager.singleton.PlaceBuilding((gridSize / 2) - new Vector2(1, 0), BuildingType.HOUSING, true);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastTick += Time.deltaTime;
        if(timeSinceLastTick * tickMultiplier > tickTime) {
            TickAll();
            curDate = curDate.AddDays(1);
            timeSinceLastTick = 0;
        }

        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Close the application
            Debug.Log("Escape key pressed. Exiting the application.");
            Application.Quit();
        }

    }

    void TickAll() {
        float initialWater = water;
        float initialElec = elec;
        float initialOxygen = oxygen;
        float initialBiomass = biomass;
        float initialBuildingMats = buildingMats;
        float initialWaste = waste;
        availableWorkforce = population;

        // flatten buildings array so we can randomize execution order
        List<Building> flattenedBuildings = new List<Building>();
        for(int i = 0; i < buildings.GetLength(0); i++){
            for(int j = 0; j < buildings.GetLength(1); j++){
                flattenedBuildings.Add(buildings[j, i]);
            }
        }

        flattenedBuildings = Utils.Shuffle(flattenedBuildings);
        List<Building> buildingsTicked = new List<Building>();

        foreach (Building building in flattenedBuildings) {
            if(building != null) {
                if(!buildingsTicked.Contains(building)){
                    building.Tick();
                    buildingsTicked.Add(building);
                }
            }
        }

        TickPopulation();

        totalDrainRateWater = water - initialWater;
        totalDrainRateElec = elec - initialElec;
        totalDrainRateOxygen = oxygen - initialOxygen;
        totalDrainRateBiomass = biomass - initialBiomass;
        totalDrainRateBuildingMats = buildingMats - initialBuildingMats;
        totalDrainRateWaste = waste - initialWaste;
    }

    public float GetWorkforceNeeded() {
        float neededWorkforce = 0;
        List<Building> uniqueBuildings = buildings.OfType<Building>().ToList().Distinct().ToList();

        foreach(Building building in uniqueBuildings) {
            neededWorkforce += gameDesignValues[building.GetBuildingType()]["populationCost"];
        }
        return neededWorkforce;
    }

    void TickPopulation() {
        water -= gameDesignValues[BuildingType.POPULATION]["water"] * population;
        oxygen -= gameDesignValues[BuildingType.POPULATION]["oxygen"] * population;
        biomass -= gameDesignValues[BuildingType.POPULATION]["biomass"] * population;
        elec -= gameDesignValues[BuildingType.POPULATION]["elec"] * population;
        buildingMats -= gameDesignValues[BuildingType.POPULATION]["buildingMats"] * population;
        waste -= gameDesignValues[BuildingType.POPULATION]["waste"] * population;
    }

    public void DestroyBuilding(Vector2 selectedSpace) {
        int x = (int) selectedSpace.x;
        int y = (int) selectedSpace.y;

        Building building = buildings[x, y];

        if(building != null) {
            for(int i = 0; i < buildings.GetLength(0); i++){
                for(int j = 0; j < buildings.GetLength(1); j++){
                    if(buildings[j, i] == building) {
                        buildings[j, i] = null;
                    }
                }
            }
        }
    }

    public static void SetTickMultiplier(float newTickMultiplier) {
        singleton.tickMultiplier = newTickMultiplier;
    }

}
