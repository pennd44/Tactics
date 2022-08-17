using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SingleUnitArea : ActionArea
{
public override List<Tile> GetTilesInArea (Board board, Tile tile)
  {
    List<Tile> retValue = new List<Tile>();
    if (tile != null)
      retValue.Add(tile);
    return retValue;
  }
}
