using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] LevelData levelData;

    void GenerateQuad(Quad quad){
        Mesh mesh = new Mesh();
        // int [] triangles;
        // Vector3 [] vertices;

    }
    void GenerateMap(List<Quad> quads){
        foreach (Quad quad in quads)
        {
            GenerateQuad(quad);
        }
    }
    public int WorldX;
    public int WorldZ;
    private Mesh mesh;
    private int [] triangles;
    private Vector3[] vertices;
    private void Start() {
        Debug.Log("hittin");
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateMesh();
        UpdateMesh();
    }
    void GenerateMesh(){
        triangles = new int[WorldX * WorldZ * 6];
        vertices = new Vector3[(WorldX+1) * (WorldZ + 1)];

        for (int i = 0, z = 0; z <= WorldZ; z++)
        {
            for (int x = 0; x <= WorldX; x++)
            {
                vertices[i] = new Vector3( x, 0, z );
                i++;
            }   
        }

        int tris = 0;
        int verts = 0;


        for (int z = 0; z < WorldZ; z++)
        {
            for (int x = 0; x < WorldX; x++)
            {
                triangles[tris + 0] = verts + 0;
                triangles[tris + 1] = verts + WorldZ + 1;
                triangles[tris + 2] = verts + 1;

                triangles[tris + 3] = verts + 1;
                triangles[tris + 4] = verts + WorldZ + 1;
                triangles[tris + 5] = verts + WorldZ + 2;

                verts++;
                tris += 6;
            }
            verts ++;   
        }
    }
    void UpdateMesh(){
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    // void OnDrawGizmosSelected()
    // {
    //     foreach (Vector3 point in levelData.GetAllPoints())
    //     {
    //         Gizmos.color = Color.yellow;
    //         Gizmos.DrawSphere(point, 0.1f);
    //     }
    //     // Draw a yellow sphere at the transform's position
    // }
}
