using UnityEngine;
using Newtonsoft.Json.Linq;
using Tactics.Saving;
using UnityEngine.AI;

public class ExploreMovementStateMachine : StateMachine, IJsonSaveable
{   
    [SerializeField] private Roles role;
    Transform target;

    private void OnEnable() {
        // Debug.Log("ExploreCameraMovementSM OnEnable");

        switch (role)
        {
            case Roles.Player:
            setState(new PlayerMovement(gameObject));
            break;
            case Roles.Ally:
             setState(new AllyMovement(gameObject, FindPlayer()));
            break;
            case Roles.Enemy:
            setState(new EnemyMovement(gameObject));
            break;
        }
    }

    private void SetPlayer()
    {
        
    }
    private void SetAlly()
    {
        FindPlayer();
        setState(new AllyMovement(gameObject, target));
    }
    private void SetEnemy()
    {

    }
    private Transform FindPlayer()
    {
        ExploreMovementStateMachine [] units = GameObject.FindObjectsOfType<ExploreMovementStateMachine>();
        // Debug.Log(units.Length);
        for (int i = 0; i < units.Length; i++)
        {
            if(units[i].role == Roles.Player)
            {
                return units[i].transform;
            }
        }
        return null;
    }
    private void OnDisable() {
        currentState.exit();
    }
    //Saving
        public JToken CaptureAsJToken()
        {
            return transform.position.ToToken();
        }

        public void RestoreFromJToken(JToken state)
        {
        //     const navMeshAgent = GetComponent<NavMeshAgent>();
        //     navMeshAgent.enabled = false;
        //     transform.position = state.ToVector3();
        //     navMeshAgent.enabled = true;
            // GetComponent<ActionScheduler>().CancelCurrentAction();
        }

}
