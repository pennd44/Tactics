using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public int healthCost = 0;
    public int staminaCost;
    public int kiCost;
    ActionArea areaOfEffect;
    ActionRange actionRange;
    List<ActionEffect> effects;
    ActionTargets targetFilter;
    // ActionCost?
    public virtual List<Tile> GetSelectableTiles(Board board){
        return actionRange.GetTilesInRange(board);
    }
    public virtual List<Tile> GetTilesInAOE(Tile tile, Board board){
        return areaOfEffect.GetTilesInArea(board, tile);
    }
    public virtual void Use(List<Tile> tiles){
        for (int i = 0; i < tiles.Count; i++)
        {
            if(targetFilter.CheckHit(tiles[i]))
            {
                for (int j = 0; j < effects.Count; j++)
                {
                    effects[i].AffectTarget(tiles[i].content);
                }
            }
        }
    }

    //show tiles in range
    //highlight area of effect
    //Use
    //Apply effect to targets in AOE
    //check if tile meets filter requirements
}
