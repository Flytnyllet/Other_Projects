using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsAngle : MonoBehaviour
{
    float fractionSum;

    [SerializeField]
    float angleSpeed = 10;

    public float endZ;

    public void Rotate(float deltaTime)
    {
        var rotZ = transform.rotation.eulerAngles.z;
        var speed = angleSpeed * deltaTime;

        float endToRot = endZ - rotZ;
        NormaliseAngle(ref endToRot);

        float rotToEnd = rotZ - endZ;
        NormaliseAngle(ref rotToEnd);

        float angle = -rotToEnd;
        if (endToRot < rotToEnd)
            angle = endToRot;

        if (angle < 0)
            speed = -speed;

        if (Mathf.Abs(angle) < Mathf.Abs(speed))
            speed = angle;

        transform.Rotate(0, 0, speed);
    }
    public void SetTargetRotationZ(float rotation)
    {
        endZ = rotation;
    }

    void NormaliseAngle(ref float angle)
    {
        while (angle < 0)
            angle += 360;

        angle %= 360;

    }
}
