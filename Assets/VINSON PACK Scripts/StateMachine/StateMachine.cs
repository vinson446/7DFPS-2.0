using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    State currentState;
    public State CurrentState => currentState;

    protected bool inTransition { get; private set; }
    protected State previousState;

    public void ChangeState<T>() where T : State
    {
        T targetState = GetComponent<T>();
        if (targetState == null)
        {
            Debug.Log("Cannot change state- state is not attached to State Machine");
            return;
        }

        InitiateStateChange(targetState);
    }

    public void RevertState()
    {
        if (previousState != null)
        {
            InitiateStateChange(previousState);
        }
    }

    // check
    void InitiateStateChange(State targetState)
    {
        if (currentState != targetState && !inTransition)
        {
            Transition(targetState);
        }
    }

    void Transition(State newState)
    {
        inTransition = true;

        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState();

        inTransition = false;
    }

    private void Update()
    {
        if (currentState != null && !inTransition)
        {
            currentState.Tick();
        }
    }
}
