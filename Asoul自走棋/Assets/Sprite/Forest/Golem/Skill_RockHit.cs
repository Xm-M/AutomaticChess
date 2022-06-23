using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_RockHit : Skill
{
    public List<Tile> tiles;
    public GameObject Tuci;
    public float damage;
    public override void OnSkillEnter()
    {
        base.OnSkillEnter();
        if (!controller.target) return;
        transform.position = controller.target.transform.position;
    }
    public override void SkillEffect()
    {
        if (!controller.target|| !controller.target.standTile) return;
        controller.target.GetDamage(damage * 1.5f,controller);
        base.SkillEffect();
        MapManage.instance.RoundTile(controller.target.standTile,tiles);
        foreach(var tile in tiles)
        {
            GameObject tuci = ObjectPool.instance.Create(Tuci);
            tuci.transform.position = tile.transform.position;
            tuci.tag=controller.tag;
            if (tile.Stander)
            {
                tile.Stander.GetDamage(damage,controller);
            }
        }
    }
}
