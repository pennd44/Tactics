using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;

public class MeshController : MonoBehaviour
{
    [Range(1.5f, 5)]
    public float radius = 2f;
    [Range(1.5f, 5)]
    public float deformationStrength = 2f;
    private Mesh mesh;
    private Vector3[] vertices, modifiedVerts;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        modifiedVerts = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            modifiedVerts[i] = vertices[i];
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
        Debug.Log(ray);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
            for (int v = 0; v < modifiedVerts.Length; v++)
            {
            Debug.Log(hit.point);
                Vector3 distance = modifiedVerts[v] - hit.point;

                float smoothingFactor = 2f;
                float force = deformationStrength / (1f + hit.point.sqrMagnitude);

                if(distance.sqrMagnitude < radius)
                {
                    if (Input.GetMouseButtonDown(0)){
                        Debug.Log("mouse down");
                        Debug.Log(v);
                        modifiedVerts[v] = modifiedVerts[v] + Vector3.up * force / smoothingFactor;
                    } else if (Input.GetMouseButtonDown(1)){
                        modifiedVerts[v] = modifiedVerts[v] + Vector3.down * force / smoothingFactor;
                    }
                }
            }
        }
            RecalculateMesh();
        
    }
}
