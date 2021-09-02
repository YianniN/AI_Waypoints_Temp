using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private StateMachine aiStateMachine;
    public void Wander()
    {
        aiStateMachine.state = State.Wander;
    }
    public void Stop()
    {
        aiStateMachine.state = State.Stop;
    }
    public void Guard()
    {
        aiStateMachine.state = State.Guard;
    }
    public void Defend()
    {
        aiStateMachine.state = State.Defend;
    }
}
