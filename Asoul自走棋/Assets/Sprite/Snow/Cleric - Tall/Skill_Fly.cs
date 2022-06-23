using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Fly : Skill
{
    public float speed;
    public float damage;
    public override void OnSkillEnter()
    {
        base.OnSkillEnter();
        transform.position = controller.transform.position;
        StartCoroutine(Flying());
    }
    IEnumerator Flying()
    {
        while ((transform.position - controller.target.transform.position).magnitude > 0.01)
        {
            transform.position = Vector2.MoveTowards(transform.position, controller.target.transform.position, speed * Time.deltaTime);
            yield return null;
        }
        OnSkillExit();
    }
    public override void OnSkillExit()
    {
        base.OnSkillExit();
        controller.target.GetDamage(damage,controller);
    }
}
