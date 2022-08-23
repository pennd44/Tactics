using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ActionArea
{
    // protected ActionArea (int hor, int vert){
    //     this.horizontal = hor;
    //     this.vertical = vert;
    // }
    public abstract List<Tile> GetTilesInArea(Board board, Tile tile);
}
