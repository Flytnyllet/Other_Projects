using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    private PlayerController player;
    public float knockbackY;
    public float knockbackX;
    public int playerDamageTakenInEditor;

    bool forced = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(forced == false)
            {
                player.TakeDamage(playerDamageTakenInEditor);

                StartCoroutine(player.Knockback(0.002f, knockbackY, knockbackX, Vector3.up, Vector3.left));
                forced = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            forced = false;
        }
    }
}
