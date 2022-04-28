using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    public BoardManager gameBoard;
    public AstarGhost pinky;

    public float speed;

    private Vector2 dir;
    private Vector2 nextDir;

    private int eatenPellets;

    public Nodes current, previous, target;

    void Start()
    {
        gameBoard = GameObject.Find("Board").GetComponent<BoardManager>();

        Nodes node = FetchCurrentNodePos(transform.localPosition);

        if (node != null)
        {
            current = node;
        }

        dir = Vector2.left;
        ChangeDirection(dir);
    }

    void Update()
    {
        //Debug.Log("Score: " + gameBoard.score);
        GottaCheckDatInput();
        Move();
        FlipNRotatePacman();
        EatPellet();
    }

    void GottaCheckDatInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeDirection(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeDirection(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeDirection(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeDirection(Vector2.down);
        }
    }

    Nodes CanPacmanMove(Vector2 d)
    {
        Nodes nodeToMoveTo = null;

        for (int i = 0; i < current.adjacent.Length; i++)
        {
            if (current.validMoves[i] == d)
            {
                nodeToMoveTo = current.adjacent[i];
                break;
            }
        }
        return nodeToMoveTo;
    }

    GameObject FetchTileAtCurrentPos(Vector2 p)
    {
        int tileX = Mathf.RoundToInt(p.x);
        int tileY = Mathf.RoundToInt(p.y);

        GameObject tile = BoardManager.gameBoard[tileX, tileY];

        if (tile != null)
            return tile;

        return null;
    }

    void EatPellet()
    {
        GameObject o = FetchTileAtCurrentPos(transform.position);

        if (o != null)
        {
            Tile t = o.GetComponent<Tile>();

            if (t != null)
            {
                if (!t.isConsumed && (t.isPellet || t.isPowerUp))
                {
                    o.GetComponent<SpriteRenderer>().enabled = false;
                    t.isConsumed = true;
                    gameBoard.score += 10;
                    eatenPellets++;
                }
            }
        }
    }

    void ChangeDirection(Vector2 d)
    {
        if (d != dir)
            nextDir = d;

        if (current != null)
        {
            Nodes nextNode = CanPacmanMove(d);

            if (nextNode != null)
            {
                dir = d;
                target = nextNode;
                previous = current;
                current = null;
            }
        }
    }

    void Move()
    {
        if (target != current && target != null)
        {
            if (nextDir == dir * -1)
            {
                dir *= -1;

                Nodes temp = target;
                target = previous;
                previous = temp;
            }

            if (IsPassedTarget())
            {
                current = target;
                transform.localPosition = current.transform.position;

                GameObject correspondingPortal = PortalInteraction(current.transform.position);
                if (correspondingPortal != null)
                {
                    transform.localPosition = correspondingPortal.transform.position;

                    current = correspondingPortal.GetComponent<Nodes>();
                }

                Nodes nodeToMoveTo = CanPacmanMove(nextDir);

                if (nodeToMoveTo != null)
                    dir = nextDir;

                if (nodeToMoveTo == null)
                    nodeToMoveTo = CanPacmanMove(dir);

                if (nodeToMoveTo != null)
                {
                    pinky.pacmanPos = nodeToMoveTo;
                    target = nodeToMoveTo;
                    previous = current;
                    current = null;
                }
                else
                {
                    pinky.pacmanPos = target;
                    dir = Vector2.zero;
                }
                pinky.recalculatePath = true;
            }
            else
            {
                transform.localPosition += (Vector3)dir * speed * Time.deltaTime;
            }
        }
    }

    void MoveToNextNode(Vector2 d)
    {
        Nodes nodeToMoveTo = CanPacmanMove(d);

        if (nodeToMoveTo != null)
        {
            transform.localPosition = nodeToMoveTo.transform.position;
            current = nodeToMoveTo;
        }
    }

    void FlipNRotatePacman()
    {
        if (dir == Vector2.left)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (dir == Vector2.right)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (dir == Vector2.up)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (dir == Vector2.down)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }
    }

    Nodes FetchCurrentNodePos(Vector2 p)
    {
        GameObject tile = BoardManager.gameBoard[(int)p.x, (int)p.y];

        if (tile != null)
        {
            return tile.GetComponent<Nodes>();
        }

        return null;
    }

    bool IsPassedTarget()
    {
        float targetNode = DistanceFromCurrentNode(target.transform.position);
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
}
