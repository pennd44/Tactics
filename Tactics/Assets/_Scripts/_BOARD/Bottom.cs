using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : GridObject
{
    public Bottom(Vertex[] corners) : base(corners)
    {
                this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
    }
}
