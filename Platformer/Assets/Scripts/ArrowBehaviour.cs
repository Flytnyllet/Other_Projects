using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public int dmg;
    public string enemy = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != this.gameObject && collision.gameObject.tag != "Player" && collision.gameObject.tag != "GroundCheck")
        {
            if (collision.transform.tag == enemy)
            {
                collision.transform.GetComponent<EnemeyGuy>().TakeDamage(dmg);
            }
            Destroy(this.gameObject);
        }
        Debug.Log(collision.transform.name);
    }
}
