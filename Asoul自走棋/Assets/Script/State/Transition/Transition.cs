using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : ScriptableObject
{
    public virtual bool ifReach(Chess chess)
    {
        return false;
    }
}
