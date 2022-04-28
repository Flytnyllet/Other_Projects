using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public PlayerController player;

    public int points;
    public bool gameOver;
    public bool restart;
    public bool victory;

    public Text pointsText;
    public Text gameOverText;
    public Text restartText;
    public Text victoryText;

    private void Update()
    {
        pointsText.text = ("Points: " + points);

        if (gameOver == true)
        {
            gameOverText.text = ("Game Over!");
            restartText.text = ("Press R to Play Again");

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if(victory == true)
        {
            player.enabled = false;
            victoryText.text = ("You Won!");
            restartText.text = ("Press R to Play Again");

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                player.enabled = true;
            }
        }
    }

    private void Start()
    {
        gameOver = false;
        restart = false;
        victory = false;
    }
}
