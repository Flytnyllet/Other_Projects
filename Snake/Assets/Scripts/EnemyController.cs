using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : StateMachine
{

    [SerializeField]
    float initialSpeed = 5;

    [SerializeField]
    float initialRadius = 8;

    [SerializeField]
    float initialChaseTime = 4;

    [SerializeField]
    float entryTime = 4;

    [SerializeField]
    Transform spawn;

    SearchState sState;
    MoveTowardsState mState;

    void Start()
    {
        sState = new SearchState(gameObject, this, initialRadius, initialSpeed, initialChaseTime);
        mState = new MoveTowardsState(gameObject, this, new Vector3(-12, 0), entryTime, sState);
        TransitionTo(mState);

        RestartGame.OnRestartGame += OnGameRestarted;
    }

    private void OnGameRestarted()
    {
        transform.position = spawn.position;
        TransitionTo(mState);

        var player = GameObject.Find("Player");
        player.transform.GetChild(1).gameObject.SetActive(true);
        var playerController = player.GetComponentInChildren<PlayerController>();
        playerController.OnRestartGame();

        var snakes = GameObject.FindObjectsOfType<Snake>();
        for (int i = 0; i < snakes.Length; i++) {
            var snake = snakes[i];
            snake.Restart();
        }
    }
}
