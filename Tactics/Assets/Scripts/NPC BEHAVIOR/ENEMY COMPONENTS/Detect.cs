using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    private float alertDistance = 5.0f;
       private UnityEngine.AI.NavMeshAgent agent;
       private GameObject player;
       [SerializeField] BattleStateMachine stateMachine;
       //^change this to be automatic
       private void Start() {
            player = GameObject.FindWithTag("Player");
       }
       private void Update() {
              if(DistanceFromPlayer()<=alertDistance)
              {
                     Debug.Log("Player within alert range");
                     //Start battle
                     stateMachine.victoryCondition = new DefeatAllEnemiesCondition();
                     stateMachine.victoryCondition.battleStateMachine = stateMachine;
                     stateMachine.setState(new BattleMenuState(stateMachine));
              }        
       }
       private float DistanceFromPlayer(){
              return Vector3.Distance(player.transform.position, transform.position);
       }
}

