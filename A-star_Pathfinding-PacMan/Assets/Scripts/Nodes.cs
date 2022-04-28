using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public Nodes[] adjacent;
    public Vector2[] validMoves;

    //Variables for A*
    [HideInInspector]
    public Nodes parent = null;
    [HideInInspector]
    public float h = 0;
    [HideInInspector]
    public float k = 0;
    
    public int weight = 1;

    void Start()
    {
        validMoves = new Vector2[adjacent.Length];

        for (int i = 0; i < adjacent.Length; i++)
        {
            Nodes neighbour = adjacent[i];
            Vector2 temp = neighbour.transform.localPosition - transform.localPosition;

            validMoves[i] = temp.normalized;
        }
    }
}
