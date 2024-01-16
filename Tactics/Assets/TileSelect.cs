using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelect : MonoBehaviour
{
    Mesh mesh;
    private Vector3 [] verts = new Vector3[3]{
        new Vector3(0,0,0),
        new Vector3(1,0,0),
        new Vector3(0,0,1),
        // new Vector3(1,0,1),
    };
    private Vector3 [] modifiedVerts;
    int [] tris = new int[3]{0, 2, 1}; //123
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        modifiedVerts = new Vector3[3];
        for (int i = 0; i < verts.Length; i++)
        {
            modifiedVerts[i] = verts[i];
        }
    }
    void RecalculateMesh(){
        Debug.Log("Recalculating Mesh");
        mesh.vertices = modifiedVerts;
        GetComponent<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
            MeshCollider meshCollider = hit.collider as MeshCollider;

            Mesh meshh = meshCollider.sharedMesh;
            Vector3[] vertices = meshh.vertices;
            int[] triangles= meshh.triangles;
            Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]] + Vector3.up * 0.1f;
            Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]] + Vector3.up * 0.1f;
            Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]] + Vector3.up * 0.1f;
            // Transform hitTransform = hit.collider.transform;
            // p0 = hitTransform.TransformPoint(p0);
            // p1 = hitTransform.TransformPoint(p1);
            // p2 = hitTransform.TransformPoint(p2);
            modifiedVerts[0] = p0;
            modifiedVerts[2] = p1;
            modifiedVerts[1] = p2;
            mesh.vertices = modifiedVerts;
            mesh.RecalculateNormals();
            // Debug.DrawLine(p0, p1);
            // Debug.DrawLine(p1, p2);
            // Debug.DrawLine(p2, p0);
            // for (int v = 0; v < modifiedVerts.Length; v++)
            // {
            // Debug.Log(hit.point);
            //     Vector3 distance = modifiedVerts[v] - hit.point;

            //     float smoothingFactor = 2f;
            //     float force = deformationStrength / (1f + hit.point.sqrMagnitude);

            //     if(distance.sqrMagnitude < radius)
            //     {
            //         if (Input.GetMouseButtonDown(0)){
            //             Debug.Log("mouse down");
            //             Debug.Log(v);
            //             modifiedVerts[v] = modifiedVerts[v] + Vector3.up * force / smoothingFactor;
            //         } else if (Input.GetMouseButtonDown(1)){
            //             modifiedVerts[v] = modifiedVerts[v] + Vector3.down * force / smoothingFactor;
            //         }
            //     }
            // }
            // transform.position = hit.point;
        }
            // RecalculateMesh();
        
    }
}
