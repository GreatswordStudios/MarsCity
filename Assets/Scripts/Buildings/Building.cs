using UnityEngine;

public enum BuildingType{
    GREENHOUSE,
    SOLAR,
    WASTEPROCESS,
    NUCLEAR,
    NUCLEAR_2,
    NUCLEAR_3,
    MINER,
    HOUSING,
    HOUSING_2,
    RESEARCH,
    LANDING,
    POPULATION
}

public interface Building
{
    public BuildingType GetBuildingType();

    public void Tick();
}
