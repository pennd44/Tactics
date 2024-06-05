using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Tile2{
    public int id;
    public Point point = new Point(0,0);
    public float height;
    public float thickness;
    public Vector3[] vertices ;
    public int[] triangles;
    TileShapes shape;
    List<Vertex> verticesList = new List<Vertex>();
    List<Tri> trisList = new List<Tri>();

    //bools for whether to render walls
    
    public Tile2(Point point, float height, float thickness, TileShapes shape = TileShapes.Square){
        this.point = point;
        this.height = height;
        this.thickness = thickness;
        this.shape = shape;
        Setup();
    }

    // vertices and triangles depend on shape and neighboring tiles
    public void Setup(){
        if( shape == TileShapes.Square){
            vertices = new Vector3[24];
            triangles = new int[36];
            vertices[0] = new Vector3((float)(point.x -.5), height, (float)(point.y - .5));
            vertices[1] = new Vector3((float)(point.x + .5), height, (float)(point.y - .5));
            vertices[2] = new Vector3((float)(point.x -.5), height, (float)(point.y + .5));
            vertices[3] = new Vector3((float)(point.x + .5), height, (float)(point.y + .5));

            vertices[4] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y - .5));
            vertices[5] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y - .5));
            vertices[6] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y + .5));
            vertices[7] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y + .5));

            vertices[8] = new Vector3((float)(point.x -.5), height, (float)(point.y - .5));
            vertices[9] = new Vector3((float)(point.x + .5), height, (float)(point.y - .5));
            vertices[10] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y - .5));
            vertices[11] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y - .5));

            vertices[12] = new Vector3((float)(point.x + .5), height, (float)(point.y - .5));
            vertices[13] = new Vector3((float)(point.x + .5), height, (float)(point.y + .5));
            vertices[14] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y - .5));
            vertices[15] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y + .5));

            vertices[16] = new Vector3((float)(point.x + .5), height, (float)(point.y + .5));
            vertices[17] = new Vector3((float)(point.x -.5), height, (float)(point.y + .5));
            vertices[18] = new Vector3((float)(point.x + .5), height - thickness, (float)(point.y + .5));
            vertices[19] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y + .5));

            vertices[20] = new Vector3((float)(point.x -.5), height, (float)(point.y + .5));
            vertices[21] = new Vector3((float)(point.x -.5), height, (float)(point.y - .5));
            vertices[22] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y + .5));
            vertices[23] = new Vector3((float)(point.x -.5), height - thickness, (float)(point.y - .5));
            
            //Top
            triangles[0] = 0;
            triangles[1] = 2;
            triangles[2] = 1;
            triangles[3] = 2;
            triangles[4] = 3;
            triangles[5] = 1;

            //Left wall
            triangles[6] = 0;
            triangles[7] = 4;
            triangles[8] = 2;
            triangles[9] = 4;
            triangles[10] = 6;
            triangles[11] = 2;

            //Right wall
            triangles[12] = 1;
            triangles[13] = 3;
            triangles[14] = 5;
            triangles[15] = 3;
            triangles[16] = 7;
            triangles[17] = 5;

            //Front wall
            triangles[18] = 2;
            triangles[19] = 6;
            triangles[20] = 3;
            triangles[21] = 6;
            triangles[22] = 7;
            triangles[23] = 3;

            //Back wall
            triangles[24] = 0;
            triangles[25] = 1;
            triangles[26] = 4;
            triangles[27] = 1;
            triangles[28] = 5;
            triangles[29] = 4;

            //Bottom
            triangles[30] = 4;
            triangles[31] = 5;
            triangles[32] = 6;
            triangles[33] = 5;
            triangles[34] = 7;
            triangles[35] = 6;

        }
        else if( shape == TileShapes.Triangle){
            vertices = new Vector3[3];
            triangles = new int[3];
            vertices[0] = new Vector3((float)(point.x -.5), height, (float)(point.y - .5));
            vertices[1] = new Vector3((float)(point.x + .5), height, (float)(point.y - .5));
            vertices[2] = new Vector3((float)(point.x), height, (float)(point.y + .5));
            triangles[0] = 0;
            triangles[1] = 2;
            triangles[2] = 1;
        }
    }

}