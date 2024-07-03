using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    private Mesh mesh;
    private int[] triangles;
    private Vector3[] vertices;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateGrid();
        UpdateMesh();
    }

    //given a list of grid objects, containing only tile2s and bottoms, determine where there should be walls


    // List<Tile2> dummyData = new List<Tile2>();
    List<Tile2>[,] tileGrid = new List<Tile2>[10, 10];
   public List<GridObject>[,] dummyData2 = new List<GridObject>[10, 10];
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
    public void GenerateDummyData2()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                float height = UnityEngine.Random.Range(0.0f, 4.0f);
                float bottomHeight = height - UnityEngine.Random.Range(1.0f, 3.0f);
                dummyData2[i, j] = new List<GridObject>();
                tileGrid[i, j] = new List<Tile2>();
                Vertex[] corners = new Vertex[4];
                float heightA = UnityEngine.Random.Range(height - 1.0f, height + 1.0f);
                float heightB = UnityEngine.Random.Range(height - 1.0f, height + 1.0f);
                float[] heights = new float[4];
                heights[0] = heightA;
                heights[1] = heightA;
                heights[2] = heightB;
                heights[3] = heightB;
                int randomIndex = UnityEngine.Random.Range(0, 3);


                corners[0] = new Vertex(new Vector3((float)(i - .5), heights[randomIndex], (float)(j - .5)));
                corners[1] = new Vertex(new Vector3((float)(i + .5), heights[(randomIndex + 1) % 4], (float)(j - .5)));
                corners[2] = new Vertex(new Vector3((float)(i + .5), heights[(randomIndex + 2) % 4], (float)(j + .5)));
                corners[3] = new Vertex(new Vector3((float)(i - .5), heights[(randomIndex + 3) % 4], (float)(j + .5)));
                Tile2 tile = new Tile2(corners);
                dummyData2[i, j].Add(tile);
                tileGrid[i, j].Add(tile);
                Vertex[] corners2 = new Vertex[4];
                corners2[0] = new Vertex(new Vector3((float)(i - .5), bottomHeight, (float)(j - .5)));
                corners2[1] = new Vertex(new Vector3((float)(i + .5), bottomHeight, (float)(j - .5)));
                corners2[2] = new Vertex(new Vector3((float)(i + .5), bottomHeight, (float)(j + .5)));
                corners2[3] = new Vertex(new Vector3((float)(i - .5), bottomHeight, (float)(j + .5)));
                Bottom bottom = new Bottom(corners2);
                dummyData2[i, j].Add(bottom);
                tile.bottom = bottom;

                float height2 = 10 + UnityEngine.Random.Range(0, 4);
                float bottomHeight2 = height2 - UnityEngine.Random.Range(1, 3);
                Vertex[] corners3 = new Vertex[4];
                corners3[0] = new Vertex(new Vector3((float)(i - .5), height2, (float)(j - .5)));
                corners3[1] = new Vertex(new Vector3((float)(i + .5), height2, (float)(j - .5)));
                corners3[2] = new Vertex(new Vector3((float)(i + .5), height2, (float)(j + .5)));
                corners3[3] = new Vertex(new Vector3((float)(i - .5), height2, (float)(j + .5)));
                Tile2 tile2 = new Tile2(corners3);
                dummyData2[i, j].Add(tile2);
                tileGrid[i, j].Add(tile2);
                Vertex[] corners4 = new Vertex[4];
                corners4[0] = new Vertex(new Vector3((float)(i - .5), bottomHeight2, (float)(j - .5)));
                corners4[1] = new Vertex(new Vector3((float)(i + .5), bottomHeight2, (float)(j - .5)));
                corners4[2] = new Vertex(new Vector3((float)(i + .5), bottomHeight2, (float)(j + .5)));
                corners4[3] = new Vertex(new Vector3((float)(i - .5), bottomHeight2, (float)(j + .5)));
                Bottom bottom2 = new Bottom(corners4);
                dummyData2[i, j].Add(bottom2);
                tile2.bottom = bottom2;

            }
        }
    }
    void AddWalls()
    {
        for (int i = 0; i < tileGrid.GetLength(0); i++)
        {
            for (int j = 0; j < tileGrid.GetLength(1); j++)
            {
                for (int k = 0; k < tileGrid[i, j].Count; k++)
                {
                    for (int l = 0; l < 4; l++)
                    {
                        GenerateWall(tileGrid[i, j][k], (Directions)l);
                    }
                }
            }
        }
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
    void GenerateGrid()
    {
        GenerateDummyData2();
        AddWalls();
        int triangleCount = 0;
        int vertexCount = 0;
        List<Vector3> verticesList = new List<Vector3>();
        List<int> trianglesList = new List<int>();

        for (int i = 0; i < dummyData2.GetLength(0); i++)
        {
            for (int j = 0; j < dummyData2.GetLength(1); j++)
            {
                for (int k = 0; k < dummyData2[i, j].Count; k++)
                {
                    for (int l = 0; l < 4; l++)
                    {
                        verticesList.Add(dummyData2[i, j][k].corners[l].pos);
                    }
                    for (int l = 0; l < 6; l++)
                    {
                        trianglesList.Add(vertexCount + dummyData2[i, j][k].triangles[l]);
                    }
                    vertexCount += dummyData2[i, j][k].corners.Length;
                    triangleCount += dummyData2[i, j][k].triangles.Length;
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
    void UpdateMesh()
    {
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
                for (int k = 0; k < dummyData2[i, j].Count; k++)
                {
                    if (dummyData2[i, j][k].pos == pos && dummyData2[i, j][k] is Tile2)
                    {
                        tile2s.Add((Tile2)dummyData2[i, j][k]);
                    }
                }
            }
        }
        return tile2s;
    }
    // do we need this?
    List<Tile2> GetNeighbors(Tile2 tile)
    {
        List<Tile2> neighbors = new List<Tile2>();
        Point[] directions = new Point[4];
        //north
        directions[0] = new Point(0, 1);
        //east
        directions[1] = new Point(1, 0);
        //south
        directions[2] = new Point(0, -1);
        //west
        directions[3] = new Point(-1, 0);
        for (int i = 0; i < directions.Length; i++)
        {
            Point neighborPos = tile.pos + directions[i];
            List<Tile2> neighborTiles = GetTile2s(neighborPos);
            if (neighborTiles.Count > 0)
            {
                neighbors.AddRange(neighborTiles);
            }
        }
        return neighbors;
    }

    bool ShouldGenerateWall(Tile2 tile, Tile2 neighbour)
    {
        // if (neighbour == null)
        // {
        //     return true;
        // }
        if (tile.height == neighbour.height)
        {
            //later will be based on corner heights
            return false;
        }
        if (tile.height < neighbour.height && tile.bottom.height >= neighbour.bottom.height)
        {
            return false;
        }
        // if (tile.height > neighbour.height && tile.bottom.height < neighbour.height)
        // {
        //     return true;
        // }
        // if (tile.height > neighbour.height && tile.bottom.height > neighbour.height)
        // {
        //     // wall from tile to tile.bottom
        //     return true;
        // }

        return true;
    }

    // bool WallToptoBottom(Tile2 tile, Tile2 neighbour)
    // {
    //     if (tile.bottom.height > neighbour.height)
    //     {
    //         return true;
    //     }
    //     return false;
    // }
    //if there are no more neighbors  in that directionand no wall was generated, generate wall

    // void GenerateWall(Tile2 tile, Directions direction)
    // {
    //     Point neighborPos = tile.pos + direction.ToPoint();
    //     List<Tile2> neighbors = GetTile2s(neighborPos);
    //     bool noWalls = false;
    //     for (int i = 0; i < neighbors.Count; i++)
    //     {
    //         if (tile.height <= neighbors[i].height && tile.bottom.height >= neighbors[i].bottom.height)
    //         {
    //             noWalls = true;
    //             return;
    //         }
    //     }
    //     if (!noWalls)
    //     {
    //         Vertex[] wallVertices = new Vertex[4];
    //         if (direction == Directions.North)
    //         {
    //             wallVertices[0] = tile.bottom.corners[3];
    //             wallVertices[1] = tile.bottom.corners[2];
    //             wallVertices[2] = tile.corners[2];
    //             wallVertices[3] = tile.corners[3];
    //         }
    //         else if (direction == Directions.East)
    //         {
    //             wallVertices[0] = tile.bottom.corners[2];
    //             wallVertices[1] = tile.bottom.corners[1];
    //             wallVertices[2] = tile.corners[1];
    //             wallVertices[3] = tile.corners[2];
    //         }
    //         else if (direction == Directions.South)
    //         {
    //             wallVertices[0] = tile.bottom.corners[1];
    //             wallVertices[1] = tile.bottom.corners[0];
    //             wallVertices[2] = tile.corners[0];
    //             wallVertices[3] = tile.corners[1];
    //         }
    //         else if (direction == Directions.West)
    //         {
    //             wallVertices[0] = tile.bottom.corners[0];
    //             wallVertices[1] = tile.bottom.corners[3];
    //             wallVertices[2] = tile.corners[3];
    //             wallVertices[3] = tile.corners[0];
    //         }

    //         for (int i = 0; i < neighbors.Count; i++)
    //         {
    //             if (tile.height > neighbors[i].height && tile.bottom.height < neighbors[i].height)
    //             {
    //                 //generate wall
    //                 if (direction == Directions.North)
    //                 {
    //                     wallVertices[0] = neighbors[i].corners[0];
    //                     wallVertices[1] = neighbors[i].corners[1];
    //                     wallVertices[2] = tile.corners[2];
    //                     wallVertices[3] = tile.corners[3];
    //                     break;
    //                 }
    //                 else if (direction == Directions.East)
    //                 {
    //                     wallVertices[0] = neighbors[i].corners[3];
    //                     wallVertices[1] = neighbors[i].corners[0];
    //                     wallVertices[2] = tile.corners[1];
    //                     wallVertices[3] = tile.corners[2];
    //                     break;
    //                 }
    //                 else if (direction == Directions.South)
    //                 {
    //                     wallVertices[0] = neighbors[i].corners[2];
    //                     wallVertices[1] = neighbors[i].corners[3];
    //                     wallVertices[2] = tile.corners[0];
    //                     wallVertices[3] = tile.corners[1];
    //                     break;
    //                 }
    //                 else if (direction == Directions.West)
    //                 {
    //                     wallVertices[0] = neighbors[i].corners[1];
    //                     wallVertices[1] = neighbors[i].corners[2];
    //                     wallVertices[2] = tile.corners[3];
    //                     wallVertices[3] = tile.corners[0];
    //                     break;
    //                 }
    //             }
    //         }
    //         //add these verts to the vertices list
    //         Wall wall = new Wall(direction, wallVertices);
    //         tile.walls.Add(wall);
    //         dummyData2[tile.pos.x, tile.pos.y].Add(wall);
    //     }
    // }
    int[][] vertexIndices = new int[][]
    {
        new int[] { 3, 2, 2, 3 }, //north
        new int[] { 2, 1, 1, 2 }, //east
        new int[] { 1, 0, 0, 1 }, //south
        new int[] { 0, 3, 3, 0 } //west
    };

    void GenerateWall(Tile2 tile, Directions direction)
    {
        Point neighborPos = tile.pos + direction.ToPoint();
        List<Tile2> neighbors = GetTile2s(neighborPos);
        bool noWalls = false;
        int[] indices = vertexIndices[(int)direction];
        int neighborDirection = ((int)direction + 2) % 4;
        int[] neighborIndices = vertexIndices[neighborDirection];
        for (int i = 0; i < neighbors.Count; i++)
        {
            // if (tile.height <= neighbors[i].height && tile.bottom.height >= neighbors[i].bottom.height)
            if (tile.corners[indices[0]].pos.y <= neighbors[i].corners[neighborIndices[1]].pos.y &&
            tile.corners[indices[1]].pos.y <= neighbors[i].corners[neighborIndices[0]].pos.y &&
            tile.bottom.corners[indices[0]].pos.y >= neighbors[i].bottom.corners[neighborIndices[1]].pos.y &&
            tile.bottom.corners[indices[1]].pos.y >= neighbors[i].bottom.corners[neighborIndices[0]].pos.y)
            {
                noWalls = true;
                return;
            }
        }
        if (!noWalls)
        {
            Vertex[] wallVertices = new Vertex[4];

            wallVertices[0] = tile.bottom.corners[indices[0]];
            wallVertices[1] = tile.bottom.corners[indices[1]];
            wallVertices[2] = tile.corners[indices[2]];
            wallVertices[3] = tile.corners[indices[3]];

            for (int i = 0; i < neighbors.Count; i++)
            {
                // if (tile.height > neighbors[i].height && tile.bottom.height < neighbors[i].height)
                if (tile.corners[indices[0]].pos.y > neighbors[i].corners[neighborIndices[1]].pos.y &&
                tile.corners[indices[1]].pos.y > neighbors[i].corners[neighborIndices[0]].pos.y &&
                tile.bottom.corners[indices[0]].pos.y < neighbors[i].corners[neighborIndices[1]].pos.y &&
                tile.bottom.corners[indices[1]].pos.y < neighbors[i].corners[neighborIndices[0]].pos.y
                )
                {
                    //generate wall
                    wallVertices[0] = neighbors[i].corners[neighborIndices[1]];
                    wallVertices[1] = neighbors[i].corners[neighborIndices[0]];
                    wallVertices[2] = tile.corners[indices[2]];
                    wallVertices[3] = tile.corners[indices[3]];
                    break;
                }
            }
            //add these verts to the vertices list
            Wall wall = new Wall(direction, wallVertices);
            tile.walls.Add(wall);
            dummyData2[tile.pos.x, tile.pos.y].Add(wall);
        }
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