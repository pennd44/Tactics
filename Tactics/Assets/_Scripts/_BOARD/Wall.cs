using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : GridObject
{
    //A wall is a vertical plane that is a part of a tile
    private Directions _direction;
    private float to_height;
    private float from_height;
    public Vector3[] corners = new Vector3[4];
    // public Vector3[] vertices;
    public int[] triangles = new int[6];
    public Wall(Directions direction, Vector3[] corners) : base(corners)
    {
        triangles = new int[6] { 2, 1, 0, 3, 2, 0 };

        this._direction = direction;
        this.to_height = (corners[0].y + corners[1].y) / 2;
        this.from_height = (corners[2].y + corners[3].y) / 2;
        if (direction == Directions.North)
        {
            this.pos = new Point((int)((corners[0].x + corners[1].x) / 2), (int)((corners[0].z + corners[1].z) / 2 - 0.5));
            // this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
        }
        else if (direction == Directions.East)
        {
            this.pos = new Point((int)((corners[1].x + corners[2].x) / 2 - 0.5), (int)((corners[1].z + corners[2].z) / 2));
            // this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
        }
        else if (direction == Directions.South)
        {
            this.pos = new Point((int)((corners[2].x + corners[3].x) / 2), (int)((corners[2].z + corners[3].z) / 2 + 0.5));
            // this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
        }
        else if (direction == Directions.West)
        {
            this.pos = new Point((int)((corners[3].x + corners[0].x) / 2 + 0.5), (int)((corners[3].z + corners[0].z) / 2));
            // this.triangles = new int[6] { 0, 1, 2, 0, 2, 3 };
        }
    }
    public bool ShouldRender(Tile2 neighbor)
    {
        return to_height != from_height;
    }
    //when a shared vertice on a neighbor tile is moved, the wall should be updated
    public void Setup()
    {

    }
}
