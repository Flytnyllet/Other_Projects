using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public delegate void PlatformCreated(GameObject obj);
    public static event PlatformCreated OnPlatformCreated;

    static bool HasAddedEvent = false;

    private void Awake()
    {
        if (HasAddedEvent)
            return;
        Player.OnPlayerDeath += ResetEvent;
        HasAddedEvent = true;
    }



    public static void Spawned(GameObject go)
    {
        if (OnPlatformCreated == null)
            return;
        OnPlatformCreated(go);
    }

    static void ResetEvent()
    {
        foreach (PlatformCreated item in OnPlatformCreated.GetInvocationList())
            OnPlatformCreated -= item;
        HasAddedEvent = false;
    }


    public float ySpeedOnImpact = 15;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0)
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = ySpeedOnImpact;
                rb.velocity = velocity;
            }
        }
    }
}
