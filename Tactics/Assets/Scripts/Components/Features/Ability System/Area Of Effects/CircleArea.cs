using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleArea : ActionArea
{
    public int horizontal;
  public int vertical;
  Tile tile;
  public override List<Tile> GetTilesInArea (Board board, Tile tile)
  {
    return board.Search(tile, ExpandSearch);
  }
  bool ExpandSearch (Tile from, Tile to)
  {
    return (from.distance + 1) <= horizontal && Mathf.Abs(to.height - tile.height) <= vertical;
  }
}
