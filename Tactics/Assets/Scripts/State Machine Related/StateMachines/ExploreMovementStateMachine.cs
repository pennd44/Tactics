using UnityEngine;

public class ExploreMovementStateMachine : StateMachine
{   
    [SerializeField] private Roles role;
    Transform target;

    private void Start() {
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
        for (int i = 0; i < units.Length; i++)
        {
            if(units[i].role == Roles.Player)
            {
                return units[i].transform;
            }
        }
        return null;
    }
}
