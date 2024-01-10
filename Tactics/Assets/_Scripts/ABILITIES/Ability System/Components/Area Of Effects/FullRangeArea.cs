using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// [System.Serializable]
[CreateAssetMenu(fileName = "FullRangeArea", menuName = "Abilities/Ability Components/ActionArea/FullRangeArea", order = 0)]

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
