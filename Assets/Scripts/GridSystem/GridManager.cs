using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{   
    public static GridManager singleton;
    public GameObject gridSpacePrefab;
    public GameObject carriedBuilding;
    const float gridSize = 1.0f; // spacing between GridSpaces

    GameObject[,] grid = new GameObject[(int) SceneMgr.gridSize.y, (int) SceneMgr.gridSize.x];

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
        Vector3 mousePos = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        Vector2 selectedSpace = Vector2.zero;

        for(int i = 0; i < grid.GetLength(0); i++) {
            for(int j = 0; j < grid.GetLength(1); j++) {
                if(grid[i, j].GetComponent<Collider>().bounds.IntersectRay(mouseRay)){
                    selectedSpace = new Vector2(i, j);

                    //grid[i, j].GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }

        // code if a building is currently being carried
        if(carriedBuilding != null) {
            GameObject gridSpace = grid[(int) selectedSpace.x, (int) selectedSpace.y];
            carriedBuilding.transform.position = gridSpace.transform.position;
            
            if(BuildingCanBePlaced(selectedSpace)) {
                if(Input.GetMouseButtonDown(0)) {
                    carriedBuilding.GetComponent<MeshRenderer>().material.color = Color.white;
                    //gridSpace.GetComponent<MeshRenderer>().material.color = Color.red;
                    SceneMgr.singleton.buildings[(int) selectedSpace.x, (int) selectedSpace.y] = new GreenhouseBuilding();

                    carriedBuilding.transform.position = gridSpace.transform.position;
                    gridSpace.GetComponent<GridSpace>().buildingObject = carriedBuilding;
                    carriedBuilding.transform.parent = gridSpace.transform;
                    carriedBuilding = null;
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

        return true;
    }
}
