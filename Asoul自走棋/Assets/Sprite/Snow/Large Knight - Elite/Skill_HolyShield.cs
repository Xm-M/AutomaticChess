using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HolyShield : Skill
{
    public override void OnSkillEnter()
    {
        base.OnSkillEnter();
        transform.position=controller.transform.position;
    }
    public override void SkillEffect()
    {
        base.SkillEffect();
        //controller.property.magicResistantCurrent += 20;
        controller.property.armorCurrent += 20;
        controller.unAttackable = true;
    }
    public override void OnSkillExit()
    {
        controller.unAttackable = false;
        base.OnSkillExit();
    }
}
