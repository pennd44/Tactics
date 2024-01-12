using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Tactics/LevelData", order = 0)]
public class LevelData : ScriptableObject {
    public List<GridObject> [,] _gridObjects;
    // public List<Quad> quads;
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