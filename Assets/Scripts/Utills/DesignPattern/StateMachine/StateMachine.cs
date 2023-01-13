using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : class
{
    private T owner;
    private State<T> curState;
    private State<T> beforeState;

    public void Init(T owner, State<T> startState)
    {
        this.owner = owner;
        curState = null;
        beforeState = null;

        ChangeState(startState);
    }

    public void Update()
    {
        if (curState == null)
            return;

        curState.Update(owner);
    }

    public void ChangeState(State<T> newState)
    {
        if (newState == null)
            return;

        if (curState != null)
        {
            beforeState = curState;   
            curState.Exit(owner);
        }

        curState = newState;
        curState.Enter(owner);
    }

    public void ChangeBefore()
    {
        if (beforeState == null)
            return;

        ChangeState(beforeState);
    }
}
