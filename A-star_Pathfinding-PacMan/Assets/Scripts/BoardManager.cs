using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private static int gameBoardWidth = 28;
    private static int gameBoardHeight = 36;

    public int pelletsCollected;
    public int score = 0;

    public static GameObject[,] gameBoard = new GameObject[gameBoardWidth, gameBoardHeight];

    void Start()
    {
        Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));

        foreach (GameObject o in objects)
        {
            Vector2 pos = o.transform.position;

            if(o.name != "Pacman" && o.name != "Intersections" && o.name != "Non-Intersections" && o.name != "Maze" && o.name != "Pellets")
            {
                if(o.GetComponent<Tile>() != null)
                {
                    if(o.GetComponent<Tile>().isPellet || o.GetComponent<Tile>().isPowerUp)
                    {
                        pelletsCollected++;
                    }
                }

                gameBoard[(int)pos.x, (int)pos.y] = o;
            }
        }
    }
    
    void Update()
    {
        
    }
}
