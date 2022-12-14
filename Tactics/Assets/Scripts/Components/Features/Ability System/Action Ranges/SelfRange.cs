using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class SelfRange : ActionRange 
{
  public SelfRange(int hor, int vert) : base(hor,vert){}
  public override List<Tile> GetTilesInRange (Board board)
  {
    List<Tile> retValue = new List<Tile>(1);
    retValue.Add(unit.currentTile);
    return retValue; 
  }
}
