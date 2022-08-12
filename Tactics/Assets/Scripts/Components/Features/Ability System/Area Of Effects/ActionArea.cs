using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionArea : MonoBehaviour
{
    public abstract List<Tile> GetTilesInArea(Board board, Tile tile);
}
