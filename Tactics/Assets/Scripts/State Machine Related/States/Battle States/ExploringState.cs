using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;



public class ExploringState : BattleState
{
    public ExploringState(BattleStateMachine stateMachine) : base(stateMachine){}
    public override void enter() {
        // Debug.Log("hit enter exploring state");
        stateMachine.cameraController.transform.rotation = Quaternion.Euler(0,0,0);
        stateMachine.victoryCondition = null; 
        for (int i = 0; i < characters.Count; i++) 
        {
             if(characters[i].alliance.type == Alliances.Enemy && !characters[i].isDead){
                    characters[i].gameObject.GetComponent<Detect>().enabled = true;
                }
            characters[i].gameObject.GetComponent<ExploreMovementStateMachine>().enabled = true;
        // Debug.Log(characters[i] + "ExploreMovementSM =" + characters[i].gameObject.GetComponent<ExploreMovementStateMachine>().enabled);
            characters[i].gameObject.GetComponent<NavMeshAgent>().enabled = true;     
                if(characters[i].GetComponent<NPCMovement>()){
                    characters[i].gameObject.GetComponent<NPCMovement>().enabled = true;
                }
        }
        for(int i = 0; i < board.tiles.Count; i++){
            board.tiles[i].occupied = false;
        }
        stateMachine.cameraController.GetComponent<BattleCameraMovement>().enabled = false;
        // Debug.Log("Battle Camera Movement =" + stateMachine.cameraController.GetComponent<BattleCameraMovement>().enabled + "should be false");
                stateMachine.cameraController.GetComponent<ExploreCameraMovement>().enabled = true;
        // Debug.Log("Explore Camera Movement =" + stateMachine.cameraController.GetComponent<ExploreCameraMovement>().enabled + "should be true");
    ui.hideBattleMenu();
    ui.hideResourceBars();
     }
    public override void handleInput() {
        if (Input.GetKeyDown(KeyCode.N))
        {
            /// come back and change first turn to depend on speed
            // stateMachine.setState(new EnterBattleState(stateMachine, characters, board, ui, 0));
            stateMachine.victoryCondition = new DefeatAllEnemiesCondition();
            stateMachine.victoryCondition.battleStateMachine = stateMachine;
            stateMachine.setState(new BattleMenuState(stateMachine));
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ui.alternateCharacterMenu();
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            Collider[] hitColliders = Physics.OverlapSphere(unit.transform.position, unit.interactDistance);
            foreach (var hitCollider in hitColliders)
            {
                // if(hitCollider.GetComponent<Interactable>() != null)
                if(hitCollider.GetComponent<Interactable>() != null)
                {
                    Interactable interactable =  hitCollider.GetComponent<Interactable>();
                    if(interactable.CanInteract(unit)){
                        interactable.ApplyEffects(unit);
                        ui.updateUi();   
                    }
                    // Debug.Log(hitCollider.name);
                }
            }
        }
    }
    public override void Tick(){
        // if(stateMachine.characters[stateMachine.currentPlayerIndex])
        handleInput();
    }
    public override void exit() {
        // Debug.Log("Exiting exploring state");
        for (int i = 0; i < characters.Count; i++) 
            {
                // characters[i].gameObject.GetComponent<ExploreMovementStateMachine>().Exit();
                if(characters[i].alliance.type == Alliances.Enemy){
                    characters[i].gameObject.GetComponent<Detect>().enabled = false;
                }
                characters[i].gameObject.GetComponent<ExploreMovementStateMachine>().enabled = false;
                if(characters[i].GetComponent<NPCMovement>()){

                    characters[i].gameObject.GetComponent<NPCMovement>().enabled = false;
                }
                characters[i].gameObject.GetComponent<NavMeshAgent>().enabled = false;
                // characters[i].gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                characters[i].currentTile = board.getClosestTile(characters[i].gameObject.transform.position);
                characters[i].snapToTile();
            }
        stateMachine.cameraController.GetComponent<ExploreCameraMovement>().enabled = false;
        stateMachine.cameraController.GetComponent<BattleCameraMovement>().enabled = true;
    }
}