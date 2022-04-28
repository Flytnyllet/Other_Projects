using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private List<GameObject> BodyParts = new List<GameObject>();

    public Transform snakeBody;
    public GameObject snakePartPrefab;
    public Color bodyColor;

    void Start()
    {
        BodyParts.Add(gameObject);
        GenerateBodyParts(5);
        for (int i = 1; i < BodyParts.Count; i++) {
            BodyParts[i].layer = 10;
        }


    }

    public void Restart()
    {
        for (int i = 1; i < BodyParts.Count; i++) {
            var part = BodyParts[i];
            Destroy(part.gameObject);
        }
        BodyParts.Clear();
        BodyParts.Add(gameObject);


        GenerateBodyParts(5);
        for (int i = 1; i < BodyParts.Count; i++) {
            BodyParts[i].layer = 10;
        }
    }
    

    public void GenerateBodyParts(int count)
    {
        for (int j = 0; j < count; j++) {
            var spawn = Instantiate(snakePartPrefab, BodyParts[BodyParts.Count - 1].transform.position, Quaternion.identity, snakeBody);


            spawn.layer = snakeBody.gameObject.layer;
            spawn.GetComponent<SpriteRenderer>().color = bodyColor;
            BodyParts.Add(spawn);


        }
    }

    public void MoveBody(float speed)
    {
        for (int i = 1; i < BodyParts.Count; i++) {
            Vector2 currentPos = BodyParts[i].transform.position;
            Vector2 lastPos = BodyParts[i - 1].transform.position;

            var radius = BodyParts[i].GetComponent<CircleCollider2D>().radius;

            float dx = currentPos.x - lastPos.x;
            float dy = currentPos.y - lastPos.y;

            var angle = Mathf.Atan2(dy, dx);

            var nx = 2 * radius * Mathf.Cos(angle);
            var ny = 2 * radius * Mathf.Sin(angle);

            currentPos.x = nx + lastPos.x;
            currentPos.y = ny + lastPos.y;

            BodyParts[i].transform.position = Vector2.MoveTowards(BodyParts[i].transform.position, currentPos, speed);
        }
    }
}
