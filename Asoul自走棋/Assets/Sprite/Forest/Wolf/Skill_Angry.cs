using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Angry : Skill
{
    public GameObject PowerUp;
    public override void OnSkillEnter()
    {
        base.OnSkillEnter();
        SkillEffect();
    }
    public override void SkillEffect()
    {
        base.SkillEffect();
        if(controller.equipWeapon.attackInterval>0.3f)
        controller.equipWeapon.attackInterval -= 0.1f;
        ObjectPool.instance.Create(PowerUp).transform.position=controller.transform.position;
    }
}
