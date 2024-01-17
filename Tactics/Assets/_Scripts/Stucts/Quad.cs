using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public struct Quad
{
    public Vector3 First;
    public Vector3 Second;
    public Vector3 Third;
    public Vector3 Fourth;
    // public Tri[] triangles = new Tri[2];
    // public int triangleIndex2;
    public Quad(Vector3 first, Vector3 second, Vector3 third, Vector3 fourth){
        // triangles[0] = t1;
        // triangles[1] = t2;

        this.First = first;
        this.Second = second;
        this.Third = third;
        this.Fourth = fourth;
    }
    public override string ToString()
    {
        return "1 : " + First + " 2: " + Second + " 3: " + Third + " 4: " + Fourth;
    }
}