using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Heal : Skill
{
    public float heal;
    public GameObject effect;
    public override void SkillEffect()
    {
        foreach(var chess in GameManage.instance.teams[controller.tag].team)
        {
            chess.property.GetDamage(heal);
            ObjectPool.instance.Create(effect).transform.position=chess.transform.position;
        }
    }
    public override void OnSkillEnter()
    {
        base.OnSkillEnter();
        SkillEffect();
    }
}
