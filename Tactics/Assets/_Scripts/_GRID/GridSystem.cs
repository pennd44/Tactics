using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int _length;
    private int _width;
    private float _cellSize;
    private List<GridObject> [,] _gridObjects;
    public GridSystem(int length, int width, float cellSize){
        this._length = length;
        this._width = width;
        this._cellSize = cellSize;

        _gridObjects = new List<GridObject>[width, length];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                _gridObjects[x,z] = new List<GridObject>();
                Point point = new Point(x,z);
                // _gridObjects[x,z].Add(new GridObject(this, point));
            }
        }

    }
    public Vector3 GetWorldPosition(Point point)
    {
        return new Vector3(point.x * _cellSize, 0, point.y * _cellSize);
    }
    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _length; z++)
            {
                // GameObject.Instantiate(debugPrefab)
            }
        }
    }
}
