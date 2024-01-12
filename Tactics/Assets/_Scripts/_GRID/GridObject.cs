using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    // private GridSystem _gridSystem;
    private Point _point;
    private Quad _quad;
    public GridObject ( Point point, Quad quad)
    {
        // this._gridSystem = gridSystem;
        this._point = point;
        this._quad = quad;
    }
}
