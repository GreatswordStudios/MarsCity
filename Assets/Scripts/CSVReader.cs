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
            { "waste", float.Parse(pValues[4]) }
        };

            
        return returnDictionary;
    }

    public static void LoadDrainCSV() {
        bool skippedFirstLine = false;
        using(var reader = new StreamReader(Application.dataPath + "/Scripts/Data/Building Consumption and Production Rate Table - Building And Resources.csv")) {
            while (!reader.EndOfStream) {
                var line = reader.ReadLine();
                var values = line.Split(',');

                if (!skippedFirstLine) {
                    skippedFirstLine = true;
                    continue;
                }

                switch(values[0]) {
                    case "Housing":
                        SceneMgr.housingDrain = ParseValues(values);
                        break;
                    case "Landing Base":
                        SceneMgr.landingDrain = ParseValues(values);
                        break;
                    case "Solar Farm":
                        SceneMgr.solarDrain = ParseValues(values);
                        break;
                    case "Nuclear Power Base":
                        SceneMgr.nuclearDrain = ParseValues(values);
                        break;
                    case "Research Facility":
                        SceneMgr.researchDrain = ParseValues(values);
                        break;
                    case "Mining Drills":
                        SceneMgr.minerDrain = ParseValues(values);
                        break;
                    case "Waste Process":
                        SceneMgr.wasteProcessDrain = ParseValues(values);
                        break;
                    case "Greenhouse":
                        SceneMgr.greenhouseDrain = ParseValues(values);
                        break;
                    default:
                        Debug.Log("CSV value: " + values[0] + " is not explicitly defined");
                        break;
                }
            }
        }
    }
}
