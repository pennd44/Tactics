using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Tile2 : GridObject
{

    //TODO: seperate functional vertices from rendering vertices
    public int id;
    public float thickness;

    TileShapes shape;

    public List<Wall> walls = new List<Wall>();
    public Bottom bottom;
    public List<Tile2> neighbors = new List<Tile2>();

    public Tile2(Vertex[] corners) : base(corners)
    {
        // triangles = new int[6] { 2, 1, 0, 3, 2, 0 };
        this.triangles = new int[6] { 2, 1, 0, 3, 2, 0 };
        tris[0] = new Tri(corners[2], corners[1], corners[0]);
        tris[1] = new Tri(corners[3], corners[2], corners[0]);
        tris[0].compliment = tris[1];
        tris[1].compliment = tris[0];
    }
    public void Setup(Wall[] walls, Tile2[] neighbors, Bottom bottom)
    {
        this.walls = new List<Wall>(walls);
        this.neighbors = new List<Tile2>(neighbors);
        this.bottom = bottom;
        //INCOMPLETE

    }
    public override void InitializeTriangles()
    {
        triangles = new int[6] { 2, 1, 0, 3, 2, 0 };
    }
    public void Destroy()
    {
        //destroy walls
        //destroy bottom
        //destroy self

    }
    public void FindNeighborTiles(List<GridObject>[,] gridObjects)
    {


    }
    // should I split into north, east, south, west?
    //bools for whether to render walls

    // public Tile2(Point point, float height, float thickness, TileShapes shape = TileShapes.Square){
    //     this.point = point;
    //     this.height = height;
    //     this.thickness = thickness;
    //     this.shape = shape;
    //     Setup();
    // }

    // vertices and triangles depend on shape and neighboring tiles
    // bool renderLeftWall = false;
    // bool renderRightWall = false;
    // bool renderFrontWall = false;
    // bool renderBackWall = false;
    // bool renderTop = false;
    // bool renderBottom = false;
    // int vertexCount = 0;
    // int triCount = 0;

    // public void Setup(){
    //     if( shape == TileShapes.Square){
    //         // vertices = new Vector3[24];
    //         // triangles = new int[36];
    //         vertices = new Vector3[8];
    //         triangles = new int[12];

    //         //Top
    //         // vertices.Add(new Vector3((float)(point.x -.5), height, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x -.5), height, (float)(point.y + .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height, (float)(point.y + .5)));
    //         vertices[0] = new Vector3((float)(point.x -.5), height, (float)(point.y - .5));
    //         vertices[1] = new Vector3((float)(point.x + .5), height, (float)(point.y - .5));
    //         vertices[2] = new Vector3((float)(point.x -.5), height, (float)(point.y + .5));
    //         vertices[3] = new Vector3((float)(point.x + .5), height, (float)(point.y + .5));

    //         corners[0] = vertices[0];
    //         corners[1] = vertices[1];
    //         corners[2] = vertices[2];
    //         corners[3] = vertices[3];

    //         //Bottom
    //         // vertices.Add(new Vector3((float)(point.x -.5), height - thickness, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height - thickness, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x -.5), height - thickness, (float)(point.y + .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height - thickness, (float)(point.y + .5)));


    //         vertices[4] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y - .5));
    //         vertices[5] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y - .5));
    //         vertices[6] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y + .5));
    //         vertices[7] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y + .5));

    //         //Left wall

    //         // vertices.Add(new Vector3((float)(point.x -.5), height, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x -.5), height, (float)(point.y + .5)));
    //         // vertices.Add(new Vector3((float)(point.x -.5), height - thickness, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x -.5), height - thickness, (float)(point.y + .5)));

    //         // vertices[8] = new Vector3((float)(point.x -.5), height, (float)(point.y - .5));
    //         // vertices[9] = new Vector3((float)(point.x + .5), height, (float)(point.y - .5));
    //         // vertices[10] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y - .5));
    //         // vertices[11] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y - .5));

    //         //Right wall

    //         // vertices.Add(new Vector3((float)(point.x + .5), height, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height, (float)(point.y + .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height - thickness, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height - thickness, (float)(point.y + .5)));
    //         // vertices[12] = new Vector3((float)(point.x + .5), height, (float)(point.y - .5));
    //         // vertices[13] = new Vector3((float)(point.x + .5), height, (float)(point.y + .5));
    //         // vertices[14] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y - .5));
    //         // vertices[15] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y + .5));

    //         //Front wall
    //         // vertices.Add(new Vector3((float)(point.x -.5), height, (float)(point.y + .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height, (float)(point.y + .5)));
    //         // vertices.Add(new Vector3((float)(point.x -.5), height - thickness, (float)(point.y + .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height - thickness, (float)(point.y + .5)));
    //         // vertices[16] = new Vector3((float)(point.x + .5), height, (float)(point.y + .5));
    //         // vertices[17] = new Vector3((float)(point.x -.5), height, (float)(point.y + .5));
    //         // vertices[18] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y + .5));
    //         // vertices[19] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y + .5));

    //         //Back wall
    //         // vertices.Add(new Vector3((float)(point.x -.5), height, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x -.5), height - thickness, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height - thickness, (float)(point.y - .5)));

    //         // vertices[20] = new Vector3((float)(point.x -.5), height, (float)(point.y + .5));
    //         // vertices[21] = new Vector3((float)(point.x -.5), height, (float)(point.y - .5));
    //         // vertices[22] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y + .5));
    //         // vertices[23] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y - .5));

    //         //Top
    //         // triangles.Add(0);
    //         // triangles.Add(2);
    //         // triangles.Add(1);
    //         // triangles.Add(2);
    //         // triangles.Add(3);
    //         // triangles.Add(1);

    //         triangles[0] = 0;
    //         triangles[1] = 2;
    //         triangles[2] = 1;
    //         triangles[3] = 2;
    //         triangles[4] = 3;
    //         triangles[5] = 1;

    //         //Left wall
    //         // triangles.Add(8);
    //         // triangles.Add(10);
    //         // triangles.Add(9);
    //         // triangles.Add(10);
    //         // triangles.Add(11);
    //         // triangles.Add(9);

    //         // triangles[6] = 0;
    //         // triangles[7] = 4;
    //         // triangles[8] = 2;
    //         // triangles[9] = 4;
    //         // triangles[10] = 6;
    //         // triangles[11] = 2;

    //         //Right wall

    //         // triangles.Add(12);
    //         // triangles.Add(14);
    //         // triangles.Add(13);
    //         // triangles.Add(14);
    //         // triangles.Add(15);
    //         // triangles.Add(13);

    //         // triangles[12] = 1;
    //         // triangles[13] = 3;
    //         // triangles[14] = 5;
    //         // triangles[15] = 3;
    //         // triangles[16] = 7;
    //         // triangles[17] = 5;

    //         //Front wall
    //         // triangles.Add(16);
    //         // triangles.Add(18);
    //         // triangles.Add(17);
    //         // triangles.Add(18);
    //         // triangles.Add(19);
    //         // triangles.Add(17);

    //         // triangles[18] = 2;
    //         // triangles[19] = 6;
    //         // triangles[20] = 3;
    //         // triangles[21] = 6;
    //         // triangles[22] = 7;
    //         // triangles[23] = 3;

    //         //Back wall
    //         // triangles.Add(20);
    //         // triangles.Add(22);
    //         // triangles.Add(21);
    //         // triangles.Add(22);
    //         // triangles.Add(23);
    //         // triangles.Add(21);

    //         // triangles[24] = 0;
    //         // triangles[25] = 1;
    //         // triangles[26] = 4;
    //         // triangles[27] = 1;
    //         // triangles[28] = 5;
    //         // triangles[29] = 4;

    //         //Bottom
    //         // triangles.Add(4);
    //         // triangles.Add(6);
    //         // triangles.Add(5);
    //         // triangles.Add(6);
    //         // triangles.Add(7);
    //         // triangles.Add(5);

    //         triangles[6] = 4;
    //         triangles[7] = 5;
    //         triangles[8] = 6;
    //         triangles[9] = 5;
    //         triangles[10] = 7;
    //         triangles[11] = 6;

    //     }
    //     else if( shape == TileShapes.Triangle){
    //         vertices = new Vector3[3];
    //         triangles = new int[3];
    //         // vertices.Add(new Vector3((float)(point.x -.5), height, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x + .5), height, (float)(point.y - .5)));
    //         // vertices.Add(new Vector3((float)(point.x), height, (float)(point.y + .5)));

    //         vertices[0] = new Vector3((float)(point.x -.5), height, (float)(point.y - .5));
    //         vertices[1] = new Vector3((float)(point.x + .5), height, (float)(point.y - .5));
    //         vertices[2] = new Vector3((float)(point.x), height, (float)(point.y + .5));
    //         corners[0] = vertices[0];
    //         corners[1] = vertices[1];
    //         corners[2] = new Vector3((float)(point.x+ .5), height, (float)(point.y + .5));
    //         corners[3] = new Vector3((float)(point.x-.5), height, (float)(point.y + .5));

    //         // triangles.Add(0);
    //         // triangles.Add(2);
    //         // triangles.Add(1);
    //         triangles[0] = 0;
    //         triangles[1] = 2;
    //         triangles[2] = 1;
    //     }
    // }

}
//eventually tiles will take in arguments for each vertex instead of height and get the height from the average vertex height