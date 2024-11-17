using UnityEngine;

public enum BuildingType{
    GREENHOUSE,
    SOLAR,
    WASTEPROCESS,
    NUCLEAR,
    MINER,
    HOUSING,
    RESEARCH,
    LANDING,
    POPULATION
}

public interface Building
{
    public BuildingType GetBuildingType();

    public void Tick();
}
