﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour {

    public int numRows;
    public int numCols;
    // scaling and spacing should generally be the same number. Unity inspector overrides, remember to set in unity inspector.
    public float scaling = 1; // Scales size of tiles only. (default tile sprite should be 1 game unit width)
    public float spacing = 1; // Spacing in game units.
    public Tile tileScript;

    public Tile[,] grid; // Public for editor to work, try not to access directly.

    Tile getTile(int i, int j) {
        return grid[i, j];
    }
    
    void Awake () {
        DontDestroyOnLoad(gameObject);
        /* // This is done by Map Editor instead.
        grid = new Tile[numRows, numCols];
        
        float topEdge = (float) (spacing * (numRows / 2.0 - 0.5));
        float leftEdge = (float) -(spacing * (numCols / 2.0 - 0.5));
        for (int i = 0; i < numRows; ++i) {
            GameObject row = new GameObject("row");
            row.transform.parent = gameObject.transform;
            row.transform.localPosition = new Vector3(0, topEdge - (spacing * i), 0);
            for (int j = 0; j < numCols; ++j)
            {
                Tile tile = Instantiate(tileScript);
                tile.transform.parent = row.transform;
                tile.transform.localPosition = new Vector3(leftEdge + (spacing * j), 0, 0);
                tile.transform.localScale = new Vector3(scaling, scaling, 0);
                grid[i, j] = tile;
            }
        }
        */
    }

    void Start() {
        
    }
    
    void Update () {
        
    }
}