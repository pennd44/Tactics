using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Quad
{
    public Vertex First;
    public Vertex Second;
    public Vertex Third;
    public Vertex Fourth;
    public Tri[] triangles = new Tri[2];
    // public int triangleIndex2;
    public Quad(Tri t1, Tri t2){
        triangles[0] = t1;
        triangles[1] = t2;
        Setup();
    }
    private void Setup(){
        First = triangles[0].cornerPoint;
        Second = triangles[0].longestEdgePoints[0];
        Second = triangles[0].longestEdgePoints[1];
        Fourth = triangles[1].cornerPoint;
    }
    public override string ToString()
    {
        return "1 : " + First + " 2: " + Second + " 3: " + Third + " 4: " + Fourth;
    }
}