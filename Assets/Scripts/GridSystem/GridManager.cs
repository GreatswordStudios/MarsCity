using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{   
    public static GridManager singleton;
    public GameObject gridSpacePrefab;
    const float gridSize = 10; // spacing between GridSpaces

    GameObject[,] grid = new GameObject[(int) SceneMgr.gridSize.y, (int) SceneMgr.gridSize.x];

    public GameObject carriedBuilding; // visual representation of the carried building
    public BuildingType carriedBuildingType = BuildingType.GREENHOUSE; // the building type we are carrying
    Vector2 selectedSpace = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        SpawnGrid();
    }

    void SpawnGrid() {
        for(int i = 0; i < (int) SceneMgr.gridSize.y; i++) {
            for(int j = 0; j < (int) SceneMgr.gridSize.x; j++) {
                GameObject spawnedSpace = Instantiate(gridSpacePrefab, this.transform.position + new Vector3(gridSize * i, 0, gridSize * j), Quaternion.identity);
                spawnedSpace.transform.parent = this.transform;
                spawnedSpace.name = "GridSpace(" + i + "," + j + ")";
                grid[i, j] = spawnedSpace;
            }
        }
    }
    
    void Update() {
        // Get the gridspace in which the mouse is over
        Vector3 mousePos = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        for(int i = 0; i < grid.GetLength(0); i++) {
            for(int j = 0; j < grid.GetLength(1); j++) {
                if(grid[i, j].GetComponent<Collider>().bounds.IntersectRay(mouseRay)){
                    selectedSpace = new Vector2(i, j);
                }
            }
        }

        // code if a building is currently being carried
        if(carriedBuilding != null) {
            GameObject gridSpace = grid[(int) selectedSpace.x, (int) selectedSpace.y];
            carriedBuilding.transform.position = gridSpace.transform.position;
            
            if(BuildingCanBePlaced(selectedSpace)) {
                if(Input.GetMouseButtonDown(0)) {
                    carriedBuilding.GetComponent<MeshRenderer>().material.color = Color.white; // set msterial back to normal

                    SceneMgr.singleton.buildings[(int) selectedSpace.x, (int) selectedSpace.y] = CreateBuildingFromEnum(carriedBuildingType); // create building (logical)

                    gridSpace.GetComponent<GridSpace>().buildingObject = carriedBuilding;
                    carriedBuilding.transform.parent = gridSpace.transform;
                    carriedBuilding = null; // remove building (visual) from our control
                }
                else {
                    carriedBuilding.GetComponent<MeshRenderer>().material.color = Color.green;
                }
            }
            else {
                carriedBuilding.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        
    }

    bool BuildingCanBePlaced(Vector2 selectedSpace) {
        if(SceneMgr.singleton.buildings[(int) selectedSpace.x, (int) selectedSpace.y] != null) {
            return false;
        }
        if(SceneMgr.singleton.buildingMats < SceneMgr.gameDesignValues[carriedBuildingType]["cost"]) {
            return false;
        }

        return true;
    }

    Building CreateBuildingFromEnum(BuildingType buildingType) {
        Building returnBuilding;
        switch(buildingType){
            case BuildingType.GREENHOUSE:
                returnBuilding = new GreenhouseBuilding();
                break;
            case BuildingType.SOLAR:
                returnBuilding = new SolarBuilding();
                break;
            case BuildingType.WASTEPROCESS:
                returnBuilding = new WasteProcessBuilding();
                break;
            case BuildingType.NUCLEAR:
                returnBuilding = new NuclearBuilding();
                break;
            case BuildingType.MINER:
                returnBuilding = new MinerBuilding();
                break;
            case BuildingType.HOUSING:
                returnBuilding = new HousingBuilding();
                break;
            case BuildingType.RESEARCH:
                returnBuilding = new ResearchBuilding();
                break;
            case BuildingType.LANDING:
                returnBuilding = new LandingBuilding();
                break;
            default:
                returnBuilding = null;
                Debug.Log("Could not create building object - Passed a incompatable enum type");
                break;
        }

        return returnBuilding;
    }
}
