using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class GridTest : MonoBehaviour
{
    [SerializeField] public LevelData levelData;
    Mesh mesh;
    public Vector3[] vertices;
    [ContextMenu("Preload")]
    private void Preload() {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
    }

    //get vertexes at x z coord
    public List<Vector3> VerticesAtPoint(Point point)
    {
        Vector3[] verticesAtPoint = Array.FindAll(vertices,  
            vertex => (vertex.x == point.x + .5f || vertex.x == point.x - .5f) && (vertex.z == point.y + .5f || vertex.z == point.y - .5f));
        return verticesAtPoint.ToList();
    }

    // void Test(){
    //     Vector3 target;

    //     List<int> foundTriangles = new List<int>();

    //     for (int t = 0; t < mesh.triangles.Length; t+=3)
    //     {
    //         Vector3[] verts = new Vector3[3] { 
    //             mesh.vertices[mesh.triangles[t]],
    //             mesh.vertices[mesh.triangles[t+1]],
    //             mesh.vertices[mesh.triangles[t+2]],
    //         };
    //         if (verts[0] == target || verts[1] == target || verts[2] == target)
    //             foundTriangles.Add(t);
    //     }
    // }


// If you wanted to try a precomputed method you could always test and store the vertex connections in a table eg.

// int[][] vertexConnections;

// int[] //one array for each vertex

// [] // holding each vertex it shares a triangle with

    List<int> FindTrianglesAtPoint(Point point)
    {
        int[] trianglesAtPoint = Array.FindAll(mesh.triangles,
            triPoint => VerticesAtPoint(point).Contains(mesh.vertices[triPoint]));
        return trianglesAtPoint.ToList();
    }

    void ContinuousTriangle(){

    }

    // We want each tile to have its 4 vetices
    //To do that we need both its triangles
    //To do that we need 2 sets of 3 verts that belong to those triangles
    // go from 8 verts to the 4 we need
    //To do that we need the verts within 0.5 tile lengths of our point
    private void Start() {
        LoadGridObjects();
    }

    [ContextMenu("load grid objects")]
    void LoadGridObjects(){
        for (int t = 0; t < mesh.triangles.Length; t+=6)
        {
            int x = (int)(( mesh.vertices[mesh.triangles[t+0]].x + 
                        mesh.vertices[mesh.triangles[t+1]].x +
                        mesh.vertices[mesh.triangles[t+2]].x +
                        mesh.vertices[mesh.triangles[t+5]].x) / 4);
            int z = (int)(( mesh.vertices[mesh.triangles[t+0]].x + 
                        mesh.vertices[mesh.triangles[t+1]].x +
                        mesh.vertices[mesh.triangles[t+2]].x +
                        mesh.vertices[mesh.triangles[t+5]].x) / 4);
            levelData._gridObjects[x, z].Add(new GridObject(new Point(x,z), new Quad(
                mesh.vertices[mesh.triangles[t+0]],
                mesh.vertices[mesh.triangles[t+1]],
                mesh.vertices[mesh.triangles[t+2]],
                mesh.vertices[mesh.triangles[t+5]]
            )));
        }
    }



    void OnDrawGizmosSelected()
    {
        // foreach (Vector3 point in VerticesAtPoint(new Point (0,0)))
        // {
        //     Gizmos.DrawSphere(point, 0.1f);
        // mesh.triangles[i] gives the index of the vertex
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[0]], 0.1f);
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[1]], 0.1f);
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[2]], 0.1f);
            // Gizmos.DrawSphere(mesh.vertices[mesh.triangles[3]], 0.1f);
            // Gizmos.DrawSphere(mesh.vertices[mesh.triangles[4]], 0.1f);
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[5]], 0.1f);
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[0+6]], 0.1f);
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[1+6]], 0.1f);
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[2+6]], 0.1f);
            
            // Gizmos.DrawSphere(mesh.vertices[mesh.triangles[3+6]], 0.1f);
            // Gizmos.DrawSphere(mesh.vertices[mesh.triangles[4+6]], 0.1f);
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[5+6]], 0.1f);
        // }

        //-12, -28  to 43, 11
        // Draw a yellow sphere at the transform's position
    }
}
