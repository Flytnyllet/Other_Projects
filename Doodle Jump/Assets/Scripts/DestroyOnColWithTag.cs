using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnColWithTag : MonoBehaviour {

    [SerializeField]
    string tag;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag) && collision.relativeVelocity.y <= 0)
        {
            Destroy(gameObject);
        }
    }
}
