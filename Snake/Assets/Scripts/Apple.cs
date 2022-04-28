using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {

    public delegate void AppleEatenBy(GameObject eatenBy);
    public static event AppleEatenBy OnAppleEatenBy;

    bool eaten = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (eaten)
            return;

        if (!collision.transform.CompareTag("Head"))
            return;

        var snakeBody = collision.transform.GetComponent<Snake>();
        snakeBody.GenerateBodyParts(1);

        if (OnAppleEatenBy != null)
            OnAppleEatenBy(collision.gameObject);

        Destroy(gameObject);
        eaten = true;
    }


}
