using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// [System.Serializable]
[CreateAssetMenu(fileName = "InfiniteRange", menuName = "Abilities/Ability Components/ActionRange/InfiniteRange", order = 0)]

public class InfiniteRange : ActionRange 
{
  // public InfiniteRange(int hor, int vert) : base(hor,vert){}
  public override List<Tile> GetTilesInRange (Board board)
  {
    return new List<Tile>(board.tiles);
  }
}
