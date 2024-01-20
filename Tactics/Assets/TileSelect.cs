using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : MonoBehaviour
{
    [SerializeField] LevelData levelData;
    Mesh mesh;
    private Vector3 [] verts = new Vector3[4]{
        new Vector3(0,0,0),
        new Vector3(1,0,0),
        new Vector3(0,0,1),
        new Vector3(1,0,1),
    };
    private Vector3 [] modifiedVerts;
    int [] tris = new int[6]{2, 1, 0, 3, 1, 2};
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        modifiedVerts = new Vector3[4];
        for (int i = 0; i < verts.Length; i++)
        {
            modifiedVerts[i] = verts[i];
        }
        Debug.Log(levelData.TrianglesDict);
        // Debug.Log(levelData.gridObjects2);
        // modifiedVerts[0] = levelData.gridObjects2[0]._quad.First;
        // modifiedVerts[1] = levelData.gridObjects2[0]._quad.Second;
        // modifiedVerts[2] = levelData.gridObjects2[0]._quad.Third;
        // modifiedVerts[3] = levelData.gridObjects2[0]._quad.Fourth;
        RecalculateMesh();
    }
    //Get quad that has hit triangle index in leveldata.gridObjects2
    void RecalculateMesh(){
        Debug.Log("Recalculating Mesh");
        mesh.vertices = modifiedVerts;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    // void IncrementQuad(int i)
    // {
    //     modifiedVerts[0] = levelData.gridObjects2[i]._quad.First;
    //     modifiedVerts[1] = levelData.gridObjects2[i]._quad.Second;
    //     modifiedVerts[2] = levelData.gridObjects2[i]._quad.Third;
    //     modifiedVerts[3] = levelData.gridObjects2[i]._quad.Fourth;
    //     RecalculateMesh();

    // }
    int quadIndex = 0;
    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0)){
        //     quadIndex++;
        //     Debug.Log(quadIndex);
        //     IncrementQuad(quadIndex);
        // }
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)){

            Vector3 p0 = levelData.AllTris[hit.triangleIndex].cornerPoint.pos;
            Vector3 p1 = levelData.AllTris[hit.triangleIndex].longestEdgePoints[0].pos;
            Vector3 p2 = levelData.AllTris[hit.triangleIndex].longestEdgePoints[1].pos;
            Vector3 p3 = levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].cornerPoint.pos;

            // Quad hitQuad = levelData.FindQuadFromTriangle(hit.triangleIndex);
            // Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]] + Vector3.up * 0.1f;
            // Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]] + Vector3.up * 0.1f;
            // Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]] + Vector3.up * 0.1f;
            // Vector3 p3 = vertices[triangles[levelData.TrianglesDict[hit.triangleIndex * 3 + 1]]] + Vector3.up * 0.1f;
            // Transform hitTransform = hit.collider.transform;
            // p0 = hitTransform.TransformPoint(p0);
            // p1 = hitTransform.TransformPoint(p1);
            // p2 = hitTransform.TransformPoint(p2);
            modifiedVerts[0] = p0;
            modifiedVerts[2] = p1;
            modifiedVerts[1] = p2;
            modifiedVerts[3] = p3;
            mesh.vertices = modifiedVerts;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            
            MeshCollider meshCollider = hit.collider as MeshCollider;
            Mesh meshh = meshCollider.sharedMesh;
            Vector3[] vertices = meshh.vertices;
            int[] triangles= meshh.triangles;
            if (Input.GetMouseButton(0))
            {
                vertices[triangles[levelData.AllTris[hit.triangleIndex].cornerPoint.id]] -= Vector3.up * 0.2f;
                vertices[triangles[levelData.AllTris[hit.triangleIndex].longestEdgePoints[0].id]] -= Vector3.up * 0.2f;
                vertices[triangles[levelData.AllTris[hit.triangleIndex].longestEdgePoints[1].id]] -= Vector3.up * 0.2f;
                vertices[triangles[levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].cornerPoint.id]] -= Vector3.up * 0.2f;

                meshh.vertices = vertices;
                meshh.RecalculateNormals();
                meshh.RecalculateBounds();

                //update leveldata
                levelData.AllTris[hit.triangleIndex].cornerPoint.pos  = vertices[triangles[levelData.AllTris[hit.triangleIndex].cornerPoint.id]];
                levelData.AllTris[hit.triangleIndex].longestEdgePoints[0].pos = vertices[triangles[levelData.AllTris[hit.triangleIndex].longestEdgePoints[0].id]];
                levelData.AllTris[hit.triangleIndex].longestEdgePoints[1].pos = vertices[triangles[levelData.AllTris[hit.triangleIndex].longestEdgePoints[1].id]];
                levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].cornerPoint.pos = vertices[triangles[levelData.AllTris[levelData.TrianglesDict[hit.triangleIndex]].cornerPoint.id]];
            }
        //     // Debug.DrawLine(p0, p1);
        //     // Debug.DrawLine(p1, p2);
        //     // Debug.DrawLine(p2, p0);
        //     // for (int v = 0; v < modifiedVerts.Length; v++)
        //     // {
        //     // Debug.Log(hit.point);
        //     //     Vector3 distance = modifiedVerts[v] - hit.point;

        //     //     float smoothingFactor = 2f;
        //     //     float force = deformationStrength / (1f + hit.point.sqrMagnitude);

        //     //     if(distance.sqrMagnitude < radius)
        //     //     {
        //     //         if (Input.GetMouseButtonDown(0)){
        //     //             Debug.Log("mouse down");
        //     //             Debug.Log(v);
        //     //             modifiedVerts[v] = modifiedVerts[v] + Vector3.up * force / smoothingFactor;
        //     //         } else if (Input.GetMouseButtonDown(1)){
        //     //             modifiedVerts[v] = modifiedVerts[v] + Vector3.down * force / smoothingFactor;
        //     //         }
        //     //     }
            }
        //     // transform.position = hit.point;
        // }
            // RecalculateMesh();
        
    }
}
