using UnityEngine;
using UnityEngine.AI;
public class AllyMovement : EntityMovement {
       
    public Transform player;
    public NavMeshAgent agent;
       
    void Start () {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
    }
    public override void Tick(){
        agent.SetDestination(player.position);
        if(Vector3.Distance (transform.position, player.position)>3){
            agent.isStopped = false;
        }else{
            agent.isStopped = true;
        }
    }
}