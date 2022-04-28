using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    RotateTowardsAngle rotate;
    Movements move;
    [SerializeField]
    float speedMultiplier = 2;


    [SerializeField]
    float distanceSpeedDivisor;

    private void Start()
    {
        move = GetComponent<Movements>();
        rotate = GetComponent<RotateTowardsAngle>();
        DieOnCollisionWithBodypart.OnDeath += OnPlayerDeath;
    }

    public void OnRestartGame()
    {
        var newPos = Camera.main.transform.position;
        newPos.z = 0;
        gameObject.transform.position = newPos;
        gameObject.SetActive(true);
        

    }

    private void OnPlayerDeath(GameObject obj)
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        var mousePosPixel = Input.mousePosition;
        var mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosPixel);
        mousePosWorld.z = 0;

        var dir = mousePosWorld - transform.position;
        var speedMultiplier = 1;
        if (Input.GetMouseButton(0))
            speedMultiplier = 2;

        rotate.SetTargetRotationZ(GetMouseRotationZ(dir));
        rotate.Rotate(Time.deltaTime);

        var speed = Vector3.Distance(mousePosWorld, transform.position) / distanceSpeedDivisor;
        move.MoveTowards(speed, speedMultiplier, dir);

    }

    float GetMouseRotationZ(Vector3 dir)
    {
        Quaternion finalRotation = Quaternion.LookRotation(Vector3.forward, dir);
        return finalRotation.eulerAngles.z;
    }
}
