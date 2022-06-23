using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Chess
{
    public GameObject small;
    public override void Death()
    {
        base.Death();
        Summon();
    }
    public void Summon()
    {
        if (!GameManage.instance.ifGameStart) return;
        Chess c = ObjectPool.instance.Create(small).GetComponent<Chess>();
        standTile.ChessEnter(c);
        c.tag = tag;
        GameManage.instance.teams[c.tag].AddMember(c);
    }
}
