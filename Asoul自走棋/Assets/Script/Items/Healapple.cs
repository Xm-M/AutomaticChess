using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healapple : Item
{
    public float heal;
    protected override void Effect(Chess chess)
    {
        base.Effect(chess);
        chess.GetDamage(-heal,null);
    }
}
