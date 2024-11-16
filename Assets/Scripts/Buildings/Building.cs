using UnityEngine;

public enum BuildingType{
    GREENHOUSE,
    SOLAR,
    WASTEPROCESS,
    NUCLEAR,
    MINER,
    HOUSING,
    RESEARCH,
    LANDING
}

public interface Building
{


    public void Tick();
}
