using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

public class ExploreMovementStateMachine : StateMachine, ISaveable
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

    public object CaptureState()
    {
        return new SerializableVector3(transform.position);
    }

    public void RestoreState(object state)
    {
        SerializableVector3 position = (SerializableVector3) state;
        GetComponent<NavMeshAgent>().enabled = false;
        transform.position = position.ToVector();
        GetComponent<NavMeshAgent>().enabled = true;
    }
}
