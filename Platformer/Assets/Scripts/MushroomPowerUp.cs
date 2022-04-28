using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomPowerUp : MonoBehaviour
{
    public GameObject arrow;

    public float speed;
    public float fireRate;
    private float nextFire;
    private int lookRight = 1;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && nextFire < Time.time)
        {
            nextFire = Time.time + fireRate;
            
            GameObject temp = Instantiate(arrow, transform.position, Quaternion.identity);

            if(transform.localScale.x > 0)
            {
                lookRight = 1;
            }
            else
            {
                lookRight = -1;
            }

            temp.transform.localScale = new Vector3 (temp.transform.localScale.x * lookRight, transform.localScale.y, transform.localScale.z);

            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(lookRight * speed, 0);

        }
    }
}
