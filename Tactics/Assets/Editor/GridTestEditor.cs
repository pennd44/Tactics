// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;

// [CustomEditor (typeof(GridTest))]
// public class GridTestEditor : Editor
// {
//     Dictionary<int, int> trianglePairs;
//     public override void OnInspectorGUI()
//     {
//         trianglePairs = new Dictionary<int, int>();
//         GridTest gridTest = (GridTest) target;
//         trianglePairs = gridTest.trianglePairs;
//         foreach (KeyValuePair<int, int> kvp in trianglePairs)
//         {
//             EditorGUILayout.IntField(kvp.Key.ToString(), kvp.Value);
//         }
//         DrawDefaultInspector();
//     }
// }
