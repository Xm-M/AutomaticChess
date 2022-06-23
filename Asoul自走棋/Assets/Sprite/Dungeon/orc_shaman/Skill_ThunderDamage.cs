using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ThunderDamage : Skill
{
    public override void OnSkillEnter()
    {
        base.OnSkillEnter();
        SkillEffect();
    }
    public override void SkillEffect()
    {
        base.SkillEffect();
        transform.position=controller.transform.position;
        GameManage.instance.BaseThunderDamage += 10;
    }
}
