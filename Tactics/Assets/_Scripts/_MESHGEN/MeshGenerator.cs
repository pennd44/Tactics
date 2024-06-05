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