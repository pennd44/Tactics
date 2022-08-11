using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;



public class ActionSelectState : BattleState
{

    protected List<Tile> actionables;
    public ActionSelectState(BattleStateMachine stateMachine) : base(stateMachine){}
    public override void enter() {
        ActionRange attack = unit.GetComponent<ActionRange>();
        actionables = attack.GetTilesInRange(board);
        board.SelectTiles(actionables, stateMachine.actionSelect);
    }
    public override void Tick(){
        handleInput();
    }
    public override void exit(){
        board.DeSelectTiles(actionables);
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
                    prevTile.changeHighlight(stateMachine.actionSelect);
                }
                prevTile = hitTile;
                hitTile.selected = true;
                hitTile.changeHighlight(stateMachine.actionHover);
                handleClick(hitTile);
            }
        }
    }
 

    //Helper Functions
    private void handleClick(Tile hitTile)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hitTile.occupied && hitTile.selectable && hitTile.content.GetComponent<Character>() != null)
            {
                Character target = hitTile.content.GetComponent<Character>();
                target.currentHealth -= unit.attack;
                unit.canAct = false;
                stateMachine.setState(new BattleMenuState(stateMachine));
                ui.updateBars();
            }
        }

    }
}