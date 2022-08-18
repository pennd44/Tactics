using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CircleArea : ActionArea
{
  [SerializeField] public int horizontal;
  [SerializeField] public int vertical;
  Tile tile;
  public CircleArea(int hor, int vert){
    this.horizontal = hor;
    this.vertical = vert;
  }
  public override List<Tile> GetTilesInArea (Board board, Tile ti)
  {
    tile = ti;
    return board.Search(tile, ExpandSearch);
  }
  bool ExpandSearch (Tile from, Tile to)
  {
    return (from.distance + 1) <= horizontal && Mathf.Abs(to.height - tile.height) <= vertical;
  }
}
