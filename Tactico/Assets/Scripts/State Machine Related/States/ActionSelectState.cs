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
        Action attack = unit.GetComponent<Action>();
        actionables = attack.GetTilesInRange(board);
        board.SelectTiles(actionables, Color.red);
        // unit.currentTile.distance = 0;
        // actionables = board.Search(unit.currentTile, unit.attackRange);
        // board.SelectTiles(actionables, Color.red);
    }
    public override void Tick(){
        handleInput();
    }
    public override void exit(){
        board.DeSelectTiles(actionables);
    }

    GameObject prevSelectedTile;
    Color prevColor = Color.white;

    public override void handleInput() {
        /// Highlight tile mouse is hovering over
        RaycastHit hit;
        Tile hitTile;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.transform.gameObject;
            if (hitObj.tag == "Tile")
            {
                hitTile = hitObj.GetComponent<Tile>();
                if (prevSelectedTile != null)
                {
                    prevSelectedTile.GetComponent<Tile>().selected = false;
                    prevSelectedTile.GetComponent<Renderer>().material.color = prevColor; // green -> blue but want green -> white
                }
                prevSelectedTile = hitObj;
                hitTile.selected = true;
                prevColor = hitTile.GetComponent<Renderer>().material.color;
                hitTile.GetComponent<Renderer>().material.color= Color.green;
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