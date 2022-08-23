using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class InfiniteRange : ActionRange 
{
  public InfiniteRange(int hor, int vert) : base(hor,vert){}
  public override List<Tile> GetTilesInRange (Board board)
  {
    return new List<Tile>(board.tiles);
  }
}
