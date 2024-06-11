using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Point _pos;
    private Directions _direction;
    private int to_height;
    private int from_height;
    public Vector3[] vertices = new Vector3[4];
    public int[] triangles = new int[6];
}
