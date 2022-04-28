using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public float smoothing;

    private Vector3 currentVelocity;

    private void LateUpdate()
    {
        if (followTarget == null)
            return;

        if (followTarget.position.y > transform.position.y)
        {
            Vector3 updatePos = new Vector3(transform.position.x, followTarget.position.y, transform.position.z);

            //Smoothing kan vara skit, testa utan
            //transform.position = updatePos;

            transform.position = Vector3.SmoothDamp(transform.position, updatePos, ref currentVelocity, smoothing * Time.deltaTime);
        }
    }
}
