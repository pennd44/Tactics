using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : BattleState
{
    protected Tile targetTile;
    public MovingState(BattleStateMachine stateMachine, Tile tile) : base(stateMachine){
        this.targetTile = tile;
    }
    public override void enter() {
        stateMachine.StartCoroutine(Traverse(targetTile));
    }
    public IEnumerator Traverse(Tile end)
    {

        List<Tile> targets = new List<Tile>();
        while (end != null)
        {
            targets.Insert(0, end);
            end = end.prev;
        }
        for (int i = 1; i < targets.Count; i++)
        {
            Tile to = targets[i];
            yield return stateMachine.StartCoroutine(Walk(to));
        }
        stateMachine.setState(new BattleMenuState(stateMachine));

    }
    public IEnumerator Walk(Tile target)
    {

        Vector3 playerPosition = unit.gameObject.transform.position;
        Vector3 tilePosition = target.gameObject.transform.position;
        //  + new Vector3(0, 1.5f, 0);
        while (unit.gameObject.transform.position != tilePosition)
        {
            Turn(tilePosition);
            unit.gameObject.transform.position = Vector3.MoveTowards(unit.gameObject.transform.position, tilePosition, 3.0f * Time.deltaTime);
            yield return null;
        }

    }

    public void Turn(Vector3 target)
    {
        Vector3 direction = (target - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    // public override void handleInput() {
    // }
    public override void Tick(){
    }
    public override void exit(){
        // stateMachine.characters[stateMachine.currentPlayerIndex].canMove = false;
    }

    //Helper Functions
    
    
}