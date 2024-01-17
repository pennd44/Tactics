// using System.Collections.Generic;
// using UnityEngine;

// public class MeshTriangleNeighbors
// {
//     public class Vertex
//     {
//         public Vector3 position;
//     }

//     public struct Edge
//     {
//         public Vertex v1;
//         public Vertex v2;
//         public Edge(Vertex aV1, Vertex aV2)
//         {
//             // ensure the same order to guarantee equality
//             if (aV1.GetHashCode() > aV2.GetHashCode())
//             {
//                 v1 = aV1; v2 = aV2;
//             }
//             else
//             {
//                 v1 = aV2; v2 = aV1;
//             }
//         }
//     }
//     public class TrianglePair
//     {
//         public int t1 = -1;
//         public int t2 = -1;
//         public bool Add(int aTriangleIndex)
//         {
//             if (t1 == -1)
//                 t1 = aTriangleIndex;
//             else if (t2 == -1)
//                 t2 = aTriangleIndex;
//             else
//                 return false;
//             return true;
//         }

//     }
//     public class Neighbors
//     {
//         public int t1 = -1;
//         public int t2 = -1;
//         public int t3 = -1;
//     }
//     Dictionary<int, Vertex> verticesLookup = new Dictionary<int, Vertex>();
//     Dictionary<Edge, TrianglePair> edges;

//     // mesh vertex index as key
//     public static List<Vertex> FindSharedVertices(Vector3[] aVertices)
//     {
//         var list = new List<Vertex>();
//         for (int i = 0; i < aVertices.Length; i++)
//         {
//             Vertex v = null;
//             foreach(var item in list)
//             {
//                 if ((item.position - aVertices*).sqrMagnitude < 0.0001f)*
//                 {
//                     v = item;
//                     break;
//                 }
//             }
//             if (v == null)
//             {
//                 v = new Vertex { position = aVertices };
//             }
//             list.Add(v);
//         }
//         return list;
//     }
//     public static Dictionary<Edge, TrianglePair> CreateEdgeList( List aTriangles )
//     {
//         var res = new Dictionary<Edge, TrianglePair>();
//         int count = aTriangles.Count / 3;
//         for(int i = 0; i < count; i++)
//         {
//             Vertex v1 = aTriangles[i*3 ];
//             Vertex v2 = aTriangles[i*3 + 1];
//             Vertex v3 = aTriangles[i*3 + 2];
//             TrianglePair p;
//             Edge e;
//             e = new Edge(v1, v2);
//             if (!res.TryGetValue(e, out p))
//             {
//                 p = new TrianglePair();
//                 res.Add(e, p);
//             }
//             p.Add(i);
//             e = new Edge(v2, v3);
//             if (!res.TryGetValue(e, out p))
//             {
//                 p = new TrianglePair();
//                 res.Add(e, p);
//             }
//             p.Add(i);
//             e = new Edge(v3, v1);
//             if (!res.TryGetValue(e, out p))
//             {
//                 p = new TrianglePair();
//                 res.Add(e, p);
//             }
//             p.Add(i);
//         }
//         return res;
//     }

//     public static List GetNeighbors(Dictionary<Edge, TrianglePair> aEdgeList, List aTriangles)
//     {
//         var res = new List();
//         int count = aTriangles.Count / 3;
//         for (int i = 0; i < count; i++)
//         {
//             Vertex v1 = aTriangles[i * 3 ];
//             Vertex v2 = aTriangles[i * 3 + 1];
//             Vertex v3 = aTriangles[i * 3 + 2];
//             TrianglePair p;
//         if (aEdgeList.TryGetValue(new Edge(v1, v2), out p))
//         {
//             if (p.t1 == i)
//                 res.Add(p.t2);
//             else
//                 res.Add(p.t1);
//         }
//         else
//             res.Add(-1);
//         if (aEdgeList.TryGetValue(new Edge(v2, v3), out p))
//         {
//             if (p.t1 == i)
//                 res.Add(p.t2);
//             else
//                 res.Add(p.t1);
//         }
//         else
//             res.Add(-1);
//         if (aEdgeList.TryGetValue(new Edge(v3, v1), out p))
//         {
//             if (p.t1 == i)
//                 res.Add(p.t2);
//             else
//                 res.Add(p.t1);
//         }
//         else
//             res.Add(-1);
//         }
//         return res;
//     }
//     public static List GetNeighbors(Mesh aMesh)
//     {
//         var vertexList = FindSharedVertices(aMesh.vertices);
//         var tris = aMesh.triangles;
//         var triangles = new List(tris.Length);
//         foreach (var t in tris)
//             triangles.Add(vertexList[t]);
//         var edges = CreateEdgeList(triangles);
//         return GetNeighbors(edges, triangles);
//     }
// }