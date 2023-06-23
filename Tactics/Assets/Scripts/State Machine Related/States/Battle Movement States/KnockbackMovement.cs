using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KnockbackMovement : BattleMovement
{
    public KnockbackMovement(BattleMovementStateMachine stateMachine) : base(stateMachine){}
    public Directions direction;
    public override bool ExpandSearch (Tile from, Tile to)
    {
        if((Mathf.Abs(from.height - to.height) > 1))
            return false;
        if ( to.occupied)
            return false; //hit wall/person
        Tile currentTileCeiling = board.GetCeiling(from);
        Tile targetTileCeiling = board.GetCeiling(to);
        if (currentTileCeiling != null)
        {
            if ((currentTileCeiling.height-to.height) < 2){
                return false;
            }
        }
        if (targetTileCeiling != null)
        {
            if((targetTileCeiling.height - to.height) < 2)
            {
                return false;
            }
            if ((targetTileCeiling.height - from.height) < 2)
            {
                return false;
            }
        }
        return base.ExpandSearch(from, to);
    }

    public override IEnumerator Traverse(Tile end)
    {
        Debug.Log("traversing bitch");
        Debug.Log("end tile " + end);

        List<Tile> targets = new List<Tile>();
        while (end != null)
        {
            targets.Insert(0, end);
            Debug.Log("prev " + end.prev);
            end = end.prev;
        }
        for (int i = 1; i < targets.Count; i++)
        {
            Tile from = targets[i-1];
            Tile to = targets[i];
            // Directions dir = from.GetDirections(to);
            // if (unit.dir != dir)
            // yield return StartCoroutine(ITurn(to.transform.position));
            if (Mathf.Abs(from.height - to.height) < 1)
            {
                Debug.Log("stumbling bitch");
                yield return stateMachine.StartCoroutine(Stumble(to));
            }
            else if (from.height > to.height)
            {
            Debug.Log("falling bitch");
                yield return stateMachine.StartCoroutine(Fall(to));
            }
            else if (from.height < to.height)
            {
            Debug.Log("slamming bitch");   
                yield return stateMachine.StartCoroutine(Slam(to));
            }
        }
    }
    public IEnumerator Stumble(Tile target)
    {
        Debug.Log("in stumbling bitch");
        Vector3 playerPosition = unit.gameObject.transform.position;
        Vector3 tilePosition = target.gameObject.transform.position;
        unit.unitAnimator.SetFloat("Speed", 1);
        //set animation to stumbling
        while (unit.gameObject.transform.position != tilePosition)
        {
            Debug.Log("in while");

            // Turn(tilePosition);
            CameraFollow(); 
            unit.transform.position = Vector3.MoveTowards(unit.gameObject.transform.position, tilePosition, 5.0f * Time.deltaTime);
            yield return null;
        }
        unit.SetDir();
        unit.unitAnimator.SetFloat("Speed", 0);
    }

    public override void Turn(Vector3 target)
    {
        Vector3 direction = (target - unit.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        unit.transform.rotation = Quaternion.Slerp(unit.transform.rotation, lookRotation, Time.deltaTime * 5f);

    }
    public IEnumerator Slam(Tile target){
       Vector3 playerPosition = unit.gameObject.transform.position;
        Vector3 tilePosition = target.gameObject.transform.position;
        Vector3 tileY = new Vector3(playerPosition.x, tilePosition.y, playerPosition.z);
        // unit.unitAnimator.SetFloat("Speed", 1);
        while(unit.gameObject.transform.position.y != tilePosition.y)
        {
            // Turn(tilePosition); i think disabled? 
            unit.transform.position = Vector3.MoveTowards(unit.gameObject.transform.position, tileY, 5.0f * Time.deltaTime);
            yield return null;
        }
        while (unit.gameObject.transform.position != tilePosition)
        {
            // Turn(tilePosition); 
            unit.transform.position = Vector3.MoveTowards(unit.gameObject.transform.position, tilePosition, 2.0f * Time.deltaTime);
            yield return null;
        }
        // unit.unitAnimator.SetFloat("Speed", 0);
    }
    public IEnumerator Fall(Tile target){
       Vector3 playerPosition = unit.gameObject.transform.position;
        Vector3 tilePosition = target.gameObject.transform.position;
        Vector3 tileXZ = new Vector3(tilePosition.x, playerPosition.y, tilePosition.z);
        // unit.unitAnimator.SetFloat("Speed", 1);
        while(unit.gameObject.transform.position.x != tilePosition.x && unit.gameObject.transform.position.z != tilePosition.z)
        {
            //animation stumble
            // Turn(tilePosition); 
            unit.transform.position = Vector3.MoveTowards(unit.gameObject.transform.position, tileXZ, 5.0f * Time.deltaTime);
            yield return null;
        }
        while (unit.gameObject.transform.position != tilePosition)
        {
            // Turn(tilePosition); 
            //set animation to falling
            unit.transform.position = Vector3.MoveTowards(unit.gameObject.transform.position, tilePosition, 5.0f * Time.deltaTime);
            yield return null;
        }
        // unit.unitAnimator.SetFloat("Speed", 0);
    }
    
}