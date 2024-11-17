using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class CSVReader
{
    private static Dictionary<string, float> ParseValues(string[] pValues){
        Dictionary<string, float> returnDictionary = new Dictionary<string, float>() {
            { "water", float.Parse(pValues[1]) },
            { "elec", float.Parse(pValues[3]) },
            { "oxygen", float.Parse(pValues[2]) },
            { "biomass", float.Parse(pValues[5]) },
            { "buildingMats", float.Parse(pValues[6]) },
            { "waste", float.Parse(pValues[4]) },
            { "cost", float.Parse(pValues[7]) },
            { "sizeX", float.Parse(pValues[10].Split(":")[0]) },
            { "sizeY", float.Parse(pValues[10].Split(":")[1]) },
            { "populationCost", float.Parse(pValues[9])}
        };

            
        return returnDictionary;
    }

    public static void LoadDrainCSV() {
        bool skippedFirstLine = false;
        using(var reader = new StreamReader(Application.streamingAssetsPath + "/Building Consumption and Production Rate Table - Building And Resources.csv")) {
            while (!reader.EndOfStream) {
                var line = reader.ReadLine();
                var values = line.Split(',');

                if (!skippedFirstLine) {
                    skippedFirstLine = true;
                    continue;
                }
    
                switch(values[0]) {
                    case "Housing":
                        SceneMgr.gameDesignValues.Add(BuildingType.HOUSING, ParseValues(values));
                        break;
                    case "Landing Base":
                        SceneMgr.gameDesignValues.Add(BuildingType.LANDING, ParseValues(values));
                        break;
                    case "Solar Farm":
                        SceneMgr.gameDesignValues.Add(BuildingType.SOLAR, ParseValues(values));
                        break;
                    case "Nuclear Power Base":
                        SceneMgr.gameDesignValues.Add(BuildingType.NUCLEAR, ParseValues(values));
                        break;
                    case "Research Facility":
                        SceneMgr.gameDesignValues.Add(BuildingType.RESEARCH, ParseValues(values));
                        break;
                    case "Mining Drills":
                        SceneMgr.gameDesignValues.Add(BuildingType.MINER, ParseValues(values));
                        break;
                    case "Waste Process":
                        SceneMgr.gameDesignValues.Add(BuildingType.WASTEPROCESS, ParseValues(values));
                        break;
                    case "Greenhouse":
                        SceneMgr.gameDesignValues.Add(BuildingType.GREENHOUSE, ParseValues(values));
                        break;
                    case "Population (1 person)":
                        SceneMgr.gameDesignValues.Add(BuildingType.POPULATION, ParseValues(values));
                        break;
                    default:
                        Debug.Log("CSV value: " + values[0] + " is not explicitly defined");
                        break;
                }
            }
        }
    }
}
