using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounceMove : MonoBehaviour
{

    float cameraLeft;
    float cameraRight;
    float cameraTop;
    float cameraBottom;

    [SerializeField]
    float speed = 3;

    Movements movement;
    RotateTowardsAngle rotate;

    // Use this for initialization
    void Start()
    {

        var cameraHeight = Camera.main.orthographicSize - 1;
        var cameraWidth = cameraHeight * Camera.main.aspect - 1;

        cameraLeft = -cameraWidth;
        cameraRight = cameraWidth;
        cameraTop = cameraHeight;
        cameraBottom = -cameraHeight;

        movement = GetComponent<Movements>();
        rotate = GetComponent<RotateTowardsAngle>();
        rotate.SetTargetRotationZ(transform.rotation.eulerAngles.z);
        
    }
    [HideInInspector]
    public bool isMoving = false;
    

    public void StartMove()
    {
        isMoving = true;
    }

    public void StopMove()
    {
        isMoving = false;
        reflectingLeft = false;
        reflectingRight = false;
        reflectingUp = false;
        reflectingDown = false;
    }

    bool reflectingLeft = false;
    bool reflectingRight = false;
    bool reflectingUp = false;
    bool reflectingDown = false;


    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
            return;

        rotate.Rotate(Time.deltaTime);
        movement.MoveForward(speed, 2f);


        var pos = transform.position;
        if(pos.x < cameraLeft) {
            Reflect(Vector2.right, ref reflectingLeft);
        }else if (pos.x > cameraLeft){
            reflectingLeft = false;
        }

        if (pos.x > cameraRight) {
            Reflect(Vector2.left, ref reflectingRight);
        }
        else if (pos.x < cameraRight) {
            reflectingRight = false;
        }

        if (pos.y > cameraTop) {
            Reflect(Vector2.down, ref reflectingUp);
        }
        else if (pos.y < cameraTop) {
            reflectingUp = false;
        }

        if (pos.y < cameraBottom) {
            Reflect(Vector2.up, ref reflectingDown);
        }
        else if (pos.y > cameraBottom) {
            reflectingDown = false;
        }


    }

    void Reflect(Vector2 normal, ref bool reflecting)
    {
        if (!reflecting) {
            var newForward = Vector2.Reflect(transform.up, normal);
            float zAngle = Quaternion.LookRotation(Vector3.forward, newForward).eulerAngles.z;
            rotate.SetTargetRotationZ(zAngle);
            reflecting = true;
        }
       
    }
}
