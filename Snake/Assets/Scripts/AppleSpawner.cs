using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour {

    float cameraLeft;
    float cameraRight;
    float cameraTop;
    float cameraBottom;

    [SerializeField]
    GameObject apple;

    [SerializeField]
    Transform appleParent;

    [SerializeField]
    int initialAppleCount = 3;

	void Start () {
        var cameraHeight = Camera.main.orthographicSize;
        var cameraWidth = cameraHeight * Camera.main.aspect;
        cameraLeft = -cameraWidth;
        cameraRight = cameraWidth;
        cameraTop = cameraHeight;
        cameraBottom = -cameraHeight;

        for (int i = 0; i < initialAppleCount; i++)
            SpawnApple();

        Apple.OnAppleEatenBy += OnAppleEaten;
        RestartGame.OnRestartGame += OnRestartGame;
    }

    private void OnRestartGame()
    {
        var apples = GameObject.FindObjectsOfType<Apple>();
        foreach (var apple in apples) {
            Destroy(apple.gameObject);
        }

        for (int i = 0; i < initialAppleCount; i++)
            SpawnApple();
    }

    private void OnAppleEaten(GameObject obj)
    {
        SpawnApple();
    }

    void Update () {
    }

    void SpawnApple()
    {

        var appleRadius = GetObjRadius(apple);
        var maxX = cameraRight - appleRadius;
        var maxY = cameraTop - appleRadius;
        Vector3 spawn = new Vector3
        {
            x = UnityEngine.Random.Range(cameraLeft, maxX) + appleRadius / 2f,
            y = UnityEngine.Random.Range(cameraBottom, maxY) + appleRadius / 2f
        };
        Instantiate(apple, spawn, Quaternion.identity, appleParent);
    }

    private float GetObjRadius(GameObject go)
    {
        var obj = Instantiate(go);
        var radius = obj.GetComponent<CircleCollider2D>().radius;
        Destroy(obj);
        return radius;
    }
}
