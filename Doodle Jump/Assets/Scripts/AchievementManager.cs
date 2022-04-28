using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {

    private static int discoveredPlatforms;
    private static int killedEnemys;
    private static int playerDeaths;

    [SerializeField]
    Text text;


    void Awake()
    {
        text = GetComponent<Text>();
        Platform.OnPlatformCreated += CheckPlatform;
        Enemy.OnEnemyDeath += OnEnemyDeath;
        Player.OnPlayerDeath += PlayerDied;

        if (playerDeaths == 2)
            text.text = "Third times the charm!";
    }

    private void PlayerDied()
    {
        playerDeaths++;
    }

    private void OnEnemyDeath(GameObject enemy)
    {
        killedEnemys++ ;

        if (killedEnemys == 10)
            text.text = "You killed 10 enemies!";
           
    }

    private void CheckPlatform(GameObject obj)
    {
        if (obj.name.Contains("Enemy"))
            return;

        discoveredPlatforms++;
        if(discoveredPlatforms == 100)
        {
            text.text = "You have discovered 100 platforms!";
        }
    }
}
