using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public Chess self;
    public State currentState;
    public State preState;

    public void Start()
    {
        self = GetComponent<Chess>();
        currentState = PrepareState.instance;
    }

    public void ChangeState(State newState)
    {
        if(currentState)
        currentState.Exit(self);
        preState = currentState;
        currentState = newState;
        if(currentState)
        currentState.Enter(self);
    }

    public void RevertToPreState()
    {
        ChangeState(preState);
    }

    private void Update()
    {
        if (currentState)
            currentState.Excute(self);
    }
}
