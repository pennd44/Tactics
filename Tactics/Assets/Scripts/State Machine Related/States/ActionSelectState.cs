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
        board.DeSelectTiles(board.tiles);
    }


    Tile prevTile;
    List<Tile> prevTiles;
    // List<Tile> selectedTiles;

    public override void handleInput() {
        /// Highlight tile mouse is hovering over
        RaycastHit hit;
        Tile hitTile;
        List<Tile> areaEffect = new List<Tile>();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 6;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            GameObject hitObj = hit.transform.gameObject;
                if (prevTiles != null)
                {
                    for (int i = 0; i < prevTiles.Count; i++)
                    {
                        prevTiles[i].selected = false;
                        // selectedTiles.Remove(prevTiles[i]);
                        if(board.Selectables(prevTiles).Contains(prevTiles[i]))
                            prevTiles[i].changeHighlight(stateMachine.actionSelect);
                        else
                            prevTiles[i].removeHighlight();
                            //if another highlight could effect this change this
                    }
                
                }
            if (hitObj.GetComponent<Tile>().selectable)
            //hitObj.tag == "Tile" && 
            {
                hitTile = hitObj.GetComponent<Tile>();
                areaEffect = ability.GetTilesInAOE(board, hitTile);
                prevTile = hitTile;
                prevTiles = areaEffect;
                for (int i = 0; i < areaEffect.Count; i++)
                {
                    areaEffect[i].selected = true;
                    // selectedTiles.Add(areaEffect[i]);
                }
                
                // hitTile.selected = true;
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
            if (hitTile.selectable)
            {
                stateMachine.StartCoroutine(mover.ITurn(hitTile.transform.position));
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