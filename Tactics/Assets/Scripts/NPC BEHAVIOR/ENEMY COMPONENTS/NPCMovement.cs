using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1;
    private Vector3 guardPosition;
    int currentWaypointIndex = 0;
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Update() {
        PatrolBehavior();
        
    }
    private void PatrolBehavior()
    {
        Vector3 nextPosition = guardPosition;
        if(patrolPath != null){
            if(AtWaypoint())
            {
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }
        navMeshAgent.destination = nextPosition;
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private Vector3 GetCurrentWaypoint()
    {
       return patrolPath.GetWaypoint(currentWaypointIndex);
    }

}
