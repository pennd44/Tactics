using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ActionArea
{
    public abstract List<Tile> GetTilesInArea(Board board, Tile tile);
}
