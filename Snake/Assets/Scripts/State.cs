using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State {

    public virtual void OnExit() { }
    public virtual void OnEnter() { }
    public virtual void OnUpdate(float deltaTime) { }

    protected GameObject gameobject;
    protected StateMachine statemachine;


    public State(GameObject gameobject, StateMachine statemachine)
    {
        this.gameobject = gameobject;
        this.statemachine = statemachine;
    }
}

