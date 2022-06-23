using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_WindyRing : Skill
{
    public float damage;
    List<Tile> tiles=new List<Tile>();
    public override void OnSkillEnter()
    {
        base.OnSkillEnter();
        transform.position=controller.transform.position;
        MapManage.instance.RoundTile(controller.standTile, tiles);
    }
    public override void SkillEffect()
    {
        base.SkillEffect();
        foreach(var tile in tiles)
        {
            if (tile.Stander) tile.Stander.GetDamage(damage, controller);
        }
        
    }
}
