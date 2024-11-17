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
        if(Utils.IsMouseOverUI()) {
            return;
        }
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

        ResetMaterials();
        // code if a building is currently being carried
        if(carriedBuilding != null) {

            GameObject gridSpace = grid[(int) selectedSpace.x, (int) selectedSpace.y];
            carriedBuilding.transform.position = gridSpace.transform.position;
            
            if(Input.GetMouseButtonDown(1)) { // cancel placement
                Destroy(carriedBuilding);
                carriedBuilding = null;
            }
            else if(BuildingCanBePlaced(selectedSpace, carriedBuildingType)) {
                
                if(Input.GetMouseButtonDown(0)) { // attempt to place
                    PlaceBuilding(selectedSpace, carriedBuildingType);
                    Destroy(carriedBuilding); // Get rid of carried representation
                    carriedBuilding = null; // remove building (visual) from our control
                }
                else { // no action being done
                    carriedBuilding.GetComponent<MeshRenderer>().material.color = Color.green;
                }
            }
            else {
                carriedBuilding.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        else { // not carrying a building
            if(Input.GetMouseButtonDown(0)) { // we close panel if not carrying and clicking
                InventoryManager.CloseAllPanel();
            }

            if (Input.GetKey(KeyCode.LeftShift)) {
                int x = (int) selectedSpace.x;
                int y = (int) selectedSpace.y;
                GridSpace gridSpace = grid[x, y].GetComponent<GridSpace>();
                if(!gridSpace.isProtected) {
                    GameObject attachedBuilding = gridSpace.buildingObject;
                    if(attachedBuilding != null){
                        attachedBuilding.GetComponent<MeshRenderer>().material.color = Color.red;
                    }

                    if(Input.GetMouseButtonDown(0)) {
                        DestroyBuilding(selectedSpace);
                    }
                }
            }
        }
    }

    public void ResetMaterials() {
        foreach(GameObject gridSpace in grid) {
            GameObject attachedBuilding = gridSpace.GetComponent<GridSpace>().buildingObject;
            if(attachedBuilding != null) {
                attachedBuilding.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }

    public void CarryBuilding(BuildingType buildingType) {
        if(carriedBuilding != null) {
            Destroy(carriedBuilding);
        }
        InventoryManager.CloseAllPanel();
        carriedBuildingType = buildingType;
        carriedBuilding = Instantiate(Utils.GetBuildingMeshFromEnum(buildingType));
    }

    public void DestroyBuilding(Vector2 selectedSpace) {
        int x = (int) selectedSpace.x;
        int y = (int) selectedSpace.y;
        
        if(grid[x, y].GetComponent<GridSpace>().buildingObject == null) return; // nothing to delete

        Destroy(grid[x, y].GetComponent<GridSpace>().buildingObject);
        SceneMgr.singleton.DestroyBuilding(selectedSpace);

        SceneMgr.singleton.buildingMats += -5;
        TooltipSystem.Hide(); // tooltip will be on top of building and must be hidden
    }

    public void PlaceBuilding(Vector2 selectedSpace, BuildingType buildingType, bool isProtected = false) {
        if(!BuildingCanBePlaced(selectedSpace, buildingType)) return;
        
        GameObject spawnedBuilding = Instantiate(Utils.GetBuildingMeshFromEnum(buildingType)); // create building (visual)
        
        // place building placeholders
        Building placedBuilding = CreateBuildingFromEnum(buildingType); // create building (logical)
        int x, y;
        for(int i = 0; i < SceneMgr.gameDesignValues[buildingType]["sizeX"]; i++) {
            for(int j = 0; j < SceneMgr.gameDesignValues[buildingType]["sizeY"]; j++) {
                x = (int) selectedSpace.x + i;
                y = (int) selectedSpace.y + j;
                SceneMgr.singleton.buildings[x, y] = placedBuilding;
                
                GridSpace gridSpace = grid[x, y].GetComponent<GridSpace>();
                gridSpace.buildingObject = spawnedBuilding;
                gridSpace.isProtected = isProtected;
                if(i == 0 && j == 0) { // only place building at location of first space
                    spawnedBuilding.transform.position = gridSpace.transform.position;
                }
            }
        }

    }

    bool BuildingCanBePlaced(Vector2 selectedSpace, BuildingType buildingType) {
        if(SceneMgr.singleton.buildingMats < SceneMgr.gameDesignValues[buildingType]["cost"]) {
            return false;
        }
    
        if(SceneMgr.singleton.population < SceneMgr.singleton.GetWorkforceNeeded() + SceneMgr.gameDesignValues[buildingType]["populationCost"]) {
            return false;
        }

        // check size
        int x, y;
        for(int i = 0; i < SceneMgr.gameDesignValues[buildingType]["sizeX"]; i++) {
            for(int j = 0; j < SceneMgr.gameDesignValues[buildingType]["sizeY"]; j++) {
                x = (int) selectedSpace.x + i;
                y = (int) selectedSpace.y + j;
                if(x > SceneMgr.singleton.buildings.GetLength(0) || y > SceneMgr.singleton.buildings.GetLength(1) || SceneMgr.singleton.buildings[x, y] != null) {
                    return false;
                }
            }
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
