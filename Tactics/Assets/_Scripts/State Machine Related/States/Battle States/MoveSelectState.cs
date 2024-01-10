using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectState : BattleState
{
    protected List<Tile> movables;
    public MoveSelectState(BattleStateMachine stateMachine) : base(stateMachine){}
    public override void enter(){
        BattleMovementStateMachine mover = unit.mover;
        movables = mover.GetTilesInRange(board);
        // unit.currentTile.distance = 0;
        // movables = board.Search(unit.currentTile, unit.range);
        board.SelectTiles(movables, stateMachine.moveSelect);
    }
    Tile prevTile;
    public override void handleInput() {
        /// Highlight tile mouse is hovering over
        RaycastHit hit;
        Tile hitTile;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.transform.gameObject;
            if (hitObj.tag == "Tile" && hitObj.GetComponent<Tile>().selectable)
            {
                hitTile = hitObj.GetComponent<Tile>();
                if (prevTile != null)
                {
                    prevTile.selected = false;
                    prevTile.changeHighlight(stateMachine.moveSelect);
                }
                prevTile = hitTile;
                hitTile.selected = true;
                hitTile.changeHighlight(stateMachine.moveHover);
                handleClick(hitTile);
            }
        }
    }
    public override void Tick(){
        handleInput();
    }
    public override void exit() {
        board.DeSelectTiles(movables);
    }

    //Helper Functions
    private void handleClick(Tile hitTile)
    {
        //on click move player to tile
        if (Input.GetMouseButtonDown(0))
        {
            if (!hitTile.occupied && hitTile.selectable == true)
            {
                Tile previousTile = unit.currentTile;
                previousTile.occupied = false;
                previousTile.content = null;
                unit.currentTile = hitTile;
                Tile newTile = unit.currentTile;
                newTile.content = unit.gameObject;
                newTile.occupied = true;
                unit.canMove = false;
                stateMachine.setState(new MovingState(stateMachine, hitTile));
            }
        }

    }
    
}
