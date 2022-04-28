using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfGrounded : MonoBehaviour {

    private PlayerController player;
    public string powerUpTag = "PowerUp";

    private void Start()
    {
        player = gameObject.GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != powerUpTag)
        {
            player.grounded = true;
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    player.grounded = true;
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.grounded = false;
    }
}
