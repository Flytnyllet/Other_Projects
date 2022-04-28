using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    public delegate void PlayerDied();
    public static event PlayerDied OnPlayerDeath;

    public float speed;

    private float movement = 0f;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        OnPlayerDeath += Enemy.ResetEvent;
    }

    private void OnDestroy()
    {
        if (playing)
        {
            OnPlayerDeath();
            foreach (PlayerDied item in OnPlayerDeath.GetInvocationList())
                OnPlayerDeath -= item;

            SceneManager.LoadScene(0);
        }
    }

    private void FixedUpdate()
    {
        movement = Input.GetAxis("Horizontal") * speed;

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector2 velocity = rb.velocity;
        velocity.x = movement;
        rb.velocity = velocity;

        //Vector2 boxSize = new Vector2(boxcastSizeX, boxcastSizeY);
        //wallInfo = Physics2D.BoxCast(transform.position + new Vector3(sprite.bounds.extents.x + boxSize.x, 0, 0) * transform.localScale.x, boxSize, 0, Vector2.zero, boxcastMask);
         
        //boxRef.transform.position = transform.position + new Vector3(sprite.bounds.extents.x + boxSize.x, 0, 0) * transform.localScale.x;
        //boxRef.transform.localScale = boxSize;

        //if (wallInfo)
        //{
        //    velocity.x = 0;
        //}
    }

    bool playing = true;

    private void OnApplicationQuit()
    {
        playing = false;
    }
}
