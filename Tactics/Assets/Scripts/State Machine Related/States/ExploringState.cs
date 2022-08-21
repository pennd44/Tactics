using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;



public class ExploringState : BattleState
{
    public ExploringState(BattleStateMachine stateMachine) : base(stateMachine){}
    public override void enter() {
        stateMachine.cameraController.transform.rotation = Quaternion.Euler(0,0,0);
        stateMachine.victoryCondition = null; 
        for (int i = 0; i < characters.Count; i++) 
        {
            characters[i].gameObject.GetComponent<Movement>().enabled = true;
            characters[i].gameObject.GetComponent<NavMeshAgent>().enabled = true;            
        }
        for(int i = 0; i < board.tiles.Count; i++){
            board.tiles[i].occupied = false;
        }
        stateMachine.cameraController.GetComponent<BattleCameraMovement>().enabled = false;
                stateMachine.cameraController.GetComponent<ExploreCameraMovement>().enabled = true;

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
        if(Input.GetKeyDown(KeyCode.F))
        {
            Collider[] hitColliders = Physics.OverlapSphere(unit.transform.position, unit.interactDistance);
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.GetComponent<Interactable>() != null)
                {
                    Debug.Log(hitCollider.name);
                }
                
            }
        }
    }
    public override void Tick(){
        handleInput();
    }
    public override void exit() {
        for (int i = 0; i < characters.Count; i++) 
            {
                characters[i].gameObject.GetComponent<Movement>().exit();
                characters[i].gameObject.GetComponent<Movement>().enabled = false;
                characters[i].gameObject.GetComponent<NavMeshAgent>().enabled = false;
                // characters[i].gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                characters[i].currentTile = board.getClosestTile(characters[i].gameObject.transform.position);
                characters[i].snapToTile();
            }
        stateMachine.cameraController.GetComponent<ExploreCameraMovement>().enabled = false;
        stateMachine.cameraController.GetComponent<BattleCameraMovement>().enabled = true;
    }
}