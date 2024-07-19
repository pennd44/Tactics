using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : GridObject
{
    public Bottom(Vertex[] corners) : base(corners)
    {
        this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 }; 
        tris[0] = new Tri(corners[0], corners[1], corners[2]);
        tris[1] = new Tri(corners[0], corners[2], corners[3]);
        tris[0].compliment = tris[1];
        tris[1].compliment = tris[0];
    }
}
