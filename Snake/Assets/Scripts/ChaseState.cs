using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{

    protected GameObject target;
    protected Movements move;
    protected RotateTowardsAngle rotation;
    protected float speed;

    protected State previousState;

    public ChaseState(GameObject gameobject, StateMachine statemachine, float speed, State previousState) : base(gameobject, statemachine)
    {
        this.speed = speed;
        move = gameobject.GetComponent<Movements>();
        rotation = gameobject.GetComponent<RotateTowardsAngle>();
        this.previousState = previousState;
    }
    

    public void SetTarget(GameObject target)
    {
        this.target = target;

    }

    public override void OnUpdate(float deltaTime)
    {
        if (target == null)
        {
            statemachine.TransitionTo(previousState);
            return;
        }
        
        var diff = target.transform.position - gameobject.transform.position;
        var angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
        rotation.SetTargetRotationZ(angle);

        rotation.Rotate(deltaTime);
        move.MoveTowards(speed, 2, diff);
        
    }
}
