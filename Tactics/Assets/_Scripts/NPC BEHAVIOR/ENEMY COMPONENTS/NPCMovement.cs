using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Character character;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float waypointTolerance = 1;
    [SerializeField] float dwellTime = 3;
    [SerializeField] float runSpeed = 4;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;
    private Vector3 guardPosition;
    float velocity;
    int currentWaypointIndex = 0;
    private void Start() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        character = GetComponent<Character>();
    }
    private void Update() {
        if(character.isDead){
            enabled = false;
            navMeshAgent.enabled = false;
            return;
            //move this to character.Die() so it's not checking every frame
        }
        timeSinceArrivedAtWaypoint += Time.deltaTime;
        PatrolBehavior();
        velocity = navMeshAgent.velocity.magnitude/runSpeed;
        character.unitAnimator.SetFloat("Speed", velocity);
    }
    private void PatrolBehavior()
    {
        Vector3 nextPosition = guardPosition;
        if(patrolPath != null){
            if(AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            nextPosition = GetCurrentWaypoint();
        }
        if(timeSinceArrivedAtWaypoint> dwellTime){
            navMeshAgent.destination = nextPosition;

        }
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
