using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapCreator
{
    #region Class Attributes

    int fortSize;
    int fortHostility;
    int lootPercentage;
    string path;
    char[,] MapArr;
    string seedString;
    int seed;

    #endregion

    #region Map Chars

    char topLeftCorner = '┌';
    char topRightCorner = '┐';
    char bottomLeftCorner = '└';
    char bottomRightCorner = '┘';
    char tWallUp = '┴';
    char tWallDown = '┬';
    char tWallLeft = '┤';
    char tWallRight = '├';

    #endregion

    #region Map - Ground Tile Prefabs

    /// Ground Tile Prefab Requirements
    /// Prefabs must be 25 x 25 in the format of a string array.
    /// Each 25 x 25 tile should be surounded by 2 rows of grass, with the exception of the structure running right up to the edge.
    /// The rows will be used later to place roads. This will ensure the large map has a system of roads that line up.
    /// Each Ground tile prefab should have associated prefabs of 25 x 25 for an item will spawn there.
    /// Naming convention: nameGroundTile, nameWeaponTile, nameClothingTile, nameFoodTile,
    /// For example if the tile is mainly neighborhoods: neighborhoodGroundTile, neighborhoodWeaponTile, neighborhoodClothingTile, neighborhoodFoodTile
    /// Ground Tile Prefab Requirements
   
    #region SportsStore

    string[] sportsStoreGroundTile = new string[] {
        "ggggggggggggggggggggggggg",
        "ggggggggggggggggggggggggg",
        "gg┌-------------┬-----┐gg",
        "gg|sssssssssssss|wwwww|gg",
        "gg|s------ssssss|wwwww|gg",
        "gg|sssssssssssss|wwwww|gg",
        "gg|s------ssssss└-=---┤gg",
        "gg|sssssssssssssssssss|gg",
        "gg|s------ssssssssssss|gg",
        "gg|sssssssssssssssssss|gg",
        "gg|ssssss┬ssssssssssss|gg",
        "gg|ssssss|ssssssssssss|gg",
        "gg|ssssss|ssssssssssss1gg",
        "gg|ssssss|ssssssssssss1gg",
        "gg|ssssss|ssssssssssss|gg",
        "gg|ssssss┴ssssssssssss|gg",
        "gg|sssssssssssssssssss|gg",
        "gg└--==-----┐sssssssss|gg",
        "gggaaaaaaaaa|sssssssss|gg",
        "gggaaaaaaaaa|sssssssss|gg",
        "gggaaaaaaaaa|sssssssss|gg",
        "gggaaaaaaaaa|sssssssss|gg",
        "ggggggggggaa└--------=┘gg",
        "ggggggggggggggggggggggggg",
        "ggggggggggggggggggggggggg",
    };

    string[] sprotsStoreWeaponTile = new string[]
    {
        "0000000000000000000000000",
        "0000000000000000000000000",
        "0000000000000000000000000",
        "0002722222222222033377000",
        "0002000000222222033333000",
        "0002722222222222033333000",
        "0002000000222222000000000",
        "0002722222222222222222000",
        "0002000000222222222222000",
        "0002222222222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222222222222222222000",
        "0000000000000222222222000",
        "0001111111110222222222000",
        "0001111111110222222222000",
        "0001111111110222222222000",
        "0001111111110222222222000",
        "0000000000110000000000000",
        "0000000000000000000000000",
        "0000000000000000000000000",
    };

    string[] sprotsStoreClothingTile = new string[]
    {
        "0000000000000000000000000",
        "0000000000000000000000000",
        "0000000000000000000000000",
        "0002722222222222033377000",
        "0002000000222222033333000",
        "0002722222222222033333000",
        "0002000000222222000000000",
        "0002722222222222222222000",
        "0002000000222222222222000",
        "0002222222222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222222222222222222000",
        "0000000000000222222222000",
        "0001111111110222222222000",
        "0001111111110222222222000",
        "0001111111110222222222000",
        "0001111111110222222222000",
        "0000000000110000000000000",
        "0000000000000000000000000",
        "0000000000000000000000000",
    };

    string[] sprotsStoreFoodTile = new string[]
    {
        "0000000000000000000000000",
        "0000000000000000000000000",
        "0000000000000000000000000",
        "0002722222222222033377000",
        "0002000000222222033333000",
        "0002722222222222033333000",
        "0002000000222222000000000",
        "0002722222222222222222000",
        "0002000000222222222222000",
        "0002222222222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222220222222222222000",
        "0002222222222222222222000",
        "0000000000000222222222000",
        "0001111111110222222222000",
        "0001111111110222222222000",
        "0001111111110222222222000",
        "0001111111110222222222000",
        "0000000000110000000000000",
        "0000000000000000000000000",
        "0000000000000000000000000",
    };

    #endregion

    #endregion


    // Constructor that takes 3 arguments:
    public MapCreator(string p, string s)
    {
        path = p;
        seedString = s;


        // Create Random Init from Seed if it is not 0000.
        if (seedString != "0000")
        {
            seed = seedString.GetHashCode();
            Random.InitState(seed);
        }

        fortSize = Random.Range(200, 500);
        fortHostility = Random.Range(1, 10);
        lootPercentage = Random.Range(10, 100);
        MapArr = new char[fortSize, fortSize];

    }

    public void CreateMap()
    {
        // Map building algorithm explained.
        // 1. Fill map up with '.' this sybmbol.
        // 2. Place outter wall all the way aroundmap.
        // 3. Find the starting point for the 100 by 100 prefabs. Since the map is square we only need one int for this.
        // 4. Place all prefabs by randomly selecting them and putting them in the map.
        // 5. Fill all tiles still marked with '.' as a ground tile like dirt of rock, ect.
        // 6. Write the map to the save file.
        
        // 1. 
        for (int i = 0; i < fortSize; i++)
        {
            for (int j = 0; j < fortSize; j++)
            {
                MapArr[i, j] = '.';
            }
        }

        // 2. 
        ConstructOuterWall();

        // 3. 
        int startLocationForPrefabsX = ((fortSize - 2) % 25) / 2;
        int startLocationForPrefabsY = ((fortSize - 2) % 25) / 2;
        ConstructBuildingPrefabs(startLocationForPrefabsX, startLocationForPrefabsY);

        WriteMapToFile("Assets/Save/Map.txt");
    }

    void WriteMapToFile(string path)
    {
        StreamWriter writer = new StreamWriter(path, false);
        for (int row = 0; row < fortSize; row++)
        {
            string tempLine = "";

            for (int col = 0; col < fortSize; col++)
            {
                tempLine += MapArr[row, col];
            }
            
            writer.WriteLine(tempLine);
        }
        writer.Close();
    }

    #region Build Helper Functions

    void ConstructOuterWall()
    {
        // Left/ Right Walls
        for (int row = 0; row < fortSize - 0; row++)
        {
            MapArr[row, 0] = '|';
            MapArr[row, fortSize - 1] = '|';
        }

        // Top/ Bottom
        for (int col = 0; col < fortSize - 1; col++)
        {
            MapArr[0, col] = '-';
            MapArr[fortSize - 1, col] = '-';
        }

        // Corners
        MapArr[0, 0] = topLeftCorner;
        MapArr[fortSize - 1, 0] = bottomLeftCorner;
        MapArr[0,  fortSize - 1] = topRightCorner;
        MapArr[fortSize - 1, fortSize - 1] = bottomRightCorner;
    }

    #region Functions - Construct Prefabs

    void ConstructBuildingPrefabs(int startLocationX, int startLocationY)
    {
        bool stillBuilding = true;

        while (stillBuilding)
        {
            // We will move through each row, until the 'startLocationX' is going to hit out of bounds and we will reset it.
            for (int row = startLocationX; row <= fortSize - 25; row += 25)
            {
                // We will move through each col, unitl the 'startLocationY' is going to hit out of bounds and we will reset it.
                for (int col = startLocationY; col <= fortSize - 25; col += 25)
                {
                    PlacePrefabAtLocation(row, col);
                }
            }

            stillBuilding = false;

        }

    }


    void PlacePrefabAtLocation(int startLocationX, int startLocationY)
    {

        for (int row = 0; row < 25; row++)
        {
            for (int col = 0; col < 25; col++)
            {
                MapArr[row + startLocationX, col + startLocationY] = sportsStoreGroundTile[row][col];
            }

        }

    }

    #endregion

    #endregion

}
