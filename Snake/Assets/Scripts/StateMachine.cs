using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour {

    protected State currentState {
        get;
        private set;
    }
    
	void Update ()
    {
        currentState.OnUpdate(Time.deltaTime);
	}

    public void TransitionTo(State nextState)
    {
        if(currentState != null)
            currentState.OnExit();

        currentState = nextState;
        currentState.OnEnter();
    }
}
