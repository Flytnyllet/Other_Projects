using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraDoubleJump : MonoBehaviour
{
    public string playerTag = "Player";
    public string groundCheckTag = "GroundCheck";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerTag || collision.tag == groundCheckTag)
        {
            if(collision.tag == playerTag)
            {
                collision.GetComponent<PlayerController>().checkDoubleJump = true;
                Destroy(this.gameObject);
            }
            else
            {
                collision.transform.parent.GetComponent<PlayerController>().checkDoubleJump = true;
                Destroy(this.gameObject);
            }
        }
    }
}
