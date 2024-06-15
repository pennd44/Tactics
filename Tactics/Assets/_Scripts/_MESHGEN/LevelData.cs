using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Tactics/LevelData", order = 0)]
public class LevelData : ScriptableObject {
    public int x;
    public int y;
    public List<GridObject> [,] _gridObjects;
    public List<GridObject> gridObjects2;
    public List<Tile2> [,] _tiles;
    public List<Tile2> tiles2;
    public List<Tri> AllTris = new List<Tri>();
    public List<int> AllTriangles = new List<int>();
    public List<Vertex> AllVerts = new List<Vertex>();
    public Dictionary<int, int> TrianglesDict;// = new Dictionary<int, int>();

    [ContextMenu("load grid objects")]

    //For each grid object in gridObjects2, generate a quad

    // public Quad FindQuadFromTriangle(int triangleIndex)
    // {

    //     if (triangleIndex % 2 != 0)
    //     {
    //         triangleIndex -= 1;
    //     }
    //     return gridObjects2[triangleIndex/2]._quad;
    // }

    public Vertex GetVertexFromVector3(Vector3 position)
    {
        foreach (Vertex v in AllVerts)
        {
            if (v.pos == position)
            {
                return v;
            }
        }
        return null;
    }
}