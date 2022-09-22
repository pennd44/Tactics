using UnityEngine;
using UnityEngine.AI;
public class AllyMovement : EntityMovement {
    private Transform player;
    private NavMeshAgent agent;
    public AllyMovement(GameObject gameObject, Transform target) : base(gameObject){
        player = target;
    }
    public override void enter()
    {
       agent = gameObject.GetComponent<NavMeshAgent>();
       Debug.Log(player);
    }
    public override void Tick(){
        agent.SetDestination(player.position);
        if(Vector3.Distance (gameObject.transform.position, player.position)>3){
            agent.isStopped = false;
            character.unitAnimator.SetFloat("Speed", 1);

        }else{
            agent.isStopped = true;
            character.unitAnimator.SetFloat("Speed", 0);
        }
    }
}