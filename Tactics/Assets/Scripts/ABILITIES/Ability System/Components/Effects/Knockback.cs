using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KnockbackEffect", menuName = "Abilities/Ability Components/ActionEffect/KnockbackEffect", order = 0)]

public class Knockback : ActionEffect
{
    Directions direction;
    // BattleMovement mover;
    Character targ;
    Tile startTile;
    Point endPos;
    BattleStateMachine bsm;
    Board board;
    // public override void AffectTarget(GameObject target, int ammount)
    // {
    //     bsm = GameObject.FindObjectOfType<BattleStateMachine>();
    //     board = bsm.board;
    //     //push target in direction unit attacks from x squares or until target falls or hits occupied tile
    //     direction = user.dir;
    //     // mover = target.GetComponent<BattleMovement>();
    //     targ = target.GetComponent<Character>();
    //     startTile = targ.currentTile;


    //     board.Search(startTile, targ.mover.currentMovement.ExpandSearch);
    //     endPos = startTile.pos;
    //     for (int i = 0; i < ammount; i++)
    //     {
    //         endPos = startTile.pos + direction.ToPoint();
    //         List<Tile> tiles = board.GetTiles(endPos);
    //         for (int j = 0; j < tiles.Count + 1; j++)
    //         {
    //             if(targ.mover.currentMovement.ExpandSearch(startTile, tiles[j]))
    //             {
    //                 startTile = tiles[j];
    //                 //sometin
    //                 break;
    //             }
    //             if(j == tiles.Count)
    //             {
    //             //targ get hit or targ fall

    //             }
    //         }
    //     }
    //     // bsm.StartCoroutine(Traverse(startTile, targ));
    //     // Traverse(startTile);
    // }
    BattleMovementStateMachine battleMovementStateMachine;
 
    public override void AffectTarget(GameObject target, int ammount)
    {
        bsm = GameObject.FindObjectOfType<BattleStateMachine>();
        board = bsm.board;
        targ = target.GetComponent<Character>();

        battleMovementStateMachine = target.GetComponent<BattleMovementStateMachine>();
        battleMovementStateMachine.SetMovement(new KnockbackMovement(battleMovementStateMachine));
        board.Search(targ.currentTile, battleMovementStateMachine.currentMovement.ExpandSearch);
        bsm.StartCoroutine(Sequence());
    }

    IEnumerator Sequence ()
    {
        // yield return battleMovementStateMachine.StartCoroutine(bsm.MoveCamera(targ.transform.position));
        yield return bsm.StartCoroutine(battleMovementStateMachine.currentMovement.Traverse(CalculateTargetTile(targ, user)));
        // stateMachine.setState(new BattleMenuState(stateMachine));

    }
    Tile CalculateTargetTile(Character target, Character unit){
        Directions dir = unit.currentTile.GetDirections(target.currentTile);
        Debug.Log(dir);
        Point point = dir.ToPoint();
        Debug.Log("start" + target.currentTile);
        Debug.Log("destination" + board.getTile(target.currentTile.pos + point));
        ///when switching to getTiles(TILESSSS) find the first tile that is less than target.height + 1
        return board.getTile(target.currentTile.pos + point);
    }
}
