using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Wander,
    Stop,
    Guard,
    Defend,
    Chase
}
public class StateMachine : MonoBehaviour
{
    public State state;
    public float chaseDistance = 2.5f;
    [SerializeField] private int stateTimer = 0;
    private SpriteRenderer sprite;
    private WaypointAI waypointAI;
    private GameObject player;

    /* To use StartCoroutine, the class cannot be void, and must be IEnumerator
     * To use IEnumerators, there must be a "yield return" somewhere
     * "yield return null;" will tell the computer to return to the code the next frame*/
    private IEnumerator WanderState()
    {
        Debug.Log("Entered Wandering state");
        sprite.color = Color.green;
        while (state == State.Wander)
        {
            Debug.Log("Wandering");
            waypointAI.state = "Wander";

            // extra lines for keeping things tidy and readable :)

            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < chaseDistance && player.activeSelf == true)
            {
                state = State.Chase;
            }
            yield return null;
        }
        Debug.Log("Exited Wandering state");
        NextState();
    }
    private IEnumerator StopState()
    {
        Debug.Log("Entered Stopped state");
        sprite.color = Color.red;
        while (state == State.Stop)
        {
            waypointAI.state = "Stop";
            yield return null;
        }
        Debug.Log("Exited Stopped state");
        NextState();
    }
    private IEnumerator GuardState()
    {
        Debug.Log("Entered Guarding state");
        sprite.color = Color.gray;
        while (state == State.Guard)
        {
            Debug.Log("Guarding");
            waypointAI.state = "Guard";

            // extra lines for keeping things tidy and readable :)

            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < chaseDistance * 2f && player.activeSelf == true)
            {
                state = State.Chase;
            }
            stateTimer++;
            if (stateTimer > 16383)
            {
                state = State.Wander;
                stateTimer = 0;
            }
            yield return null;
        }
        Debug.Log("Exited Guarding state");
        NextState();
    }
    private IEnumerator DefendState()
    {
        Debug.Log("Entered Defending state");
        sprite.color = Color.blue;
        while (state == State.Defend)
        {
            Debug.Log("Defending");
            waypointAI.state = "Defend";
            yield return null;
        }
        Debug.Log("Exited Defending state");
        waypointAI.enemyPosChosen = false;
        NextState();
    }
    private IEnumerator ChaseState()
    {
        Debug.Log("Entered Chasing state");
        sprite.color = Color.black;
        while (state == State.Chase)
        {
            Debug.Log("Chasing");
            waypointAI.target = player;
            waypointAI.state = "Chase";

            // extra lines for keeping things tidy and readable :)

            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance > chaseDistance * 2f)
            {
                state = State.Guard;
            }
            else if (distance < waypointAI.baseSpeed * Time.deltaTime)
            {
                player.SetActive(false);
                state = State.Wander;
            }
            yield return null;
        }
        waypointAI.target = null;
        Debug.Log("Exited Chasing state");
        NextState();
    }
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.LogError("dude this object has no sprite renderer smh");
        }
        waypointAI = GetComponent<WaypointAI>();
        if (waypointAI == null)
        {
            Debug.LogError("dude there's no waypoint AI on this object wtf");
        }
        Player playerFound = FindObjectOfType<Player>();
        if (playerFound != null)
        {
            player = playerFound.gameObject;
        }
        NextState();
    }
    private void NextState()
    {
        switch (state)
        {
            case State.Wander:
                StartCoroutine(WanderState());
                break;
            case State.Stop:
                StartCoroutine(StopState());
                break;
            case State.Guard:
                StartCoroutine(GuardState());
                break;
            case State.Defend:
                StartCoroutine(DefendState());
                break;
            case State.Chase:
                StartCoroutine(ChaseState());
                break;
            default:
                waypointAI.state = "Stop";
                Debug.Log("Invalid state; defaulted to Stop state");
                StopState();
                break;
        }
    }
}
