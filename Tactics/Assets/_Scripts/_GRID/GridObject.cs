using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class GridObject
{
    // private GridSystem _gridSystem;
    private Point _point;
    private Quad _quad;
    public GridObject ( Point point, Quad quad)
    {
        // this._gridSystem = gridSystem;
        Debug.Log("created go " + quad);
        this._point = point;
        this._quad = quad;
    }
    public Vector3 Center(){
        int x = (int)(( _quad.First.x + 
                        _quad.Second.x +
                        _quad.Third.x +
                        _quad.Fourth.x) / 4);
        int y = (int)((  _quad.First.y + 
                        _quad.Second.y +
                        _quad.Third.y +
                        _quad.Fourth.y) / 4);
        int z = (int)((  _quad.First.z + 
                        _quad.Second.z +
                        _quad.Third.z +
                        _quad.Fourth.z) / 4);
        return new Vector3(x, y, z);
    }
}
