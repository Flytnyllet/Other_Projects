using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBlock : MonoBehaviour
{
    public PlayerController player;
    public GameObject powerUp;

    public Sprite emptyBlock;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //float blockHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y;
        //float powerUpHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y;

        if (player.tag == collision.transform.tag)
        {
            Instantiate(powerUp, new Vector3(transform.position.x, transform.position.y/*transform.position.y + (blockHeight / 2) + (powerUpHeight / 2)*/, transform.position.z), Quaternion.identity);

            ChangeSprite();

            Destroy(this);
        }
    }

    void ChangeSprite()
    {
        GetComponent<SpriteRenderer>().sprite = emptyBlock;
    }
}
