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

    #endregion

    // Constructor that takes no arguments:
    public MapCreator()
    {
        fortSize = 5000;
        fortHostility = 5;
        lootPercentage = 50;
        Map = new string[fortSize];
    }

    // Constructor that takes 3 arguments:
    public MapCreator(int fS, int fH, int lP, string p)
    {
        fortSize = fS;
        fortHostility = fH;
        lootPercentage = lP;
        path = p;
        Map = new string[fortSize];
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
        int leftWall
    }

    #endregion

}
