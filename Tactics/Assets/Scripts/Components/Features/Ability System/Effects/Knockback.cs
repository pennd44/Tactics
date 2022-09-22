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


        board.Search(startTile, targ.mover.currentMovement.ExpandSearch);
        endPos = startTile.pos;
        for (int i = 0; i < ammount; i++)
        {
            endPos = startTile.pos + direction.ToPoint();
            List<Tile> tiles = board.GetTiles(endPos);
            for (int j = 0; j < tiles.Count + 1; j++)
            {
                if(targ.mover.currentMovement.ExpandSearch(startTile, tiles[j]))
                {
                    startTile = tiles[j];
                    //sometin
                    break;
                }
                if(j == tiles.Count)
                {
                //targ get hit or targ fall

                }
            }
        }
        // bsm.StartCoroutine(Traverse(startTile, targ));
        // Traverse(startTile);
    }
}
