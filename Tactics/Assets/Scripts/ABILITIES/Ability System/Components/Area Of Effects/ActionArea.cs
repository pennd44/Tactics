// [System.Serializable]
using System.Collections.Generic;
using UnityEngine;
public abstract class ActionArea : ScriptableObject {
    // protected ActionArea (int hor, int vert){
    //     this.horizontal = hor;
    //     this.vertical = vert;
    // }
    public abstract List<Tile> GetTilesInArea(Board board, Tile tile);
}
