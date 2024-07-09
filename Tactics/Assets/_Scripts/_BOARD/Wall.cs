using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : GridObject
{
    //A wall is a vertical plane that is a part of a tile
    public Tile2 tile;
    public GridObject bottom;
    // if either of the two tiles that share this wall are raised or lowered, the wall should be updated
    private Directions _direction;
    private float to_height;
    private float from_height;
    public Vertex[] corners = new Vertex[4];
    // public Vector3[] vertices;
    public int[] triangles = new int[6];
    public Wall(Directions direction, Vertex[] corners) : base(corners)
    {
        // triangles = new int[6] { 2, 1, 0, 3, 2, 0 };
        // this.triangles = new int[6] { 2, 1, 0, 3, 2, 0 };
        // tris[0] = new Tri(corners[2], corners[1], corners[0]);
        // tris[1] = new Tri(corners[3], corners[2], corners[0]);
        // tris[0].compliment = tris[1];
        // tris[1].compliment = tris[0];

        this._direction = direction;
        this.to_height = (corners[0].pos.y + corners[1].pos.y) / 2;
        this.from_height = (corners[2].pos.y + corners[3].pos.y) / 2;
        if (direction == Directions.North)
        {
            this.pos = new Point((int)((corners[0].pos.x + corners[1].pos.x) / 2), (int)((corners[0].pos.z + corners[1].pos.z) / 2 - 0.5));
            // this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
        }
        else if (direction == Directions.East)
        {
            this.pos = new Point((int)((corners[1].pos.x + corners[2].pos.x) / 2 - 0.5), (int)((corners[1].pos.z + corners[2].pos.z) / 2));
            // this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
        }
        else if (direction == Directions.South)
        {
            this.pos = new Point((int)((corners[2].pos.x + corners[3].pos.x) / 2), (int)((corners[2].pos.z + corners[3].pos.z) / 2 + 0.5));
            // this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
        }
        else if (direction == Directions.West)
        {
            this.pos = new Point((int)((corners[3].pos.x + corners[0].pos.x) / 2 + 0.5), (int)((corners[3].pos.z + corners[0].pos.z) / 2));
            // this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
        }
        this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };

        tris[0] = new Tri(corners[0], corners[1], corners[2]);
        tris[1] = new Tri(corners[0], corners[2], corners[3]);
        tris[0].compliment = tris[1];
        tris[1].compliment = tris[0];
    }

}
