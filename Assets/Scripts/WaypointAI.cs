using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAI : MonoBehaviour
{


    public float baseSpeed = 2.5f;
    [SerializeField] private GameObject[] wanderGoals, guardGoals, defendGoals;
    public GameObject target;
    private int goalIndex = 0;
    private int defendIndex = 0;
    public bool enemyPosChosen = false;
    public int rngTest = 0;

    public string state;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rngTest += 1;
        if (rngTest > 255)
        {
            rngTest = 0;
        }
        switch (state)
        {
            case "Wander":
                MovementAI(wanderGoals, baseSpeed, true);
                break;
            case "Guard":
                MovementAI(guardGoals, baseSpeed * 0.5f, false);
                break;
            case "Stop":
                break;
            case "Defend":
                DefenceAI(defendGoals, baseSpeed * 2);
                break;
            case "Chase":
                if (target == null)
                {
                    state = "Wander";
                    break;
                }
                ChaseAI(baseSpeed * 2);
                break;
            default:
                state = "Stop";
                break;
        }

        // extra lines for keeping things tidy and readable :)

    }
    void MovementAI(GameObject[] goals, float speed, bool randomSkips)
    {
        if (goalIndex >= goals.Length)
        {
            goalIndex = 0;
        }
        float wanderDistance = Vector2.Distance(transform.position, goals[goalIndex].transform.position);
        if (wanderDistance > 0.01f)
        {
            MovementSnippet(goals, speed);
        }
        else
        {
            goalIndex += 1;
            if (goalIndex >= goals.Length)
            {
                goalIndex = 0;
            }
            if (randomSkips)
            {
                rngGoalIncrement(goals);
                rngGoalAssignment(goals);
            }
        }
    }
    void ChaseAI(float speed)
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        Vector2 aiPosition = transform.position;
        aiPosition += (direction * speed * Time.deltaTime);
        transform.position = aiPosition;
    }
    void DefenceAI(GameObject[] goals, float speed)
    {
        if (!enemyPosChosen)
        {
            defendIndex = rngTest;
            while (defendIndex >= goals.Length)
            {
                defendIndex -= goals.Length;
            }
            enemyPosChosen = true;
        }
        goalIndex = defendIndex;
        float defendDistance = Vector2.Distance(transform.position, goals[goalIndex].transform.position);
        if (defendDistance > 0.01f)
        {
            MovementSnippet(goals, speed);
        }
    }
    void MovementSnippet(GameObject[] goals, float speed)
    {
        Vector2 direction = (goals[goalIndex].transform.position - transform.position).normalized;
        Vector2 aiPosition = transform.position;
        aiPosition += (direction * speed * Time.deltaTime);
        transform.position = aiPosition;
    }
    void rngGoalIncrement(GameObject[] goals)
    {
        if (rngTest >= 222)
        {
            goalIndex += 1;
            if (goalIndex >= goals.Length)
            {
                goalIndex = 0;
            }
        }
    }
    void rngGoalAssignment(GameObject[] goals)
    {
        if (rngTest == 0)
        {
            goalIndex = Random.Range(0, goals.Length);
        }
    }
}
