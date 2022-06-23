using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    
    public List<ToState> toStates;
    public virtual void Excute(Chess chess)
    {
        foreach (var transition in toStates)
        {
            if (transition.transition.ifReach(chess))
            {
                if (chess == null) return;
                if (transition.toState != null) chess.stateController.ChangeState(transition.toState);
                else chess.stateController.RevertToPreState();
            }
        }
    }
    public virtual void Enter(Chess chess)
    {
        
    }
    public virtual void Exit(Chess chess)
    {

    }
}
[Serializable]
public class ToState
{
    public Transition transition;
    public State toState;
}
