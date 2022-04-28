using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AstarGhost : MonoBehaviour
{
    public List<Nodes> path = new List<Nodes>();
    public Nodes current, nextStep, pacmanPos, previous;

    public Pacman pacman;

    public bool recalculatePath = false;

    public float speed;

    private void Start()
    {
        FindBestPath();

        previous = current;
        nextStep = path[0];
    }

    private void Update()
    {
        DrawPath();
        Move();
    }

    public void FindBestPath()
    {
        path.Clear();
        List<Nodes> openList = new List<Nodes>();
        List<Nodes> closedList = new List<Nodes>();

        openList.Add(current);

        while (openList.Count != 0)
        {
            openList = openList.OrderBy(entry => entry.h + entry.k).ToList();
            Nodes currentNode = openList[0];
            openList.RemoveAt(0);

            closedList.Add(currentNode);

            foreach (var neighbour in currentNode.adjacent)
            {
                bool inClosedList = false;

                if (currentNode.name == pacmanPos.name)
                {
                    Nodes temp = currentNode;
                    while (temp.name != current.name)
                    {
                        path.Add(temp);
                        temp = temp.parent;
                    }
                    path.Reverse();
                    return;
                }

                if (closedList.Exists(entry => entry.name == neighbour.name))
                {
                    inClosedList = true;
                }
                else if (openList.Exists(entry => entry.name == neighbour.name) && (neighbour.k + neighbour.h) > (DistanceCalculation((Vector2)neighbour.transform.position, (Vector2)pacmanPos.transform.position) + currentNode.k + neighbour.weight))
                {
                    neighbour.h = DistanceCalculation((Vector2)neighbour.transform.position, (Vector2)pacmanPos.transform.position);
                    neighbour.k = currentNode.k + neighbour.weight;

                    neighbour.parent = currentNode;
                }
                else
                {
                    neighbour.h = DistanceCalculation((Vector2)neighbour.transform.position, (Vector2)pacmanPos.transform.position);
                    neighbour.k = currentNode.k + neighbour.weight;

                    neighbour.parent = currentNode;

                    openList.Add(neighbour);
                }
            }
        }
    }

    float DistanceCalculation(Vector2 pA, Vector2 pB)
    {
        float dx = pA.x - pB.x;
        float dy = pA.y - pB.y;

        float d = Mathf.Sqrt(dx * dx + dy * dy);

        return d;
    }

    void Move()
    {
        if (nextStep != current && nextStep != null)
        {
            if (IsPassedTarget())
            {
                current = nextStep;
                transform.localPosition = current.transform.position;

                GameObject correspondingPortal = PortalInteraction(current.transform.position);
                if (correspondingPortal != null)
                {
                    transform.localPosition = correspondingPortal.transform.position;
                    current = correspondingPortal.GetComponent<Nodes>();
                }

                FindBestPath();
                if (path.Count > 0)
                {
                    nextStep = path[0];
                    previous = current;
                    current = null;
                }
            }
            else
            {
                transform.localPosition += (nextStep.transform.position - previous.transform.position).normalized * speed * Time.deltaTime;
            }
        }
        else
        {
            transform.localPosition += (pacman.transform.position - transform.position).normalized * speed * Time.deltaTime;
            if(recalculatePath == true)
            {
                FindBestPath();
                if (path.Count > 0)
                {
                    nextStep = path[0];
                    previous = current;
                    current = null;
                    recalculatePath = false;
                }
            }
        }
    }

    bool IsPassedTarget()
    {
        float targetNode = DistanceFromCurrentNode(nextStep.transform.position);
        float currentNode = DistanceFromCurrentNode(transform.localPosition);

        return currentNode > targetNode;
    }

    float DistanceFromCurrentNode(Vector2 tp)
    {
        Vector2 v = tp - (Vector2)previous.transform.position;
        return v.sqrMagnitude;
    }

    GameObject PortalInteraction(Vector2 p)
    {
        GameObject tile = BoardManager.gameBoard[(int)p.x, (int)p.y];

        if (tile != null)
        {
            if (tile.GetComponent<Tile>() != null)
            {
                if (tile.GetComponent<Tile>().isPortal)
                {
                    GameObject correspondingPortal = tile.GetComponent<Tile>().portalTarget;
                    return correspondingPortal;
                }
            }
        }

        return null;
    }

    void DrawPath()
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i].transform.position, path[i+1].transform.position, Color.cyan);
        }
    }
}
