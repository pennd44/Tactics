using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// [System.Serializable]

[CreateAssetMenu(fileName = "SingleUnitArea", menuName = "Abilities/Ability Components/ActionArea/SingleUnitArea", order = 0)]
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
