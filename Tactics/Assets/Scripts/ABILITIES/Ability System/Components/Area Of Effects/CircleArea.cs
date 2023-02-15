using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
[CreateAssetMenu(fileName = "CircleArea", menuName = "Abilities/Ability Components/ActionArea/CircleArea", order = 0)]
public class CircleArea : ActionArea
{

    Tile tile;
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
