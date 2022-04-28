using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerChaseState : ChaseState {

    float radius;
    float playerChaseTime;

    float timePassed;

    public PlayerChaseState(GameObject gameobject, StateMachine statemachine, float speed, State previousState, float radius, float playerChaseTime) : base(gameobject, statemachine, speed, previousState)
    {
        this.radius = radius;
        this.playerChaseTime = playerChaseTime;
    }

    //jaga i 4 sek eller om något kommer närmre


    public override void OnEnter()
    {
        //starta timer
        timePassed = 0;
    }

    public override void OnExit()
    {
        //nollställa timer
        timePassed = 0;
    }

    public override void OnUpdate(float deltaTime)
    {
        timePassed += deltaTime;

        if(timePassed > playerChaseTime)
        {
            //sök efter äpplen på vägen -- 
            var apple = SearchState.FindHitsInRadius(radius, gameobject)
                .Where(obj => obj.transform.GetComponent<Apple>() != null)
                .OrderByDescending(hit => Vector3.Distance(gameobject.transform.position, hit.point)).ToArray();


            if(apple.Length != 0)
            {
                var chase = new ChaseState(gameobject, statemachine, speed, previousState);
                chase.SetTarget(apple[apple.Length-1].transform.gameObject);
                statemachine.TransitionTo(chase);

            }

        }
        else
        {
            base.OnUpdate(deltaTime);
        }
    }
    
}
