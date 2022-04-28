using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGhost : MonoBehaviour
{
    public enum State
    {
        Chase,
        Disperse,
        Scared
    }

    public float speed;
    public Nodes startNode;

    State currentState = State.Disperse;
    State previousState;

    public GameObject pacman;
    private Nodes current, target, previous;
    private Vector2 dir, nextDir;

    private int iterateState;
    private float stateTimer;

    public int disperseTimer1;
    public int chaseTimer1;
    public int disperseTimer2;
    public int chaseTimer2;
    public int disperseTimer3;
    public int chaseTimer3;
    public int disperseTimer4;

    void Start()
    {
        Nodes node = FetchCurrentNodePos(transform.position);
        
        if (node != null)
        {
            current = node;
        }
        dir = Vector2.right;
        previous = current;

        Vector2 pacmanPos = pacman.transform.position;
        Vector2 targetTile = new Vector2(Mathf.RoundToInt(pacmanPos.x), Mathf.RoundToInt(pacmanPos.y));
        target = FetchCurrentNodePos(targetTile);
    }

    void Update()
    {
        UpdateState();
        Move();
    }

    void Move()
    {
        if (target != current && target != null)
        {
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

                target = PickNode();
                previous = current;
                current = null;
            }
            else
            {
                transform.localPosition += (Vector3)dir * speed * Time.deltaTime;
            }
        }
    }

    void UpdateState()
    {
        if (currentState != State.Scared)
        {
            stateTimer += Time.deltaTime;

            if (iterateState == 1)
            {
                if (currentState == State.Disperse && stateTimer > disperseTimer1)
                {
                    ChangeState(State.Chase);
                    stateTimer = 0;
                }

                if (currentState == State.Chase && stateTimer > chaseTimer1)
                {
                    iterateState = 2;
                    ChangeState(State.Disperse);
                    stateTimer = 0;
                }
            }
            else if (iterateState == 2)
            {
                if (currentState == State.Disperse && stateTimer > disperseTimer2)
                {
                    ChangeState(State.Chase);
                    stateTimer = 0;
                }

                if (currentState == State.Chase && stateTimer > chaseTimer2)
                {
                    iterateState = 3;
                    ChangeState(State.Disperse);
                    stateTimer = 0;
                }
            }
            else if (iterateState == 3)
            {
                if (currentState == State.Disperse && stateTimer > disperseTimer3)
                {
                    ChangeState(State.Chase);
                    stateTimer = 0;
                }

                if (currentState == State.Chase && stateTimer > chaseTimer3)
                {
                    iterateState = 4;
                    ChangeState(State.Disperse);
                    stateTimer = 0;
                }
            }
            else if (iterateState == 4)
            {
                if (currentState == State.Disperse && stateTimer > disperseTimer4)
                {
                    ChangeState(State.Chase);
                    stateTimer = 0;
                }
            }
        }
        else if (currentState == State.Scared)
        {

        }
    }

    void ChangeState(State s)
    {
        currentState = s;
    }

    Nodes FetchCurrentNodePos(Vector2 p)
    {
        GameObject tile = BoardManager.gameBoard[(int)p.x, (int)p.y];

        if (tile != null)
        {
            if (tile.GetComponent<Nodes>() != null)
            {
                return tile.GetComponent<Nodes>();
            }
        }

        return null;
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

    float DistanceFromCurrentNode(Vector2 tp)
    {
        Vector2 v = tp - (Vector2)previous.transform.position;
        return v.sqrMagnitude;
    }

    bool IsPassedTarget()
    {
        float targetNode = DistanceFromCurrentNode(target.transform.position);
        float currentNode = DistanceFromCurrentNode(transform.localPosition);

        return currentNode > targetNode;
    }

    float DistanceCalculation(Vector2 pA, Vector2 pB)
    {
        float dx = pA.x - pB.x;
        float dy = pA.y - pB.y;

        float d = Mathf.Sqrt(dx * dx + dy * dy);

        return d;
    }

    Nodes PickNode()
    {
        Vector2 targetTile = Vector2.zero;
        Vector2 pacmanPos = pacman.transform.position;

        targetTile = new Vector2(Mathf.RoundToInt(pacmanPos.x), Mathf.RoundToInt(pacmanPos.y));

        Nodes nodeToMoveTo = null;
        Nodes[] foundNodes = new Nodes[4];
        Vector2[] foundNodesDir = new Vector2[4];

        int validNodes = 0;

        for (int i = 0; i < current.adjacent.Length; i++)
        {
            if (current.validMoves[i] != dir * -1)
            {
                foundNodes[validNodes] = current.adjacent[i];
                foundNodesDir[validNodes] = current.validMoves[i];
                validNodes++;
            }
        }

        if (foundNodes.Length == 1)
        {
            nodeToMoveTo = foundNodes[0];
            dir = foundNodesDir[0];
        }

        if (foundNodes.Length > 1)
        {
            float tempDist = 10000f;

            for (int i = 0; i < foundNodes.Length; i++)
            {
                if (foundNodesDir[i] != Vector2.zero)
                {
                    float dist = DistanceCalculation(foundNodes[i].transform.position, targetTile);

                    if (dist < tempDist)
                    {
                        tempDist = dist;
                        nodeToMoveTo = foundNodes[i];
                        dir = foundNodesDir[i];
                    }
                }
            }
        }

        return nodeToMoveTo;
    }
}

