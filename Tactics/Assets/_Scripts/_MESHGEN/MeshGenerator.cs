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
        GenerateMesh();
        UpdateMesh();
    }

    List<Tile2> dummyData = new List<Tile2>();
    [ContextMenu("generate dummy data")]
    public void GenerateDummyData(){
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                dummyData.Add(new Tile2(new Point(i,j), 0, 2));
                dummyData.Add(new Tile2(new Point(i,j), 3, 2, TileShapes.Triangle));
            }
        }
    }
    //differentiate between grid and graphics; grid is the data structure, graphics is the visual representation; grid first, graphics second based on grid
    void GenerateGrid(){
        GenerateDummyData();
        int triangleCount = 0;
        int vertexCount = 0;
        List<Vector3> verticesList = new List<Vector3>();
        List<int> trianglesList = new List<int>();
        for (int i = 0; i < dummyData.Count; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                verticesList.Add(dummyData[i].vertices[j]);
            }
            if(dummyData[i].thickness < int.MaxValue)
            {
                dummyData[i].bottom = new Bottom();
                //expand
            }
            //if has bottom, add bottom vertices
            if(dummyData[i].bottom != null)
            {
                for (int j = 0; j < 4; j++)
                {
                    verticesList.Add(dummyData[i].bottom.vertices[j]);
                }
            }
            List<Tile2> [] neighbors = new List<Tile2>[4];
            //neighbors[0] = FindNeighborTile by pos
            //neighbors[1] = FindNeighborTile by pos
            //neighbors[2] = FindNeighborTile by pos
            //neighbors[3] = FindNeighborTile by pos
            if(neighbors[0].Count > 0)
            {
                for (int j = 0; j < neighbors[0].Count; j++)
                {





                    //This is where ill pick up tomorrow
                    if(neighbors[0][j].bottom.height > dummyData[i].height)
                    {
                        continue;
                    }
                    
                    if(neighbors[0][j].height < dummyData[i].height)
                    {
                        dummyData[i].walls.Add(new Wall());
                        //expand
                    }






                }
            }
            //FindNeighborTiles by pos
            //if has walls, add wall vertices
            if(dummyData[i].walls.Count > 0)
            {
                for (int j = 0; j < dummyData[i].walls.Count; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        verticesList.Add(dummyData[i].walls[j].vertices[k]);
                    } 
                }
            }
            
            //later, will remove triangles from generateGrid and put them in generateMesh
            for (int j = 0; j < 6; j++)
            {
                trianglesList.Add(vertexCount + dummyData[i].triangles[j]);
            }
            if(dummyData[i].bottom != null)
            {
                for (int j = 0; j < 6; j++)
                {
                    trianglesList.Add(vertexCount + dummyData[i].bottom.triangles[j]);
                }
            }
            if(dummyData[i].walls.Count > 0)
            {
                for (int j = 0; j < dummyData[i].walls.Count; j++)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        trianglesList.Add(vertexCount + dummyData[i].walls[j].triangles[k]);
                    } 
                }
            }



            //if has bottom, add bottom triangles
            //if has walls, add wall triangles

            //find neighbor tiles by pos; will be a list
            vertexCount+= dummyData[i].vertices.Length;
            triangleCount+= dummyData[i].triangles.Length;
        }
        vertices = verticesList.ToArray();
        triangles = trianglesList.ToArray();
    }
    void GenerateMesh(){
        
        GenerateDummyData();
        int triangleCount = 0;
        int vertexCount = 0;
        List<Vector3> verticesList = new List<Vector3>();
        List<int> trianglesList = new List<int>();
        for (int i = 0; i < dummyData.Count; i++)
        {
            for (int j = 0; j < dummyData[i].vertices.Length; j++)
            {
                verticesList.Add(dummyData[i].vertices[j]);
            }
            for (int j = 0; j < dummyData[i].triangles.Length; j++)
            {
                trianglesList.Add(vertexCount + dummyData[i].triangles[j]);
            }
            vertexCount+= dummyData[i].vertices.Length;
            triangleCount+= dummyData[i].triangles.Length;
        }
        vertices = verticesList.ToArray();
        triangles = trianglesList.ToArray();
    }
    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
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