// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;
// using UnityEngine.UIElements;



public class BattleMenuState : BattleState
{

    public BattleMenuState(BattleStateMachine stateMachine) : base(stateMachine){}
    public override void enter() {
        ui.displayBattleMenu(); /// in future, have character as argument and show ui options based on character
    }
    public override void handleInput() {
    }
    public override void Tick(){
        handleInput();
    }
    public override void exit() {
        ui.hideBattleMenu();
    }
}