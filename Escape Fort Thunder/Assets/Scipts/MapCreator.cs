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
    string[] Map;
    string seedString;
    int seed;

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
        Map = new string[fortSize];

        Debug.Log(fortSize);
        Debug.Log(fortHostility);
        Debug.Log(lootPercentage);


    }

    public void CreateMap()
    {
        for (int i = 0; i < fortSize; i++)
        {
            for (int j = 0; j < fortSize; j++)
            {
                Map[i] += "."; 
            }
        }

        WriteMapToFile("Assets/Save/Map.txt");
    }

    void WriteMapToFile(string path)
    {
        StreamWriter writer = new StreamWriter(path, false);
        for (int line = 0; line < Map.Length; line++)
        {
            writer.WriteLine(Map[line]);
        }
        writer.Close();
    }

    #region Build Helper Functions

    void ConstructOuterWall()
    {

    }

    void ConstructLeftRightWall()
    {
        // Function will load the starting left and right main walls.
        // Pick how many tiles the wall will be inset from the edge of the map.
        int wallInset = Random.Range(0, 20);
        for (int row = wallInset; row < fortSize - wallInset; row++)
        {
            Debug.Log("Issue is the string array Map. Need to change this.");
        }
        
    }

    #endregion

}
