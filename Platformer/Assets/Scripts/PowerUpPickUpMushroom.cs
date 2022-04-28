using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickUpMushroom : MonoBehaviour
{
    public string playerTag = "Player";

    public GameObject pickUpEffect;
    private MushroomPowerUp playerPowerUp;
    public GameObject arrow;

    public float fireRate;
    public float speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == playerTag)
        {
            //Instantiate(pickUpEffect, transform.position, transform.rotation);
            if (collision.transform.GetComponent<MushroomPowerUp>() == null)
            { 
                collision.gameObject.AddComponent<MushroomPowerUp>();
                playerPowerUp = collision.transform.GetComponent<MushroomPowerUp>();
                playerPowerUp.fireRate = fireRate;
                playerPowerUp.speed = speed;
                playerPowerUp.arrow = arrow;
            }

            Destroy(gameObject);
        }
    }
}
