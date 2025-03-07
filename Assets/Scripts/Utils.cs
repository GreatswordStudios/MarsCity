using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static bool mouseOverUI = false;

    public static List<T> Shuffle<T>(List<T> arr) {
        int p = arr.Count;
        for (int n = p-1; n > 0 ; n--) {
            int r = SceneMgr.rng.Next(1, n);
            var t = arr[r];
            arr[r] = arr[n];
            arr[n] = t;
        }

        return arr;
    }

    public static bool IsMouseOverUI() {
        return mouseOverUI;
    }

    public static GameObject GetBuildingMeshFromEnum(BuildingType buildingType) {
        GameObject prefab = null;
        switch(buildingType) {
            case BuildingType.GREENHOUSE:
                prefab = Resources.Load<GameObject>("Architecture/Greenhouse");
                break;
            case BuildingType.SOLAR:
                prefab = Resources.Load<GameObject>("Architecture/");
                break;
            case BuildingType.WASTEPROCESS:
                prefab = Resources.Load<GameObject>("Architecture/Recycling Plant");
                break;
            case BuildingType.NUCLEAR:
                prefab = Resources.Load<GameObject>("Architecture/Power T1");
                break;
            case BuildingType.NUCLEAR_2:
                prefab = Resources.Load<GameObject>("Architecture/Power T2");
                break;
            case BuildingType.NUCLEAR_3:
                prefab = Resources.Load<GameObject>("Architecture/Power T3");
                break;
            case BuildingType.MINER:
                prefab = Resources.Load<GameObject>("Architecture/Mining Facility");
                break;
            case BuildingType.HOUSING:
                prefab = Resources.Load<GameObject>("Architecture/main HAB");
                break;
            case BuildingType.HOUSING_2:
                prefab = Resources.Load<GameObject>("Architecture/Housing");
                break;
            case BuildingType.RESEARCH:
                prefab = Resources.Load<GameObject>("Architecture/Research Lab");
                break;
            case BuildingType.LANDING:
                prefab = Resources.Load<GameObject>("Architecture/Landing Pad");
                break;
        }

        return prefab;
    }

}
