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

    #region Map - Building Prefabs

    char[,] smallBuilding01 = new char[10, 10];
    int[] smallBuilding01Dims = new int[] {10, 10};

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

        fortSize = Random.Range(500, 1000);
        fortHostility = Random.Range(1, 10);
        lootPercentage = Random.Range(10, 100);
        MapArr = new char[fortSize, fortSize];

        InitializePrefabs();
    }

    public void CreateMap()
    {
        for (int i = 0; i < fortSize; i++)
        {
            for (int j = 0; j < fortSize; j++)
            {
                MapArr[i, j] = '.';
            }
        }

        ConstructOuterWall();
        ConstructBuildingPrefabs();

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
        // Pick how many tiles the wall will be inset from the edge of the map.
        int wallInset = Random.Range(0, 20);

        // Left/ Right Walls
        for (int row = wallInset; row < fortSize - wallInset; row++)
        {
            MapArr[row, wallInset] = '|';
            MapArr[row, fortSize - wallInset] = '|';
        }

        // Top/ Bottom
        for (int col = wallInset; col < fortSize - wallInset; col++)
        {
            MapArr[wallInset, col] = '-';
            MapArr[fortSize - wallInset, col] = '-';
        }

        // Corners
        MapArr[wallInset, wallInset] = topLeftCorner;
        MapArr[fortSize - wallInset, wallInset] = bottomLeftCorner;
        MapArr[wallInset,  fortSize - wallInset] = topRightCorner;
        MapArr[fortSize - wallInset, fortSize - wallInset] = bottomRightCorner;
    }

    void ConstructBuildingPrefabs()
    {
        int startXLocation = 0;
        int startYLocation = 0;

        //DoesPrefabFitAtLocation(0, 0, smallBuilding01Dims);
        int numberOfSmallBuildings = Random.Range(fortSize / 20, fortSize / 10);
        Debug.Log("Number of Small Buildings" + numberOfSmallBuildings.ToString());

        while(numberOfSmallBuildings > 0)
        {
            startXLocation = Random.Range(0, fortSize);
            startYLocation = Random.Range(0, fortSize);

            if (DoesPrefabFitAtLocation(startXLocation, startYLocation, smallBuilding01Dims))
            {
                numberOfSmallBuildings -= 1;
                PlacePrefabAtLocation(startXLocation, startYLocation, smallBuilding01Dims, smallBuilding01);
            }
        }

    }

    #region Functions - Construct Prefabs

    void InitializePrefabs()
    {
        // smallBuilding01
        // Left - Right Walls
        //for (int row = 0; row < smallBuilding01Dims[0]; row++)
        //{
        //    smallBuilding01[row, 0] = '|';
        //    smallBuilding01[row, smallBuilding01Dims[1]] = '|';
        //}

        for (int row = 0; row < smallBuilding01Dims[0]; row++)
        {
            for (int col = 0; col < smallBuilding01Dims[1]; col++)
            {
                smallBuilding01[row, col] = 'X';
            }
        }
    }

    bool DoesPrefabFitAtLocation(int startX, int startY, int[] dims)
    {
        bool prefabFits = true;

        for (int row = startX; row < startX + dims[0]; row++)
        {
            for (int col = startY; col < startY + dims[1]; col++)
            {
                if (MapArr[row, col] != '.') prefabFits = false;
            }
        }

        Debug.Log(prefabFits);

        return prefabFits;
    }

    void PlacePrefabAtLocation(int startX, int startY, int[] dims, char[,] prefab)
    {
        for (int row = 0; row < dims[0]; row++)
        {
            for (int col = 0; col < dims[1]; col++)
            {
                MapArr[row + startX, col + startY] = prefab[row, col];
            }
        }
    }

    #endregion

    #endregion

}
