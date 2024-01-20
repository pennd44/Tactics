using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Tactics/LevelData", order = 0)]
public class LevelData : ScriptableObject {
    public List<GridObject> [,] _gridObjects;
    public List<GridObject> gridObjects2;
    public List<Tri> AllTris = new List<Tri>();
    public List<Vertex> AllVerts = new List<Vertex>();
    public Dictionary<int, int> TrianglesDict;// = new Dictionary<int, int>();
    public Quad FindQuadFromTriangle(int triangleIndex)
    {

        if (triangleIndex % 2 != 0)
        {
            triangleIndex -= 1;
        }
        return gridObjects2[triangleIndex/2]._quad;
    }

    // public List<Vector3> GetAllPoints(){
    //     List<Vector3> allPoints = new List<Vector3>();
    //     foreach (Quad quad in quads)
    //     {
    //         allPoints.Add(quad.First);
    //         allPoints.Add(quad.Second);
    //         allPoints.Add(quad.Third);
    //         allPoints.Add(quad.Fourth);
    //     }
    //     return allPoints;
    // }
}