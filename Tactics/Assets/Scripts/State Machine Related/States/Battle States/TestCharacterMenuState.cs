using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;



public class TestCharacterMenuState : BattleState
{
    public TestCharacterMenuState(BattleStateMachine stateMachine) : base(stateMachine){}
    public override void enter() {
        Debug.Log("entered test character menu state");
        for (int i = 0; i < characters.Count; i++) 
        {
            characters[i].gameObject.GetComponent<ExploreMovementStateMachine>().enabled = false;
        Debug.Log(characters[i] + "ExploreMovementSM =" + characters[i].gameObject.GetComponent<ExploreMovementStateMachine>().enabled);
            characters[i].gameObject.GetComponent<NavMeshAgent>().enabled = false;            
        }
        stateMachine.cameraController.GetComponent<BattleCameraMovement>().enabled = false;
        Debug.Log("Battle Camera Movement =" + stateMachine.cameraController.GetComponent<BattleCameraMovement>().enabled + "should be false");

         ui.displayCharacterMenu();
     }
    public override void handleInput() {
         if (Input.GetKeyDown(KeyCode.M))
        {
            stateMachine.setState(new ExploringState(stateMachine));
        }
    }
    public override void Tick(){
        handleInput();
    }
    public override void exit() {
        Debug.Log("Exiting test character menu state");
        // for (int i = 0; i < characters.Count; i++) 
        //     {
        //         characters[i].gameObject.GetComponent<ExploreMovementStateMachine>().enabled = true;
        //         characters[i].gameObject.GetComponent<NavMeshAgent>().enabled = true;
        //     }
        // stateMachine.cameraController.GetComponent<ExploreCameraMovement>().enabled = true;
    }
}