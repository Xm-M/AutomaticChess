using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerApple : Item
{
    public float attack;
    protected override void Effect(Chess chess)
    {
        base.Effect(chess);
        chess.equipWeapon.attack += attack;
    }
}
