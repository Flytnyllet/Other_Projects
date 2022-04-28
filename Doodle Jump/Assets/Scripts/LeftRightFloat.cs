using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LeftRightFloat : MonoBehaviour
{

    private float cameraLeft;
    private float cameraRight;
    float halfSize;

    public float speed;
    Rigidbody2D rb;

    bool headingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var size = Camera.main.orthographicSize * Camera.main.aspect;
        halfSize = GetComponent<SpriteRenderer>().size.x / 2f;

        cameraLeft = -size;
        cameraRight = size;
    }

    void LateUpdate()
    {
        var pos = transform.position;

        var scale = transform.localScale;

        if (pos.x < cameraLeft + halfSize && !headingRight || pos.x > cameraRight - halfSize && headingRight)
        {
            scale.x = -scale.x;
            speed = -speed;
            headingRight = !headingRight;
        }


        transform.localScale = scale;

        var vel = rb.velocity;
        vel.x = speed;
        rb.velocity = vel;

    }
}
