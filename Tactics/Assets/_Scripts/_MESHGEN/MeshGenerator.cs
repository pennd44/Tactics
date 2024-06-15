using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    private Mesh mesh;
    private int [] triangles;
    private Vector3[] vertices;
    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateGrid();
        UpdateMesh();
    }
    // List<Tile2> dummyData = new List<Tile2>();
    List<GridObject> [,] dummyData2 = new List<GridObject>[10,10];
    // [ContextMenu("generate dummy data")]
    // public void GenerateDummyData(){
    //     for (int i = 0; i < 10; i++)
    //     {
    //         for (int j = 0; j < 10; j++)
    //         {
    //             dummyData.Add(new Tile2(new Point(i,j), 0, 2));
    //             dummyData.Add(new Tile2(new Point(i,j), 3, 2, TileShapes.Triangle));
    //         }
    //     }
    // }
    public void GenerateDummyData2(){
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                dummyData2[i,j] = new List<GridObject>();
                Vector3 [] corners = new Vector3[4];
                corners[0] = new Vector3((float)(i-.5), 1, (float)(j-.5));
                corners[1] = new Vector3((float)(i+.5), 1, (float)(j-.5));
                corners[2] = new Vector3((float)(i+.5), 1, (float)(j+.5));
                corners[3] = new Vector3((float)(i-.5), 1, (float)(j+.5));
                Tile2 tile = new Tile2(corners);
                dummyData2[i,j].Add(tile);
                Vector3 [] corners2 = new Vector3[4];
                corners2[0] = new Vector3((float)(i-.5), -1, (float)(j-.5));
                corners2[1] = new Vector3((float)(i+.5), -1, (float)(j-.5));
                corners2[2] = new Vector3((float)(i+.5), -1, (float)(j+.5));
                corners2[3] = new Vector3((float)(i-.5), -1, (float)(j+.5));
                Bottom bottom = new Bottom(corners2);
                dummyData2[i,j].Add(bottom);
                tile.bottom = bottom;
                //vertical walls
                // Vector3 [] corners3 = new Vector3[4];
                // corners3[0] = new Vector3((float)(i-.5), 1, (float)(j-.5));
                // corners3[1] = new Vector3((float)(i+.5), 1, (float)(j-.5));
                // corners3[2] = new Vector3((float)(i+.5), -1, (float)(j-.5));
                // corners3[3] = new Vector3((float)(i-.5), -1, (float)(j-.5));
                // Wall wall = new Wall(Directions.North, corners3);
                // dummyData2[i,j].Add(wall);
                // tile.walls.Add(wall);
                // Vector3 [] corners4 = new Vector3[4];
                // corners4[0] = new Vector3((float)(i+.5), 1, (float)(j-.5));
                // corners4[1] = new Vector3((float)(i+.5), 1, (float)(j+.5));
                // corners4[2] = new Vector3((float)(i+.5), -1, (float)(j+.5));
                // corners4[3] = new Vector3((float)(i+.5), -1, (float)(j-.5));
                // Wall wall2 = new Wall(Directions.East, corners4);
                // dummyData2[i,j].Add(wall2);
                // tile.walls.Add(wall2);
                // Vector3 [] corners5 = new Vector3[4];
                // corners5[0] = new Vector3((float)(i+.5), 1, (float)(j+.5));
                // corners5[1] = new Vector3((float)(i-.5), 1, (float)(j+.5));
                // corners5[2] = new Vector3((float)(i-.5), -1, (float)(j+.5));
                // corners5[3] = new Vector3((float)(i+.5), -1, (float)(j+.5));
                // Wall wall3 = new Wall(Directions.South, corners5);
                // dummyData2[i,j].Add(wall3);
                // tile.walls.Add(wall3);
                // Vector3 [] corners6 = new Vector3[4];
                // corners6[0] = new Vector3((float)(i-.5), 1, (float)(j+.5));
                // corners6[1] = new Vector3((float)(i-.5), 1, (float)(j-.5));
                // corners6[2] = new Vector3((float)(i-.5), -1, (float)(j-.5));
                // corners6[3] = new Vector3((float)(i-.5), -1, (float)(j+.5));
                // Wall wall4 = new Wall(Directions.West, corners6);
                // dummyData2[i,j].Add(wall4);
                // tile.walls.Add(wall4);
            }
        }
    }
    void AddWalls(){
        f
    }
    //differentiate between grid and graphics; grid is the data structure, graphics is the visual representation; grid first, graphics second based on grid
    // void SetUpTile2s(){
    //     for (int i = 0; i < 10; i++)
    //     {
    //         for (int j = 0; j < 10; j++)
    //         {
    //             dummyData2[i,j] = new List<Tile2>();
    //             dummyData2[i,j].Add(new Tile2(new Point(i,j), 0, 2));
    //             dummyData2[i,j].Add(new Tile2(new Point(i,j), 3, 2, TileShapes.Triangle));
    //         }
    //     }
    // }
    void GenerateGrid(){
        GenerateDummyData2();
        int triangleCount = 0;
        int vertexCount = 0;
        List<Vector3> verticesList = new List<Vector3>();
        List<int> trianglesList = new List<int>();
        
        for (int i = 0; i < dummyData2.GetLength(0); i++)
        {
            for (int j = 0; j < dummyData2.GetLength(1); j++)
            {
                for (int k = 0; k < dummyData2[i,j].Count; k++)
                {
                    for (int l = 0; l < 4; l++)
                    {
                        verticesList.Add(dummyData2[i,j][k].corners[l]);
                    }
                    for (int l = 0; l < 6; l++)
                    {
                        trianglesList.Add(vertexCount + dummyData2[i,j][k].triangles[l]);
                    }
                    vertexCount+= dummyData2[i,j][k].corners.Length;
                    triangleCount+= dummyData2[i,j][k].triangles.Length;
                }
            }
        }
        vertices = verticesList.ToArray();
        triangles = trianglesList.ToArray();







        // // for (int i = 0; i < dummyData2.Count; i++)
        // {
        //     for (int j = 0; j < 4; j++)
        //     {
        //         verticesList.Add(dummyData2[i].corners[j]);
        //     }
        //     // if(dummyData2[i].thickness < int.MaxValue)
        //     // {
        //     //     dummyData2[i].bottom = new Bottom();
        //     //     //expand
        //     // }
        //     //if has bottom, add bottom vertices
        //     if(dummyData2[i].bottom != null)
        //     {
        //         for (int j = 0; j < 4; j++)
        //         {
        //             verticesList.Add(dummyData2[i].bottom.vertices[j]);
        //         }
        //     }
        //     List<Tile2> [] neighbors = new List<Tile2>[4];
        //     //neighbors[0] = FindNeighborTile by pos
        //     //neighbors[1] = FindNeighborTile by pos
        //     //neighbors[2] = FindNeighborTile by pos
        //     //neighbors[3] = FindNeighborTile by pos
        //     if(neighbors[0].Count > 0)
        //     {
        //         for (int j = 0; j < neighbors[0].Count; j++)
        //         {





        //             //This is where ill pick up tomorrow
        //             if(neighbors[0][j].bottom.height > dummyData2[i].height)
        //             {
        //                 continue;
        //             }
                    
        //             if(neighbors[0][j].height < dummyData2[i].height)
        //             {
        //                 dummyData2[i].walls.Add(new Wall());
        //                 //expand
        //             }






        //         }
        //     }
        //     //FindNeighborTiles by pos
        //     //if has walls, add wall vertices
        //     if(dummyData2[i].walls.Count > 0)
        //     {
        //         for (int j = 0; j < dummyData2[i].walls.Count; j++)
        //         {
        //             for (int k = 0; k < 4; k++)
        //             {
        //                 verticesList.Add(dummyData2[i].walls[j].vertices[k]);
        //             } 
        //         }
        //     }
            
        //     //later, will remove triangles from generateGrid and put them in generateMesh
        //     for (int j = 0; j < 6; j++)
        //     {
        //         trianglesList.Add(vertexCount + dummyData2[i].triangles[j]);
        //     }
        //     if(dummyData2[i].bottom != null)
        //     {
        //         for (int j = 0; j < 6; j++)
        //         {
        //             trianglesList.Add(vertexCount + dummyData2[i].bottom.triangles[j]);
        //         }
        //     }
        //     if(dummyData2[i].walls.Count > 0)
        //     {
        //         for (int j = 0; j < dummyData2[i].walls.Count; j++)
        //         {
        //             for (int k = 0; k < 6; k++)
        //             {
        //                 trianglesList.Add(vertexCount + dummyData2[i].walls[j].triangles[k]);
        //             } 
        //         }
        //     }



        //     //if has bottom, add bottom triangles
        //     //if has walls, add wall triangles

        //     //find neighbor tiles by pos; will be a list
        //     vertexCount+= dummyData2[i].vertices.Length;
        //     triangleCount+= dummyData2[i].triangles.Length;
        // }
        // vertices = verticesList.ToArray();
        // triangles = trianglesList.ToArray();
    }
    // void GenerateMesh(){
        
    //     GenerateDummyData();
    //     int triangleCount = 0;
    //     int vertexCount = 0;
    //     List<Vector3> verticesList = new List<Vector3>();
    //     List<int> trianglesList = new List<int>();
    //     for (int i = 0; i < dummyData.Count; i++)
    //     {
    //         for (int j = 0; j < dummyData[i].vertices.Length; j++)
    //         {
    //             verticesList.Add(dummyData[i].vertices[j]);
    //         }
    //         for (int j = 0; j < dummyData[i].triangles.Length; j++)
    //         {
    //             trianglesList.Add(vertexCount + dummyData[i].triangles[j]);
    //         }
    //         vertexCount+= dummyData[i].vertices.Length;
    //         triangleCount+= dummyData[i].triangles.Length;
    //     }
    //     vertices = verticesList.ToArray();
    //     triangles = trianglesList.ToArray();
    // }
    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    List<Tile2> GetTile2s(Point pos)
    {
        List<Tile2> tile2s = new List<Tile2>();
        for (int i = 0; i < dummyData2.GetLength(0); i++)
        {
            for (int j = 0; j < dummyData2.GetLength(1); j++)
            {
                for (int k = 0; k < dummyData2[i,j].Count; k++)
                {
                    if(dummyData2[i,j][k].pos == pos && dummyData2[i,j][k] is Tile2)
                    {
                        tile2s.Add((Tile2)dummyData2[i, j][k]);
                    }
                }
            }
        }
        return tile2s;
    }
    List<GridObject> GetNeighbors(Tile2 tile)
    {
        List<GridObject> neighbors = new List<GridObject>();
        Point [] directions = new Point[4];
        //north
        directions[0] = new Point(0,1);
        //east
        directions[1] = new Point(1,0);
        //south
        directions[2] = new Point(0,-1);
        //west
        directions[3] = new Point(-1,0);
        for (int i = 0; i < directions.Length; i++)
        {
            Point neighborPos = tile.pos + directions[i];
            List<Tile2> neighborTiles = GetTile2s(neighborPos);
            if(neighborTiles.Count > 0)
            {
                neighbors.AddRange(neighborTiles);
            }
        }
        return neighbors;
    }

    bool ShouldGenerateWall(Tile2 tile, Tile2 neighbour){
        if(neighbour == null){
            return true;
        }
        if(tile.height == neighbour.height){
            //later will be based on corner heights
            return false;
        }
        return true;
    }

    bool WallToptoBottom(Tile2 tile, Tile2 neighbour){
        if(tile.height - tile.thickness > neighbour.height){
            return true;
        }
        return false;
    }
    void GenerateWall(Tile2 tile, Tile2 neighbour){
        if(!ShouldGenerateWall(tile, neighbour)){
            return;
        }
        Vector3 [] vertices = new Vector3[4];
        if(WallToptoBottom(tile, neighbour)){
            //generate wall
            vertices[0] = tile.vertices[0];
            vertices[1] = tile.vertices[1];
            vertices[2] = tile.bottom.vertices[0];
            vertices[3] = tile.bottom.vertices[1];
        }
        else{
            //generate wall
            vertices[0] = tile.vertices[1];
            vertices[1] = tile.vertices[2];
            vertices[2] = neighbour.vertices[1];
            vertices[3] = neighbour.vertices[2];
        }
        //generate wall
    

    }

}

 // void GenerateQuad(Quad quad){
    //     Mesh mesh = new Mesh();
    //     // int [] triangles;
    //     // Vector3 [] vertices;

    // }
    // void GenerateMap(List<Quad> quads){
    //     foreach (Quad quad in quads)
    //     {
    //         GenerateQuad(quad);
    //     }
    // }
    // [ContextMenu("load grid objects")]
    // public void LoadGridObjects()
    // {
    //     levelData._gridObjects = new List<GridObject>[levelData.x, levelData.y];
    //     foreach (GridObject go in levelData.gridObjects2)
    //     {
    //         if (levelData._gridObjects[go._point.x, go._point.y] == null)
    //             levelData._gridObjects[go._point.x, go._point.y] = new List<GridObject>();
    //         levelData._gridObjects[go._point.x, go._point.y].Add(go);
    //     }
    // }
    // [ContextMenu("load tile2s")]
    // public void LoadTile2s()
    // {
    //     levelData._tiles = new List<Tile2>[levelData.x, levelData.y];
    //     foreach (Tile2 t in levelData.tiles2)
    //     {
    //         if (levelData._tiles[t.point.x, t.point.y] == null)
    //             levelData._tiles[t.point.x, t.point.y] = new List<Tile2>();
    //         levelData._tiles[t.point.x, t.point.y].Add(t);
    //     }
    // }