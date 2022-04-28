using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float minSpeed;
    [SerializeField]
    float maxSpeed;

    [SerializeField]
    float angleSpeed;

    [SerializeField]
    float distanceSpeedDivisor;

    [SerializeField]
    float speedMultiplier = 2;

    Snake snake;

    void Start()
    {
        snake = GetComponent<Snake>();
    }

    void Update()
    {
        var mousePosPixel = Input.mousePosition;
        var mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosPixel);
        mousePosWorld.z = 0;

        var dir = mousePosWorld - transform.position;

        var toAngleZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var currentAngleZ = transform.localRotation.eulerAngles.z;

        
        var minAngleBetween = Mathf.Abs(360 - (toAngleZ + currentAngleZ));

        transform.Rotate(0,0,minAngleBetween);
        var angleZ = transform.localRotation.eulerAngles.z;
        Debug.Log(angleZ);

        //transform.localRotation = Quaternion.Euler(0, 0, currentAngleZ);

        Vector3.Distance(mousePosWorld, transform.position);
        MoveForward(0);

    }

    void MoveForward(float distanceToMouse)
    {
        
        var speed = distanceToMouse / distanceSpeedDivisor;
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);


        if (Input.GetMouseButton(0))
            speed *= speedMultiplier;


        if (distanceToMouse < speed * Time.deltaTime)
            speed = distanceToMouse / Time.deltaTime;


        transform.position += transform.up * speed * Time.deltaTime;
        snake.MoveBody(speed * Time.deltaTime);
    }
}
