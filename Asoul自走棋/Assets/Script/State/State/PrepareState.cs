using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="PrepareState",menuName ="State/PrepareState")]
public class PrepareState : State
{
    public static State instance;
    private void OnEnable()
    {
        if(instance==null)
        instance = this;
    }
    public override void Enter(Chess chess)
    {
        base.Enter(chess);
    }
}
