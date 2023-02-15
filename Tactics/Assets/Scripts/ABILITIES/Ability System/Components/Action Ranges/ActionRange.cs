using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
// public abstract class ActionRange
// {

// }
public abstract class ActionRange : ScriptableObject {
     [SerializeField] public int horizontal = 1;
    public int vertical = int.MaxValue;
    public virtual bool directionOriented { get { return false; }}
    public Character unit;
    protected ActionRange (int hor, int vert){
        this.horizontal = hor;
        this.vertical = vert;
    }
    public virtual List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = board.Search(unit.currentTile, ExpandSearch);
        Filter(retValue);
        return retValue;    
    }
    protected virtual bool ExpandSearch (Tile from, Tile to)
    {
        return (from.distance + 1 <= horizontal);
    }
    protected virtual void Filter(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
           if(tiles[i] == unit.currentTile){
            tiles.RemoveAt(i);
           }
        }

    }
}