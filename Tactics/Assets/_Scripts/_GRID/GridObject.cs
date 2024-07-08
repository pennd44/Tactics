using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
public abstract class GridObject
{
    public Point pos;
    public float height;
    // public Vector3[] corners = new Vector3[4];
    public Vertex[] corners = new Vertex[4];
    public int[] triangles = new int[6];
    public Tri[] tris = new Tri[2];


    public GridObject(Vertex[] corners)
    {
        this.corners = corners;
        tris[0] = new Tri(corners[2], corners[1], corners[0]);
        tris[1] = new Tri(corners[3], corners[2], corners[0]);
        tris[0].compliment = tris[1];
        tris[1].compliment = tris[0];
        // this.height = (corners[0].y + corners[1].y + corners[2].y + corners[3].y) / 4;
        // this.pos = new Point((int)((corners[0].x + corners[1].x + corners[2].x + corners[3].x) / 4), (int)((corners[0].z + corners[1].z + corners[2].z + corners[3].z) / 4));
        this.height = (corners[0].pos.y + corners[1].pos.y + corners[2].pos.y + corners[3].pos.y) / 4;
        this.pos = new Point((int)((corners[0].pos.x + corners[1].pos.x + corners[2].pos.x + corners[3].pos.x) / 4), (int)((corners[0].pos.z + corners[1].pos.z + corners[2].pos.z + corners[3].pos.z) / 4));

        this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
    }


    public Vector3[] vertices;
}




// [Serializable]
// public class GridObject
// {
//     // private GridSystem _gridSystem;
//     public Point _point;
//     public Quad _quad;
//     public float height;
//     public GridObject ( Point point, Quad quad)
//     {
//         // this._gridSystem = gridSystem;
//         Debug.Log("created go " + quad);
//         this._point = point;
//         this._quad = quad;
//     }
//     // public Vector3 Center(){
//     //     int x = (int)(( _quad.First.x + 
//     //                     _quad.Second.x +
//     //                     _quad.Third.x +
//     //                     _quad.Fourth.x) / 4);
//     //     int y = (int)((  _quad.First.y + 
//     //                     _quad.Second.y +
//     //                     _quad.Third.y +
//     //                     _quad.Fourth.y) / 4);
//     //     int z = (int)((  _quad.First.z + 
//     //                     _quad.Second.z +
//     //                     _quad.Third.z +
//     //                     _quad.Fourth.z) / 4);
//     //     return new Vector3(x, y, z);
//     // }
// }
