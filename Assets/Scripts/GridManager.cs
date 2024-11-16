using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject gridSpacePrefab; 
    const float gridSize = 1.0f; // spacing between GridSpaces

    GridSpace[,] grid = new GridSpace[(int) SceneMgr.gridSize.y, (int) SceneMgr.gridSize.x];

    // Start is called before the first frame update
    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid() {
        for(int i = 0; i < (int) SceneMgr.gridSize.y; i++) {
            for(int j = 0; j < (int) SceneMgr.gridSize.x; j++) {
                //grid[i, j] =  Instantiate(gridSpacePrefab, this.transform.position + new Vector3(gridSize * i, 0, gridSize * j));
            }
        }
    }
}