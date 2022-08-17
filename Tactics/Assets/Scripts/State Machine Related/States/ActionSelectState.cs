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
    protected Ability ability;
    public override void enter() {
        mover = unit.GetComponent<BattleMovement>();
        // ActionRange attack = unit.GetComponent<ActionRange>();
        AbilityHolder abilityHolder = unit.GetComponent<AbilityHolder>();
        // actionables = attack.GetTilesInRange(board);
        Ability ability = abilityHolder.ability;
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
        List<Tile> areaOfEffect = new List<Tile>();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObj = hit.transform.gameObject;
            if (hitObj.tag == "Tile" && hitObj.GetComponent<Tile>().selectable)
            {
                hitTile = hitObj.GetComponent<Tile>();
                Debug.Log((ability.areaOfEffect != null));
                Debug.Log(ability.GetTilesInAOE(board, hitTile));
                areaOfEffect = ability.GetTilesInAOE(board, hitTile);
                if (prevTiles != null)
                {
                    prevTile.selected = false;
                    board.ChangeHighlights(prevTiles, stateMachine.actionSelect);
                    // prevTile.changeHighlight(stateMachine.actionSelect);
                }
                prevTile = hitTile;
                prevTiles = areaOfEffect;
                hitTile.selected = true;
                board.ChangeHighlights(areaOfEffect, stateMachine.actionHover);
                // hitTile.changeHighlight(stateMachine.actionHover);
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