using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class InfiniteRange : ActionRange 
{
  public override List<Tile> GetTilesInRange (Board board)
  {
    return new List<Tile>(board.tiles);
  }
}
