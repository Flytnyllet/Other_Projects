using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{

    [SerializeField]
    float minSpeed;
    [SerializeField]
    float maxSpeed;

    Snake snake;

    // Use this for initialization
    void Start()
    {
        snake = GetComponent<Snake>();
    }

    public void MoveForward(float speed, float speedMultiplier)
    {
        var realSpeed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        realSpeed *= speedMultiplier;

        //distance is not forward aligned
        if (realSpeed < speed * Time.deltaTime)
            speed = realSpeed / Time.deltaTime;


        transform.position += transform.up * speed * Time.deltaTime;
        snake.MoveBody(speed * Time.deltaTime);
    }

    public void MoveTowards(float speed, float speedMultiplier, Vector3 point)
    {
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);
        speed *= speedMultiplier;

        var velocity = transform.up * speed * Time.deltaTime;
        var maxVelocity = transform.position - point;

        if (maxVelocity.sqrMagnitude < velocity.sqrMagnitude)
            velocity = maxVelocity;
        
        transform.position += velocity;
        snake.MoveBody((float)(speed * Time.deltaTime));
    }
}
