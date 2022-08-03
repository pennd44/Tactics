// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UIElements;


// public class EnterBattleState : BattleState
// {
//     public EnterBattleState(BattleStateMachine stateMachine) : base(stateMachine){}
//     private List<Tile> movables;


//     public override void enter() { 
//         // foreach(Character character in characters){
//         //     character.currentTile = board.getClosestTile(character.gameObject.transform.position);
//         //     character.snapToTile();
//         // }
//     //TEMP
//         unit.currentTile.distance = 0;
//         movables = board.Search(unit.currentTile, unit.range);
//         board.SelectTiles(movables, Color.blue);

//         ui.displayBattleMenu();
//     }
//         GameObject prevSelectedTile;
//         Color prevColor = Color.white;
//     public override void handleInput() {

        
//         RaycastHit hit;
//         Tile hitTile;
//         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//         if (Physics.Raycast(ray, out hit))
//         {
//             GameObject hitObj = hit.transform.gameObject;
//             if (hitObj.tag == "Tile")
//             {
//                 hitTile = hitObj.GetComponent<Tile>();
//                 // if(prevSelectedTile){
//                 //     prevColor= prevSelectedTile.GetComponent<Renderer>().material.color; //blue need white
//                 // }
//                 if (prevSelectedTile != null)
//                 {
//                     prevSelectedTile.GetComponent<Tile>().selected = false;
//                     prevSelectedTile.GetComponent<Renderer>().material.color = prevColor; // green -> blue but want green -> white
//                 }
//                 prevSelectedTile = hitObj;
//                 hitTile.selected = true;
//                 prevColor = hitTile.GetComponent<Renderer>().material.color;
//                 hitTile.GetComponent<Renderer>().material.color= Color.green;
//                 // handleInput(hitTile);
//             }
//         }
//         if (Input.GetKeyDown(KeyCode.N))
//         {
//             ui.hideBattleMenu();
//             stateMachine.setState(new ExploringState(stateMachine));

//             Debug.Log("exiting battle");
//         }

//         ////TEMP
//          if (Input.GetMouseButtonDown(0)){
//             ui.resetBattleMenu();
//             stateMachine.setState(new EnterBattleState(stateMachine));
//          }

//     }
//     public override void Tick(){
//         handleInput();
//     }
//     public override void exit() {
//         for(int i = 0; i < board.tiles.Count; i++){
//             board.tiles[i].selected = false;
//             board.tiles[i].GetComponent<Renderer>().material.color= Color.white;
//         }
//         ////
//         board.DeSelectTiles(movables);
//     }



// //////////////////////////////
//     public int GetNextPlayerIndex()
//     {
//         stateMachine.currentPlayerIndex = (stateMachine.currentPlayerIndex + 1) % characters.Count;
//         return stateMachine.currentPlayerIndex;
//     }


// }