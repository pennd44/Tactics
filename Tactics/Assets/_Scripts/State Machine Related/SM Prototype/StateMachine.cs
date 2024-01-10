using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;

    public void setState(State state)
    {
        if (currentState != null)
        {
            currentState.exit();
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.enter();
        }
    }
     void Update()
    {
        currentState.Tick();
    }
}
