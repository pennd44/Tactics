using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

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
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        modifiedVerts = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            modifiedVerts[i] = vertices[i];
        }
        MoveQuads();
    }
    //initialize gridObjects list at every x z point on map
    //go through triangles and add each quad to list[x,z]
    [SerializeField] Transform debugPrefab;
    public GridObject test;
    [ContextMenu("load grid objects")]
    void LoadGridObjects(){
        mesh = GetComponent<MeshFilter>().sharedMesh;
        levelData._gridObjects = new List<GridObject>[55, 39];
        for (int t = 0; t < mesh.triangles.Length; t+=6)
        {

            Debug.Log(mesh.vertices[mesh.triangles[t+0]]);
            int x = (int)(( mesh.vertices[mesh.triangles[t+0]].x + 
                        mesh.vertices[mesh.triangles[t+1]].x +
                        mesh.vertices[mesh.triangles[t+2]].x +
                        mesh.vertices[mesh.triangles[t+5]].x) / 4);
            Debug.Log(x);
            int z = (int)(( mesh.vertices[mesh.triangles[t+0]].z + 
                        mesh.vertices[mesh.triangles[t+1]].z +
                        mesh.vertices[mesh.triangles[t+2]].z +
                        mesh.vertices[mesh.triangles[t+5]].z) / 4);
            Debug.Log(z);
            if(levelData._gridObjects[x + 12, z + 28] == null)
            {
                Debug.Log("creating new");
                levelData._gridObjects[x + 12,z + 28] = new List<GridObject>();
            }
            
            // GridObject go = new GridObject(new Point(x,z), new Quad(
            //     mesh.vertices[mesh.triangles[t+0]],
            //     mesh.vertices[mesh.triangles[t+1]],
            //     mesh.vertices[mesh.triangles[t+2]],
            //     mesh.vertices[mesh.triangles[t+5]]
            // ));
            // GameObject.Instantiate(debugPrefab, go.Center(), Quaternion.identity);
            // levelData._gridObjects[x + 12, z + 28].Add(go);
        }
    }


    [ContextMenu("load grid objects 2")]
    void LoadGridObjects2(){
        mesh = GetComponent<MeshFilter>().sharedMesh;
        // levelData.gridObjects2 = new List<GridObject>();
        for (int t = 0; t < mesh.triangles.Length; t+=3)
        {
            Vector3 p0 = mesh.vertices[mesh.triangles[t+0]];
            Vector3 p1 = mesh.vertices[mesh.triangles[t+1]];
            Vector3 p2 = mesh.vertices[mesh.triangles[t+2]];


            float distance0 = Vector3.Distance(p0, p1);
            float distance1 = Vector3.Distance(p0, p2);
            float distance2 = Vector3.Distance(p1, p2);
            List<Vector3> edgePoints = new List<Vector3>();
            Vector3 cornerPoint;
            if (distance0 > distance1 && distance0 > distance2)
            {
                edgePoints.Add(p0);
                edgePoints.Add(p1);
                cornerPoint = p2;
            }
            else if (distance1 > distance2){
                edgePoints.Add(p0);
                edgePoints.Add(p2);
                cornerPoint = p1;
            }
            else{
                edgePoints.Add(p1);
                edgePoints.Add(p2);
                cornerPoint = p0;
            }

            Tri neighborTri;
            for (int j = 0; j < mesh.triangles.Length; j+=3)
            { 
                List<Vector3> neighborEdgePoints = new List<Vector3>();
                Vector3 cornerPointNeighbor = Vector3.up;
                Vector3 v0 = mesh.vertices[mesh.triangles[j+0]];
                Vector3 v1 = mesh.vertices[mesh.triangles[j+1]];
                Vector3 v2 = mesh.vertices[mesh.triangles[j+2]];

                if (edgePoints.Contains(v0)) 
                {
                    neighborEdgePoints.Add(v0);
                }
                else{
                    cornerPointNeighbor = v0;
                }
                if (edgePoints.Contains(v1)) 
                {
                    neighborEdgePoints.Add(v1);
                }
                else{
                    cornerPointNeighbor = v1;
                }
                if (edgePoints.Contains(v2)) 
                {
                    neighborEdgePoints.Add(v2);
                }  else{
                    cornerPointNeighbor = v2;
                }
                
                if (neighborEdgePoints.Count== 2)
                {
                    if (cornerPointNeighbor == cornerPoint) continue;

                    //triangle j is a neighbor
                    neighborTri = (new Tri(v0, v1, v2));
                    levelData.TrianglesDict.Add( (t+3)/3, (j+3)/3);
                    break;
                }

                // if (v == v1 || v == v2 || v == v3){ // if any common vertex found…
//     var nTri: int = i / 3; // find the triangle number…
//     if (nTri != myTriangle){ // and if it’s diff from #myTriangle…
//     // triangle #nTri is neighbour of triangle #myTriangle
//     }
            }






            // int x = (int)(( p0.x + p1.x + p2.x ) / 3);
            // int z = (int)(( p0.z + p1.z + p2.z) / 3);
            
             
            
            
            
            // GridObject go = new GridObject(new Point(x,z), new Quad(
            //     mesh.vertices[mesh.triangles[t+0]],
            //     mesh.vertices[mesh.triangles[t+1]],
            //     mesh.vertices[mesh.triangles[t+2]],
            //     mesh.vertices[mesh.triangles[t+5]]
            // ));
            // levelData.gridObjects2.Add(go);
        }




               
//     int i = myTriangle * 3; // each triangle occupies 3 entries in the triangles array
//     int v1= triangles[i++]; // get v1, v2 and v3,
//     int v2 = triangles[i++]; // the 3 vertex indexes of
//     int v3 = triangles*; // triangle #myTriangle*
// for (i = 0; i < triangles.length; i++){
// // compare each vertex index to v1, v2 and v3:
//     var v: int = triangles*;*
//     if (v == v1 || v == v2 || v == v3){ // if any common vertex found…
//     var nTri: int = i / 3; // find the triangle number…
//     if (nTri != myTriangle){ // and if it’s diff from #myTriangle…
//     // triangle #nTri is neighbour of triangle #myTriangle
//     }
//     }
// }




    }
    
    private Vector3[] modifiedVerts;
    void MoveQuads(){
        for (int t = 0; t < 7; t+=6)
        {
                modifiedVerts[mesh.triangles[t+0]] += Vector3.up;
                modifiedVerts[mesh.triangles[t+1]] += Vector3.up;
                modifiedVerts[mesh.triangles[t+2]] += Vector3.up;
                modifiedVerts[mesh.triangles[t+5]] += Vector3.up;
        }
            // modifiedVerts[mesh.triangles[0]] += Vector3.up;
            // modifiedVerts[mesh.triangles[1]] += Vector3.up;
            // modifiedVerts[mesh.triangles[2]] += Vector3.up;
            // modifiedVerts[mesh.triangles[5]] += Vector3.up;
        mesh.vertices = modifiedVerts;
        GetComponent<MeshCollider>().sharedMesh = mesh;
         mesh.RecalculateNormals();
    }



    void OnDrawGizmosSelected()
    {
        mesh = GetComponent<MeshFilter>().sharedMesh;
        Gizmos.color = Color.yellow;
        for (int t = 0; t < 100; t+=6)
        {
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[t]], 0.1f);
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[t + 1]], 0.1f);
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[t + 2]], 0.1f);
            Gizmos.DrawSphere(mesh.vertices[mesh.triangles[t + 5]], 0.1f);
    //             foreach (GridObject go in levelData._gridObjects[x, 1])
    //             {
    //             Debug.Log(go.Center());
    //             Gizmos.DrawSphere(go.Center(), 1);
    //             }
        }
        // for (int x = 0; x < 55; x++)
        // {
        //     for (int z = 0; z < 39; z++)
        //     { 
                // Debug.Log(levelData._gridObjects[x, z]);
                //  foreach (GridObject go in levelData._gridObjects[x, z])
                //  {
                //     Debug.Log(go.Center());
                //     // Gizmos.DrawSphere(go.Center(), 0.2f);
                //  }              
        //     }
        // }
        // foreach (GridObject go in levelData._gridObjects)
        // {
        //     Gizmos.DrawSphere(point, 0.1f);
        // mesh.triangles[i] gives the index of the vertex
            // Gizmos.DrawSphere(mesh.vertices[mesh.triangles[0]], 0.1f);

        //-12, -28  to 43, 11
    }

    // public  void Weld(Mesh mesh, float threshold, float bucketStep)
    //         {
    //             Vector3[] oldVertices = mesh.vertices;
    //             Vector3[] newVertices = new Vector3[oldVertices.Length];
    //             int[] old2new = new int[oldVertices.Length];
    //             int newSize = 0;
    
    //             // Find AABB
    //             Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
    //             Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
    //             for (int i = 0; i < oldVertices.Length; i++)
    //             {
    //                 if (oldVertices[i].x < min.x) min.x = oldVertices[i].x;
    //                 if (oldVertices[i].y < min.y) min.y = oldVertices[i].y;
    //                 if (oldVertices[i].z < min.z) min.z = oldVertices[i].z;
    //                 if (oldVertices[i].x > max.x) max.x = oldVertices[i].x;
    //                 if (oldVertices[i].y > max.y) max.y = oldVertices[i].y;
    //                 if (oldVertices[i].z > max.z) max.z = oldVertices[i].z;
    //             }
    //             min -= Vector3.one * 0.111111f;
    //             max += Vector3.one * 0.899999f;
    
    //             // Make cubic buckets, each with dimensions "bucketStep"
    //             int bucketSizeX = Mathf.FloorToInt((max.x - min.x) / bucketStep) + 1;
    //             int bucketSizeY = Mathf.FloorToInt((max.y - min.y) / bucketStep) + 1;
    //             int bucketSizeZ = Mathf.FloorToInt((max.z - min.z) / bucketStep) + 1;
    //             List<int>[,,] buckets = new List<int>[bucketSizeX, bucketSizeY, bucketSizeZ];
    
    //             // Make new vertices
    //             for (int i = 0; i < oldVertices.Length; i++)
    //             {
    //                 // Determine which bucket it belongs to
    //                 int x = Mathf.FloorToInt((oldVertices[i].x - min.x) / bucketStep);
    //                 int y = Mathf.FloorToInt((oldVertices[i].y - min.y) / bucketStep);
    //                 int z = Mathf.FloorToInt((oldVertices[i].z - min.z) / bucketStep);
    
    //                 // Check to see if it's already been added
    //                 if (buckets[x, y, z] == null)
    //                     buckets[x, y, z] = new List<int>(); // Make buckets lazily
    
    //                 for (int j = 0; j < buckets[x, y, z].Count; j++)
    //                 {
    //                     Vector3 to = newVertices[buckets[x, y, z][j]] - oldVertices[i];
    //                     if (Vector3.SqrMagnitude(to) < 0.001f)
    //                     {
    //                         old2new[i] = buckets[x, y, z][j];
    //                         goto skip; // Skip to next old vertex if this one is already there
    //                     }
    //                 }
    
    //                 // Add new vertex
    //                 newVertices[newSize] = oldVertices[i];
    //                 buckets[x, y, z].Add(newSize);
    //                 old2new[i] = newSize;
    //                 newSize++;
    
    //                 skip:;
    //             }
    
    //             // Make new triangles
    //             int[] oldTris = mesh.triangles;
    //             int[] newTris = new int[oldTris.Length];
    //             for (int i = 0; i < oldTris.Length; i++)
    //             {
    //                 newTris[i] = old2new[oldTris[i]];
    //             }
    
    //             Vector3[] finalVertices = new Vector3[newSize];
    //             for (int i = 0; i < newSize; i++)
    //                 finalVertices[i] = newVertices[i];
    
    //             mesh.Clear();
    //             mesh.vertices = finalVertices;
    //             mesh.triangles = newTris;
    // }



    
}
