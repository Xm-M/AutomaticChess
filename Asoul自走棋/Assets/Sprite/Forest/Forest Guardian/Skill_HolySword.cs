using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HolySword : Skill
{
    public float heal;
    public float attack;
    public float attackSpeed;
    public override void OnSkillEnter()
    {
        base.OnSkillEnter();
        SkillEffect();
        transform.position=controller.transform.position;
    }
    public override void SkillEffect()
    {
        base.SkillEffect();
        controller.property.GetDamage(heal);
        controller.equipWeapon.attack += attack;
        if(controller.equipWeapon.attackInterval>0.3f)
        controller.equipWeapon.attackInterval -= attackSpeed;
    }
    public override void OnSkillExit()
    {
        base.OnSkillExit();
    }
}
