using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnCollisionWithBodypart : MonoBehaviour {

    public delegate void Death(GameObject obj);
    public static event Death OnDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Contains("Part") || collision.name.Contains("Head")) {
            if (OnDeath != null)
                OnDeath(gameObject);
        }
    }
}
