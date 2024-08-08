using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class Board : MonoBehaviour
{
    public List<Tile> tiles;
    public Point[] dirs = new Point[4]
    {
        new Point(0, 1),
        new Point(0, -1),
        new Point(1, 0),
        new Point(-1, 0)
    };
    public void SelectTiles(List<Tile> tiles, Material material)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
            tiles[i].changeHighlight(material);
            tiles[i].selectable = true;
        }
    }
    public void DeSelectTiles(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
            // tiles[i].GetComponent<Renderer>().material.color= tiles[i].originalColor;
            tiles[i].removeHighlight();
            tiles[i].selectable = false;
        }
    }
    public List<Tile> Selectables(List<Tile> tiles)
    {
        List<Tile> selectables = new List<Tile>();
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].selectable)
                selectables.Add(tiles[i]);
        }
        return selectables;
    }
    // public List<Tile> Search(Tile start, int range)
    // {

    //     List<Tile> retValue = new List<Tile>() { start };
    //     Queue<Tile> queue = new Queue<Tile>();
    //     queue.Enqueue(start);
    //     clearMovables();
    //     start.distance = 0;
    //     start.visited = true;
    //     if (queue.Count > 0)
    //     {
    //         do
    //         {
    //             Tile t = queue.Dequeue();
    //             for (int j = 0; j < 4; j++)
    //             {
    //                 Tile tile = getTile(t.pos + dirs[j]);
    //                 if (tile != null && !tile.visited && !tile.occupied)
    //                 {
    //                     tile.distance = t.distance + t.cost;
    //                     tile.visited = true;
    //                     tile.prev = t;
    //                     queue.Enqueue(tile);
    //                     retValue.Add(tile);
    //                 }


    //             }
    //             // distance++;

    //         } while (queue.Peek().distance < range);
    //     }
    //     return retValue;
    // }

    //Turn this search below back on 

    // public List<Tile> Search (Tile start, Func<Tile, Tile, bool> addTile)
    // {
    //     List<Tile> retValue = new List<Tile>();
    //     retValue.Add(start);

    //     ClearSearch();
    //     Queue<Tile> checkNext = new Queue<Tile>();
    //     Queue<Tile> checkNow = new Queue<Tile>();
    //     start.distance = 0;
    //     checkNow.Enqueue(start);
    //     while(checkNow.Count > 0)
    //     {
    //         Tile t = checkNow.Dequeue();
    //         for(int i = 0; i < 4; ++i)
    //         {
    //             Tile next = getTile(t.pos + dirs[i]);
    //             if (next == null || next.distance <= t.distance + 1)
    //                 continue;
    //             if (addTile(t, next))
    //             {
    //                 next.distance = t.distance + 1;
    //                 next.prev = t;
    //                 checkNext.Enqueue(next);
    //                 retValue.Add(next);
    //             }
    //         }
    //         if(checkNow.Count == 0)
    //             SwapReference(ref checkNow, ref checkNext);
    //     }

    //     return retValue;
    // }
    public List<Tile> Search(Tile start, Func<Tile, Tile, bool> addTile)
    {
        List<Tile> retValue = new List<Tile>();
        retValue.Add(start);

        ClearSearch();
        // Queue<Tile> checkNext = new Queue<Tile>();
        Queue<Tile> checkNow = new Queue<Tile>();
        start.distance = 0;
        checkNow.Enqueue(start);
        while (checkNow.Count > 0)
        {
            Tile t = checkNow.Dequeue();
            for (int i = 0; i < 4; ++i)
            {
                List<Tile> nextTiles = GetTiles(t.pos + dirs[i]);
                if (nextTiles == null)
                    continue;
                for (int j = 0; j < nextTiles.Count; ++j)
                {
                    Tile next = nextTiles[j];
                    if (next == null || next.distance <= t.distance + 1)
                        continue;
                    if (addTile(t, next))
                    {
                        next.distance = t.distance + 1;
                        next.prev = t;
                        checkNow.Enqueue(next);
                        retValue.Add(next);
                    }
                }
            }
            // if (checkNow.Count == 0)
            //     SwapReference(ref checkNow, ref checkNext);
        }

        return retValue;
    }
    void SwapReference(ref Queue<Tile> a, ref Queue<Tile> b)
    {
        Queue<Tile> temp = a;
        a = b;
        b = temp;
    }
    void ClearSearch()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].visited = false;
            tiles[i].prev = null;
            tiles[i].distance = int.MaxValue;
        }
    }

    public void clearMovables()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].visited = false;
            tiles[i].prev = null;
            tiles[i].distance = int.MaxValue;
        }
    }

    private void Awake()
    {
        tiles = UnityEngine.Object.FindObjectsOfType<Tile>().ToList();
    }
    public LevelData levelData;
    public List<Quad> quads;
    void Start()
    {
        foreach (Transform child in transform)
        {
            MeshFilter meshFilter = child.GetComponent<MeshFilter>();
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;
            // Quad quad = new Quad(child.TransformPoint(vertices[0]), child.TransformPoint(vertices[1]), child.TransformPoint(vertices[2]), child.TransformPoint(vertices[3]));
            // quads.Add(quad);
            // levelData.quads.Add(quad);
        }
    }

    public Tile getTile(Point p)
    {
        foreach (Tile t in tiles)
        {
            if (t.pos == p)
            {
                return t;
            }
        }
        return null;
    }

    public Tile GetCeiling(Tile t)
    {
        List<Tile> tilesAtCurrentPosition = GetTiles(t.pos);
        Tile currentTileCeiling = null;
        float currentCeilingHeight = float.MaxValue; // to make sure we’re only getting the nearest ceiling
        for (int i = 0; i < tilesAtCurrentPosition.Count; i++)
        {
            if (tilesAtCurrentPosition[i].height > t.height && tilesAtCurrentPosition[i].height < currentCeilingHeight)
            {
                //current tile has a ceiling
                currentTileCeiling = tilesAtCurrentPosition[i];
                currentCeilingHeight = currentTileCeiling.height;
            }
        }
        if (currentTileCeiling != null)
            return currentTileCeiling;
        return null;
    }

    public List<Tile> GetTiles(Point p)
    {
        List<Tile> ts = new List<Tile>();
        foreach (Tile t in tiles)
        {
            if (t.pos == p)
            {
                ts.Add(t);
            }
        }
        if (ts.Count > 0)
            return ts;
        else
            return null;
    }
    public Tile getClosestTile(Vector3 v)
    {
        Tile closest = null;
        float closestDistance = float.MaxValue;

        for (int i = 0; i < tiles.Count; i++)
        {

            var distance = Vector3.Distance(v, tiles[i].transform.position);
            if (distance < closestDistance)
            {
                closest = tiles[i];
                closestDistance = distance;
            }
        }
        // Debug.Log(closest.pos.x + ", " + closest.pos.y);
        return closest;
    }
    public void ChangeHighlights(List<Tile> tiles, Material material)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].changeHighlight(material);
        }
    }


    public void exitBattle()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].occupied = false;
            tiles[i].content = null;
        }
    }
}
