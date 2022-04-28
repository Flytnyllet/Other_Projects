using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsState : State
{
    Vector3 to;
    Vector3 from;
    float deltatime;
    float timePassed;

    State nextState;

    RotateTowardsAngle rotate;
    Snake snake;

    public MoveTowardsState(GameObject gameobject, StateMachine statemachine, Vector3 to, float deltatime, State nextState) : base(gameobject, statemachine)
    {
        this.to = to;
        this.nextState = nextState;
        this.deltatime = deltatime;
        snake = gameobject.GetComponent<Snake>();
        rotate = gameobject.GetComponent<RotateTowardsAngle>();
    }

    public override void OnEnter()
    {
        from = gameobject.transform.position;
        timePassed = 0;

    }

    public override void OnExit()
    {
        float randomAngle = Random.Range(0, 359);
        gameobject.GetComponent<RotateTowardsAngle>().SetTargetRotationZ(randomAngle);
    }

    public override void OnUpdate(float deltaTime)
    {
        timePassed += deltaTime;

        var oldPos = gameobject.transform.position;
        gameobject.transform.position = Vector3.Lerp(from, to, timePassed/deltatime);

        if (timePassed / deltatime >= 1)
            statemachine.TransitionTo(nextState);


        var dir = to - from;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotate.SetTargetRotationZ(angle - 90);
        rotate.Rotate(deltaTime);

        snake.MoveBody((gameobject.transform.position - oldPos).magnitude);
    }
}
