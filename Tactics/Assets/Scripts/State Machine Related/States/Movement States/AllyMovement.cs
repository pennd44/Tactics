using UnityEngine;
using UnityEngine.AI;
public class AllyMovement : EntityMovement {
    private Transform player;
    private NavMeshAgent agent;
    public AllyMovement(GameObject gameObject, Transform player) : base(gameObject){
        this.player = player;
    }
    public override void enter()
    {
       NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
    }
    public override void Tick(){
        agent.SetDestination(player.position);
        if(Vector3.Distance (gameObject.transform.position, player.position)>3){
            agent.isStopped = false;
        }else{
            agent.isStopped = true;
        }
    }
}