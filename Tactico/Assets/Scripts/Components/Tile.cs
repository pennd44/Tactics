using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Point pos;
    public float height;
    List<Tile> neighbors = new List<Tile>();
    public int distance = int.MaxValue;
    public Tile prev;
    public GameObject content;
    public bool occupied = false;
    public bool selected = false;
    public bool selectable = false;
    public bool visited = false;
    public int cost = 1;

    private void findNeighbors(){

    }
    private void Awake() {
        pos = new Point(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
        height = transform.position.y;
    }
}
