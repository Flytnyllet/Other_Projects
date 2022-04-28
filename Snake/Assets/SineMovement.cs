using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    RotateTowardsAngle rotate;

    public bool isRunning = false;
    float passedTime = 0;
    
    float halfWidth;
    float halfHeight;

    [SerializeField]
    float yPerX = 2;

    [SerializeField]
    float xSpeed = 2;

    Vector3 cameraPosition;

    [SerializeField]
    float timeoffsetX = 0;

    [SerializeField]
    float timeoffsetY = 0;

    Snake snake;

    public void StartMovement()
    {
        var xDiff = transform.position.x;
        timeoffsetX = xDiff;

        if (timeoffsetX < 0)
            timeoffsetX = Mathf.Abs(timeoffsetX) + halfWidth * 2;

        timeoffsetX /= xSpeed;

        var yDiff = transform.position.y;
        timeoffsetY = yDiff;

        if (timeoffsetY < 0)
            timeoffsetY = Mathf.Abs(timeoffsetY) + halfHeight * 2;

        timeoffsetY /= xSpeed * yPerX;
        isRunning = true;
    }


    public void StopMovement()
    {
        isRunning = false;
    }
    // Use this for initialization
    void Start()
    {
        var cam = Camera.main;
        cameraPosition = cam.transform.position;
        cameraPosition.z = 0;

        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
        snake = GetComponent<Snake>();
        rotate = GetComponent<RotateTowardsAngle>();
    }

    Vector3 lastPos = Vector3.zero;

    // Update is called once per frame
    void Update()
    {

        if (!isRunning)
            return;
        
        //Xvel = 2;
        //f = xVel / bredd
        var xSec = halfWidth * 2 / xSpeed;

        //hastigheten borde vara f * bredd för x
        // xVel * höjden / (bredd * yPerX) för y
        var fy = xSec / yPerX;

        passedTime += Time.deltaTime;

        var x = LinearSin(xSec, passedTime + timeoffsetX);
        var y = LinearSin(fy, passedTime + timeoffsetY);


        transform.position = cameraPosition + new Vector3(x * halfWidth, y * halfHeight);


        var dir = transform.position - lastPos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotate.SetTargetRotationZ(angle - 90);
        rotate.Rotate(Time.deltaTime);


        snake.MoveBody(xSpeed);
        lastPos = transform.position;

    }

    float LinearSin(float sec, float passedTime)
    {
        var pTime = passedTime + sec / 2;

        float lValue = 0;
        if (pTime * 10 % (20*sec) < (10*sec)) {
            lValue = Mathf.Lerp(0, 1, pTime / sec % 1);
        }
        else {
            lValue = Mathf.Lerp(1,0, pTime / sec % 1);
        }

        return lValue * 2 - 1;
    }
}
