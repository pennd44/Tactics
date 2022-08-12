using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleMovement : MonoBehaviour
{
    protected Character unit;
    protected Board board;
    // protected Transform jumper;
    protected virtual void Awake() {
        unit = GetComponent<Character>();
        board = GameObject.FindObjectOfType<Board>();
        // jumper = transform.FindChild("Jumper");
    }
    public virtual List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = board.Search(unit.currentTile, ExpandSearch);
        Filter(retValue);
        return retValue;    
    }
    protected virtual bool ExpandSearch (Tile from, Tile to)
    {
        return (from.distance + 1 <= unit.range);
    }
    protected virtual void Filter(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
           if(tiles[i].occupied){
            tiles.RemoveAt(i);
           }
        }

    }
    public abstract IEnumerator Traverse(Tile tile);
    public abstract void Turn(Vector3 target);
    // protected virtual IEnumerator Turn(Directions dir){
    //     TransformLocalEulerTweener t = (TransformLocalEulerTweener) transform.RotateToLocal(dir.ToEuler(), 0.25f, EasingEquations.EaseInOutQuad);
    //     if (Mathf.Approximately(t.startValue.y, 0f) && Mathf.Approximately(t.endValue.y, 270f))
    //         t.startValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
    //     else if (Mathf.Approximately(t.startValue.y, 270) && Mathf.Approximately(t.endValue.y, 0))
    //         t.endValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
    //     unit.dir = dir;
    //     while(t != null)
    //         yield return null;
    // }
}



//getTile return array
//if (t in getTile(currentTile.pos)).height > currentTile.height
//  we have a ceiling
//  if cieling.height - targetTile.height < 1
//      cant access space is too small
//      continue
//  if (t in getTile(targetTile.pos)).height > targetTile.height
//      target tile has a ceiling
//      if targetCieling.height  - targetTile.height < 1


//CHECK IF WE HAVE A CIELING TOO LOW TO GO TO LEAVE 1 METER OF SPACE ABOVE TARGET TILE FLOOR
//CHECK IF TARGET TILE HAS A CIELING TOO LOW TO LEAVE 1 METER OF SPACE ABOVE TARGET TILE FLOOR
//
// Tile [] tilesAtCurrentPosition = board.GetTiles(currentTile.pos);
// Tile currentTileCeiling;
// float currentCeilingHeight = float.MaxValue; // to make sure we’re only getting the nearest ceiling
// for (int i = 0; i currentTile.height && tilesAtCurrentPosition[i].height < currentCeilingHeight;)
// {
// //current tile has a ceiling
// currentTileCeiling = tilesAtCurrentPosition[i];
// currentCeilingHeight = currentTileCeiling.height;
// }
// }

// if (currentTileCeiling.height – targetTile.height < 1)
// {
// //space is large enough for unit to fit
// //I'd make a function to get the tile above a given tile but im not sure off the top of my head how to get around that it would sometimes return a tile and sometimes nothing
// if ( targetTile.GetCeiling() == null)
// //add tile
// if(targetTile.GetCeiling != null && targetTile.GetCeiling().height -targetTile.height)
// {
// //theres enough space on target tile
// //add tile
// }
// }