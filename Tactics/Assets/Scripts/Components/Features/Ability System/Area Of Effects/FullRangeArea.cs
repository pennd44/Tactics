using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class FullRangeArea : ActionArea
{
public override List<Tile> GetTilesInArea (Board board, Tile tile)
  {
    // ActionRange range = GetComponent<ActionRange>();
    // return range.GetTilesInRange(board);
    List<Tile> tiles = new List<Tile>();
    return tiles;
  }
}
