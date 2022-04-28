using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopInScreen : MonoBehaviour
{

    private float cameraLeft;
    private float cameraRight;
    float halfSize;

    void Start()
    {
        halfSize = GetComponent<SpriteRenderer>().size.x/2f;
        var size = Camera.main.orthographicSize * Camera.main.aspect;

        cameraLeft = -size;
        cameraRight = size;
    }

    void LateUpdate()
    {
        var pos = transform.position;

        if (pos.x < cameraLeft - halfSize)
            pos.x = cameraRight;
        else if (pos.x > cameraRight + halfSize)
            pos.x = cameraLeft;
        
        transform.position = pos;
    }
}
