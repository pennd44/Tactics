using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class TileSelect2 : MonoBehaviour
{
    Mesh mesh;
    private Vector3[] verts = new Vector3[4]{
        new Vector3(0,0,0),
        new Vector3(1,0,0),
        new Vector3(0,0,1),
        new Vector3(1,0,1)
    };
    private Vector3[] modifiedVerts;
    int[] tris = new int[6] { 2, 1, 0, 3, 1, 2 };
    // private int[] tris = new int[6] { 0, 1, 2, 2, 1, 3 };
    // int[] tris = new int[3] { 2, 1, 0 };

    [SerializeField] Mesh generatedMesh;
    [SerializeField] MeshGenerator meshGenerator;
    void Start()
    {
        // allVerts = levelData.AllVerts;
        // allTris = levelData.AllTris;
        // trianglesDict = levelData.TrianglesDict;
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        modifiedVerts = new Vector3[4];
        for (int i = 0; i < verts.Length; i++)
        {
            modifiedVerts[i] = verts[i];
        }

        RecalculateMesh();
    }
    //Get quad that has hit triangle index in leveldata.gridObjects2
    void RecalculateMesh()
    {
        mesh.vertices = modifiedVerts;
        // GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    int timesCalled = 0;
    // int GetVertexIndex(Vector3 position, Vector3[] vertices)
    // {
    //     Debug.Log("times called: " + timesCalled++);
    //     Debug.Log("position : " + position);
    //     Debug.Log("in mesh.vertices: " + Array.IndexOf(vertices, position) + "and in leveldata.AllVerts: " + levelData.AllVerts.IndexOf( levelData.GetVertexFromVector3(position) ) + " Equal to eachother? : " + (Array.IndexOf(vertices, position) == levelData.AllVerts.IndexOf( levelData.GetVertexFromVector3(position) )));
    //     int vertexId = Array.IndexOf(vertices, position);
    //     return vertexId;
    // }


    // int quadIndex = 0;





    // Update is called once per frame
    Tri tri;
    void Update()
    {
        generatedMesh = meshGenerator.mesh;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GridObject hoveredGridObject = null;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Debug.Log("hit.triangleIndex: " + hit.triangleIndex);
            // foreach (var gridObject in meshGenerator.gridObjects)
            // {
            meshGenerator.triangleDictionary.TryGetValue(hit.triangleIndex * 3, out Tri tri);
            if (tri != null)
            {
                Vector3 p0 = tri.cornerPoint.pos;
                Vector3 p1 = tri.longestEdgePoints[0].pos;
                Vector3 p2 = tri.longestEdgePoints[1].pos;
                Vector3 p3 = tri.compliment.cornerPoint.pos;
                modifiedVerts[0] = p0;
                modifiedVerts[2] = p1;
                modifiedVerts[1] = p2;
                modifiedVerts[3] = p3;
                mesh.vertices = modifiedVerts;
                mesh.RecalculateNormals();
                mesh.RecalculateBounds();
            }
            // Vector3 p0 = generatedMesh.vertices[generatedMesh.triangles[hit.triangleIndex * 3]];
            // Vector3 p1 = generatedMesh.vertices[generatedMesh.triangles[hit.triangleIndex * 3 + 1]];
            // Vector3 p2 = generatedMesh.vertices[generatedMesh.triangles[hit.triangleIndex * 3 + 2]];
            // Vector3 p3 = generatedMesh.vertices[generatedMesh.triangles[meshGenerator.triangleDictionary[hit.triangleIndex] * 3]];
            //     if (gridObject.tris.Any(tri => tri.id == hit.triangleIndex))
            //     //problem is that the hit.triangleIndex is not the same as the id of the tri in the gridObject

            //     {
            //         hoveredGridObject = gridObject;
            //         // Found the gridObject containing the Tri with the specified id
            //         // Do something with the gridObject

            //         break;
            //     }
            // }

            // if (hoveredGridObject == null)
            // {
            //     return;
            // }
            // Vector3 p0 = hoveredGridObject.tris[0].cornerPoint.pos;
            // Vector3 p1 = hoveredGridObject.tris[0].longestEdgePoints[0].pos;
            // Vector3 p2 = hoveredGridObject.tris[0].longestEdgePoints[1].pos;
            // Vector3 p3 = hoveredGridObject.tris[1].cornerPoint.pos;


            // MeshCollider meshCollider = hit.collider as MeshCollider;
            // Mesh hitMesh = meshCollider.sharedMesh;
            // Vector3[] vertices = hitMesh.vertices;
            // List<Vector3> verticesList = hitMesh.vertices.ToList();
            // List<int> triangles = hitMesh.triangles.ToList();
            if (Input.GetMouseButtonDown(0))
            {
                // List<Tri> wallTris;
                // wallTris = meshGenerator.tris.Where(tri => tri.cornerPoint.pos.x == tri.longestEdgePoints[0].pos.x && tri.cornerPoint.pos.z == tri.longestEdgePoints[0].pos.z).ToList();
                // //Debug
                // Debug.Log("wallTris.Count: " + wallTris.Count);
                // Move the hit tile down by 1 unit
                Vector3[] vertices = generatedMesh.vertices;
                int[] triangles = generatedMesh.triangles;

                // Get the indices of the vertices for the hit triangle
                int vertexIndex1 = triangles[hit.triangleIndex * 3];
                int vertexIndex2 = triangles[hit.triangleIndex * 3 + 1];
                int vertexIndex3 = triangles[hit.triangleIndex * 3 + 2];
                int vertexIndex4 = triangles[meshGenerator.tris[hit.triangleIndex].compliment.index * 3];

                // Move the vertices down by 1 unit
                vertices[vertexIndex1] -= Vector3.up;
                vertices[vertexIndex2] -= Vector3.up;
                vertices[vertexIndex3] -= Vector3.up;
                vertices[vertexIndex4] -= Vector3.up;
                

                // Update the mesh vertices and recalculate normals and bounds
                generatedMesh.vertices = vertices;
                generatedMesh.RecalculateNormals();
                generatedMesh.RecalculateBounds();
            }
            if(Input.GetMouseButtonDown(1))
            {
                Debug.Log("Mesh.Triangles: " + generatedMesh.triangles.Length);
                Debug.Log("Tri Index Length" + meshGenerator.tris.Count * 3);
                Debug.Log("Tri Index Length" + meshGenerator.tris[generatedMesh.triangles.Length / 3].index * 3);
                // Draw a gizmo on vertex 4
                Vector3 vertex4 = generatedMesh.vertices[generatedMesh.triangles[meshGenerator.tris[hit.triangleIndex].compliment.index * 3]];
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(vertex4, 0.1f);
            }
            // {
            //     Tri tri1 = levelData.AllTris[hit.triangleIndex];
            //     Tri tri2 = levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]];

            //     //get the mesh.vertices index of the corner point of the triangle
            //     int cornerPointIndex = tri1.cornerPointIndex;
            //     int cornerPointIndex2 = tri2.cornerPointIndex;
            //     //get the mesh.vertices index of the longest edge point of the triangle
            //     int longestEdgePointIndex = tri1.longestEdgePoints1Index;
            //     int longestEdgePointIndex2 = tri1.longestEdgePoints2Index;


            //     vertices[cornerPointIndex] -= Vector3.up;
            //     vertices[longestEdgePointIndex] -= Vector3.up;
            //     vertices[longestEdgePointIndex2] -= Vector3.up;
            //     vertices[cornerPointIndex2] -= Vector3.up;

            //     // vertices[triangles[levelData.AllTris[hit.triangleIndex].cornerPoint.id]] -= Vector3.up * 0.2f;
            //     // vertices[triangles[levelData.AllTris[hit.triangleIndex].longestEdgePoints[0].id]] -= Vector3.up * 0.2f;
            //     // vertices[triangles[levelData.AllTris[hit.triangleIndex].longestEdgePoints[1].id]] -= Vector3.up * 0.2f;
            //     // vertices[triangles[levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].cornerPoint.id]] -= Vector3.up * 0.2f;

            //     hitMesh.vertices = vertices;
            //     hitMesh.RecalculateNormals();
            //     hitMesh.RecalculateBounds();

            //     //update leveldata
            //     // Debug.Log(levelData.AllVerts[cornerPointIndex] == p0);
            //     // Debug.Log(levelData.AllVerts[longestEdgePointIndex] == p1);
            //     // Debug.Log(levelData.AllVerts[longestEdgePointIndex2] == p2);
            //     // Debug.Log(levelData.AllVerts[cornerPointIndex2] == p3);

            //     Debug.Log("p0 before " + levelData.AllTris[hit.triangleIndex].cornerPoint.pos);
            //     Debug.Log("p1 before " + levelData.AllTris[hit.triangleIndex].longestEdgePoints[0].pos);
            //     Debug.Log("p2 before " + levelData.AllTris[hit.triangleIndex].longestEdgePoints[1].pos);
            //     Debug.Log("p3 before " + levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].cornerPoint.pos);


            //     Debug.Log("v0 before " + levelData.AllVerts[cornerPointIndex].pos);
            //     Debug.Log("v1 before " + levelData.AllVerts[longestEdgePointIndex].pos);
            //     Debug.Log("v2 before " + levelData.AllVerts[longestEdgePointIndex2].pos);
            //     Debug.Log("v3 before " + levelData.AllVerts[cornerPointIndex2].pos);

            //     // levelData.AllVerts[cornerPointIndex].pos -= Vector3.up;

            //     // levelData.AllVerts[longestEdgePointIndex].pos -= Vector3.up;

            //     // levelData.AllVerts[longestEdgePointIndex2].pos -= Vector3.up;
            //     // levelData.AllVerts[cornerPointIndex2].pos -= Vector3.up;

            //     //problem is leveldata.AllVerts[cornerPointIndex] is not the same as p0
            //     levelData.AllTris[hit.triangleIndex].cornerPoint.pos -= Vector3.up;
            //     levelData.AllTris[hit.triangleIndex].longestEdgePoints[0].pos -= Vector3.up;
            //     levelData.AllTris[hit.triangleIndex].longestEdgePoints[1].pos -= Vector3.up;
            //     levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].cornerPoint.pos -= Vector3.up;

            //     Debug.Log("p0 after " + levelData.AllTris[hit.triangleIndex].cornerPoint.pos);
            //     Debug.Log("p1 after " + levelData.AllTris[hit.triangleIndex].longestEdgePoints[0].pos);
            //     Debug.Log("p2 after " + levelData.AllTris[hit.triangleIndex].longestEdgePoints[1].pos);
            //     Debug.Log("p3 after " + levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].cornerPoint.pos);
            //     // mesh.vertices =  new Vector3 [4]{p0, p1, p2, p3};
            //     Debug.Log("v0 after " + levelData.AllVerts[cornerPointIndex].pos);
            //     Debug.Log("v1 after " + levelData.AllVerts[longestEdgePointIndex].pos);
            //     Debug.Log("v2 after " + levelData.AllVerts[longestEdgePointIndex2].pos);
            //     Debug.Log("v3 after " + levelData.AllVerts[cornerPointIndex2].pos);
            //     // mesh.RecalculateNormals();
            //     // mesh.RecalculateBounds();



            //     // levelData.AllTris[hit.triangleIndex].cornerPoint.pos  = vertices[triangles[levelData.AllTris[hit.triangleIndex].cornerPoint.id]];
            //     // levelData.AllTris[hit.triangleIndex].longestEdgePoints[0].pos = vertices[triangles[levelData.AllTris[hit.triangleIndex].longestEdgePoints[0].id]];
            //     // levelData.AllTris[hit.triangleIndex].longestEdgePoints[1].pos = vertices[triangles[levelData.AllTris[hit.triangleIndex].longestEdgePoints[1].id]];
            //     // levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].cornerPoint.pos = vertices[triangles[levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].cornerPoint.id]];
            // }
            // //On right click, create a face 1 unit above the hit face



            // if(Input.GetMouseButtonDown(1))
            // {
            //     // if (hit.triangleIndex > levelData.TrianglesDict[hit.triangleIndex])
            //     // {
            //     //     triangles.RemoveAt((hit.triangleIndex + 1)*3 - 1);
            //     //     triangles.RemoveAt((hit.triangleIndex + 1)*3 - 2);
            //     //     triangles.RemoveAt((hit.triangleIndex + 1)*3 - 3);
            //     //     triangles.RemoveAt((levelData.TrianglesDict[hit.triangleIndex] + 1)*3 - 1);
            //     //     triangles.RemoveAt((levelData.TrianglesDict[hit.triangleIndex] + 1)*3 - 2);
            //     //     triangles.RemoveAt((levelData.TrianglesDict[hit.triangleIndex] + 1)*3 - 3);
            //     // }
            //     // else
            //     // {
            //     //     triangles.RemoveAt((levelData.TrianglesDict[hit.triangleIndex] + 1)*3 - 1);
            //     //     triangles.RemoveAt((levelData.TrianglesDict[hit.triangleIndex] + 1)*3 - 2);
            //     //     triangles.RemoveAt((levelData.TrianglesDict[hit.triangleIndex] + 1)*3 - 3); 
            //     //     triangles.RemoveAt((hit.triangleIndex + 1)*3 - 1);
            //     //     triangles.RemoveAt((hit.triangleIndex + 1)*3 - 2);
            //     //     triangles.RemoveAt((hit.triangleIndex + 1)*3 - 3);
            //     // }
            //     Vertex v0 = new Vertex(p0.pos + Vector3.up);
            //     Vertex v1 = new Vertex(p1.pos + Vector3.up);
            //     Vertex v2 = new Vertex(p2.pos + Vector3.up);
            //     Vertex v3 = new Vertex(p3.pos + Vector3.up);
            //     verticesList.Add(v0.pos);
            //     verticesList.Add(v1.pos);
            //     verticesList.Add(v2.pos);
            //     verticesList.Add(v3.pos);
            //     triangles[(hit.triangleIndex + 1)*3 - 1] = verticesList.Count - 3;
            //     triangles[(hit.triangleIndex + 1)*3 - 2] = verticesList.Count - 2;
            //     triangles[(hit.triangleIndex + 1)*3 - 3] = verticesList.Count - 1;
            //     triangles[(levelData.TrianglesDict[hit.triangleIndex] + 1)*3 - 1] = verticesList.Count - 4;
            //     triangles[(levelData.TrianglesDict[hit.triangleIndex] + 1)*3 - 2] = verticesList.Count - 2;
            //     triangles[(levelData.TrianglesDict[hit.triangleIndex] + 1)*3 - 3] = verticesList.Count - 3;
            //     hitMesh.vertices = verticesList.ToArray();
            //     hitMesh.triangles = triangles.ToArray();
            //     hitMesh.RecalculateNormals();
            //     hitMesh.RecalculateBounds();
            //     meshCollider.sharedMesh = hitMesh;
            //     //nicccce next is making the tile select mesh update to match the hitmesh
            //     levelData.AllVerts.Add(v0);
            //     levelData.AllVerts.Add(v1);
            //     levelData.AllVerts.Add(v2);
            //     levelData.AllVerts.Add(v3);
            //     levelData.AllTris[hit.triangleIndex].cornerPoint = v0;
            //     levelData.AllTris[hit.triangleIndex].longestEdgePoints[0] = v1;
            //     levelData.AllTris[hit.triangleIndex].longestEdgePoints[1] = v2;
            //     levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].cornerPoint = v3;
            //     levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].longestEdgePoints[0] = v1;
            //     levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].longestEdgePoints[1] = v2;

            // levelData.AllTris.RemoveAt(hit.triangleIndex);
            // levelData.AllTris.RemoveAt(levelData.TrianglesDict[hit.triangleIndex]);
            // (t+3)/3 - 1 to t
            // levelData.AllTris[hit.triangleIndex].Id
            //create 4 verts
            // levelData.AllVerts.Add(v0);
            // levelData.AllVerts.Add(v1);
            // levelData.AllVerts.Add(v2);
            // levelData.AllVerts.Add(v3);
            //create 2 tris for top side
            // levelData.AllTris.Add(new Tri(levelData.AllTris.Count, v0, v2, v1));
            // levelData.AllTris.Add(new Tri(levelData.AllTris.Count, v1, v2, v3));
            //create 2 tris for left side
            // levelData.AllTris.Add(new Tri(levelData.AllTris.Count, p0, v0, p1));
            // levelData.AllTris.Add(new Tri(levelData.AllTris.Count, p1, v0, v1));
            //create 2 tris for right side
            // levelData.AllTris.Add(new Tri(levelData.AllTris.Count, p2, v2, p3));
            // levelData.AllTris.Add(new Tri(levelData.AllTris.Count, p3, v2, v3));
            //create 2 tris for front side
            // levelData.AllTris.Add(new Tri(levelData.AllTris.Count, p0, p2, v0));
            // levelData.AllTris.Add(new Tri(levelData.AllTris.Count, p2, v2, v0));
            //create 2 tris for back side
            // levelData.AllTris.Add(new Tri(levelData.AllTris.Count, p1, v1, p3));
            // levelData.AllTris.Add(new Tri(levelData.AllTris.Count, p3, v1, v3));

            //How do I update hitmesh.vertices to include the new vertices?
            //How do I update hitmesh.triangles to include the new triangles?
            // Vector3[] newVertices = new Vector3[levelData.AllVerts.Count];
            // for (int i = 0; i < levelData.AllVerts.Count; i++)
            // {
            //     newVertices[i] = levelData.AllVerts[i].pos;
            // }
            // hitMesh.vertices = newVertices;

            // Update hitmesh.triangles to match levelData.AllTris
            // int[] newTriangles = new int[levelData.AllTris.Count * 3];
            // for (int i = 0; i < levelData.AllTris.Count; i++)
            // {
            //     newTriangles[i * 3] = levelData.AllTris[i].First.id;
            //     newTriangles[i * 3 + 1] = levelData.AllTris[i].Second.id;
            //     newTriangles[i * 3 + 2] = levelData.AllTris[i].Third.id;
            // }
            // hitMesh.triangles = newTriangles;
            // hitMesh.RecalculateNormals();
            // hitMesh.RecalculateBounds();
            // }
        }
    }
}
