using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SearchState : State
{
    float radius;
    float speed;

    ChaseState chaseApple;
    PlayerChaseState chasePlayer;
    WallBounceMove sineMovement;

    public SearchState(GameObject gameobject, StateMachine statemachine, float radius, float speed, float playerChaseTime) : base(gameobject, statemachine)
    {
        this.radius = radius;
        this.speed = speed;

        chaseApple = new ChaseState(gameobject, statemachine, speed, this);
        chasePlayer = new PlayerChaseState(gameobject, statemachine, speed, this, radius, playerChaseTime);
        sineMovement = gameobject.GetComponent<WallBounceMove>();
    }


    public override void OnUpdate(float deltaTime)
    {
        var closest = FindClosestObjectOrPlayerInRadius(radius, gameobject);
        if (closest != null)
        {
            ChaseState to = null;
            if (closest.transform.GetComponent<Apple>() != null)
                to = chaseApple;
            else
                to = chasePlayer;
            

            to.SetTarget(closest.transform.gameObject);
            statemachine.TransitionTo(to);
        }else if (!sineMovement.isMoving) {
            sineMovement.StartMove();
        }
    }

    public static GameObject FindClosestObjectOrPlayerInRadius(float radius, GameObject gameobject)
    {
        //kolla efter target i radie
        var hits = FindHitsInRadius(radius, gameobject);
        if (hits.Length > 0)
        {

            //var closest = hits.OrderByDescending(hit => Vector3.Distance(gameobject.transform.position, hit.point)).Last();

            GameObject closest = hits[0].transform.gameObject;
            float closestDistance = Vector3.Distance(closest.transform.position, gameobject.transform.position);


            for (int i = 1; i < hits.Length; i++)
            {
                if (hits[i].transform.GetComponent<PlayerController>() != null) {
                    return hits[i].transform.gameObject;
                }

                var newDistance = Vector3.Distance(hits[i].transform.position, gameobject.transform.position);
                if ( closestDistance > newDistance)
                {
                    closestDistance = newDistance;
                    closest = hits[i].transform.gameObject;
                }
            }

            return closest;
        }

        return null;
    }

    public static RaycastHit2D[] FindHitsInRadius(float radius, GameObject gameobject)
    {
        return Physics2D.CircleCastAll(gameobject.transform.position, radius, Vector2.up, radius, LayerMask.GetMask("Default", "Player"));
    }

    public override void OnExit()
    {
        sineMovement.StopMove();
    }
    
}
