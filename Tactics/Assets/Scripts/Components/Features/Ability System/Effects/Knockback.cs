using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : ActionEffect
{
    Directions direction;
    // BattleMovement mover;
    Character targ;
    Tile startTile;
    Point endPos;
    BattleStateMachine bsm;
    Board board;
    public override void AffectTarget(GameObject target)
    {
        bsm = GameObject.FindObjectOfType<BattleStateMachine>();
        board = bsm.board;
        //push target in direction unit attacks from x squares or until target falls or hits occupied tile
        direction = user.dir;
        // mover = target.GetComponent<BattleMovement>();
        targ = target.GetComponent<Character>();
        startTile = targ.currentTile;

        board.Search(startTile, ExpandSearch);
        endPos = startTile.pos;
        for (int i = 0; i < ammount; i++)
        {
            endPos = startTile.pos + direction.ToPoint();
            List<Tile> tiles = board.GetTiles(endPos);
            for (int j = 0; j < tiles.Count; j++)
            {
                if(ExpandSearch(startTile, tiles[j]))
                {
                    startTile = tiles[j];
                    break; 
                }
                //targ get hit or targ fall
            }
        }
        // bsm.StartCoroutine(Traverse(startTile, targ));
        // Traverse(startTile);
    }
    private bool ExpandSearch (Tile from, Tile to)
    {
        if((Mathf.Abs(from.height - to.height) > 1))
            return false;
        if ( to.occupied)
            return false;
        Tile currentTileCeiling = board.GetCeiling(from);
        Tile targetTileCeiling = board.GetCeiling(to);
        if (currentTileCeiling != null)
        {
            if ((currentTileCeiling.height-to.height) < 2){
                return false;
            }
        }
        if (targetTileCeiling != null)
        {
            if((targetTileCeiling.height - to.height) < 2)
            {
                return false;
            }
            if ((targetTileCeiling.height - from.height) < 2)
            {
                return false;
            }
        }
        return true;
    }

// public IEnumerator Traverse(Tile end, Character target)
//     {

//         List<Tile> targets = new List<Tile>();
//         while (end != null)
//         {
//             targets.Insert(0, end);
//             end = end.prev;
//         }
//         for (int i = 1; i < targets.Count; i++)
//         {
//             Tile from = targets[i-1];
//             Tile to = targets[i];
//             // Directions dir = from.GetDirections(to);
//             // if (unit.dir != dir)
//             // yield return StartCoroutine(ITurn(to.transform.position));
           
//             // if (Mathf.Abs(from.height - to.height) < 1)
//             //     yield return StartCoroutine(Walk(to));
//             // else if (from.height > to.height)
//             //     yield return StartCoroutine(JumpDown(to));
//             // else if (from.height < to.height)
//             //     yield return StartCoroutine(JumpUp(to));
//         }
//     }
//     public IEnumerator Walk(Tile target)
//     {

//         Vector3 playerPosition = unit.gameObject.transform.position;
//         Vector3 tilePosition = target.gameObject.transform.position;
//         unit.unitAnimator.SetFloat("Speed", 1);
//         while (unit.gameObject.transform.position != tilePosition)
//         {
//             Turn(tilePosition);
//             CameraFollow(); 
//             unit.transform.position = Vector3.MoveTowards(unit.gameObject.transform.position, tilePosition, 5.0f * Time.deltaTime);
//             yield return null;
//         }
//         unit.SetDir();
//         unit.unitAnimator.SetFloat("Speed", 0);
//     }



}
