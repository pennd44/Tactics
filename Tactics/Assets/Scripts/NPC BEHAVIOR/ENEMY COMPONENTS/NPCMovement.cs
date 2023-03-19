using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1;
    private Vector3 guardPosition;
    int currentWaypointIndex = 0;
    private void PatrolBehavior()
    {
        Vector3 nextPosition = guardPosition;
        if(patrolPath != null){
            if(AtWaypoint())
            {
                CycleWaypoint();
            }
            nextPosition = GetCuttentWaypoint();
        }
    }

    private bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCuttentWaypoint());
        return distanceToWaypoint < waypointTolerance;
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private Vector3 GetCuttentWaypoint()
    {
       return patrolPath.GetWaypoint(currentWaypointIndex);
    }
}
