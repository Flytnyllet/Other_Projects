using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDeath(GameObject enemy);
    public static event EnemyDeath OnEnemyDeath;
   

    public static void ResetEvent()
    {
        foreach (EnemyDeath item in OnEnemyDeath.GetInvocationList())
            OnEnemyDeath -= item;
    }

    [SerializeField]
    float ySpeedOnImpact = 15;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;


        var player = collision.transform;
        var pRb = player.GetComponent<Rigidbody2D>();

        var vel = pRb.velocity;



        if (vel.y > 0)
        {
            Destroy(player.gameObject);
            return;
        }

        var eColTop = GetComponent<BoxCollider2D>().bounds.extents.y + transform.position.y;
        var pColBot = player.GetComponent<BoxCollider2D>().bounds.extents.y + player.position.y;

        if (eColTop >= pColBot)
        {
            Destroy(player.gameObject);
            return;
        }

        if(OnEnemyDeath != null)
            OnEnemyDeath(gameObject);
        Destroy(gameObject);


        vel.y = ySpeedOnImpact;
        pRb.velocity = vel;
    }


}
