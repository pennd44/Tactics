using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;



public class ActionSelectState : BattleState
{

    protected List<Tile> actionables;
    public ActionSelectState(BattleStateMachine stateMachine) : base(stateMachine){}
    protected BattleMovement mover;
    protected AbilityHolder abilityHolder;
    protected Ability ability;
    public override void enter() {
        mover = unit.GetComponent<BattleMovement>();
        abilityHolder = unit.GetComponent<AbilityHolder>();
        ability = abilityHolder.ability;
        actionables = ability.GetSelectableTiles(board);
        board.SelectTiles(actionables, stateMachine.actionSelect);
    }
    public override void Tick(){
        handleInput();
    }
    public override void exit(){
        board.DeSelectTiles(actionables);
    }

    Tile prevTile;
    List<Tile> prevTiles;

    public override void handleInput() {
        /// Highlight tile mouse is hovering over
        RaycastHit hit;
        Tile hitTile;
        List<Tile> areaEffect = new List<Tile>();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.transform.gameObject;
            if (hitObj.tag == "Tile" && hitObj.GetComponent<Tile>().selectable)
            {
                hitTile = hitObj.GetComponent<Tile>();
                areaEffect = ability.GetTilesInAOE(board, hitTile);
                if (prevTiles != null)
                {
                    prevTile.selected = false;
                    board.ChangeHighlights(prevTiles, stateMachine.actionSelect);
                }
                prevTile = hitTile;
                prevTiles = areaEffect;
                hitTile.selected = true;
                board.ChangeHighlights(areaEffect, stateMachine.actionHover);
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
                stateMachine.StartCoroutine(mover.ITurn(hitTile.content.transform.position));
                unit.unitAnimator.SetTrigger("attacking");
                ability.Use(ability.GetTilesInAOE(board, hitTile));
                // Character target = hitTile.content.GetComponent<Character>();
                // target.currentHealth -= unit.attack;
                unit.canAct = false;
                stateMachine.setState(new BattleMenuState(stateMachine));
                ui.updateBars();
            }
        }

    }
}