using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Wander,
    Stop
}
public class StateMachine : MonoBehaviour
{
    [SerializeField] private State state;
    private SpriteRenderer sprite;
    private WaypointAI waypointAI;

    /* To use StartCoroutine, the class cannot be void, and must be IEnumerator
     * To use IEnumerators, there must be a "yield return" somewhere
     * "yield return null;" will tell the computer to return to the code the next frame*/
    private IEnumerator WanderState()
    {
        Debug.Log("Entered Wandering state");
        sprite.color = Color.green;
        while (state == State.Wander)
        {
            waypointAI.isMoving = true;
            Debug.Log("Wandering");
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
            waypointAI.isMoving = false;
            yield return null;
        }
        Debug.Log("Exited Stopped state");
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
            default:
                state = State.Stop;
                Debug.Log("Invalid state; defaulted to Stop state");
                StopState();
                break;
        }
    }
}
