using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Darkdragon : Skill
{
    public static float damage=30;
    public static Skill_Darkdragon dark;
    public float speed;
    public bool ifmove;
    public override void OnSkillEnter()
    {
        base.OnSkillEnter();
        transform.position=controller.transform.position;
        if (dark != null)
        {
            dark.controller = controller;
            damage += 10;
            dark.SkillEffect();
            OnSkillExit();
        }
        else
        {
            dark = this;
            SkillEffect();
        }
    }
    public override void SkillEffect()
    {
        PlayAudioa();
        base.SkillEffect();
        if(!ifmove)
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        ifmove = true;
        while ((transform.position - controller.target.transform.position).magnitude > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, controller.target.transform.position,speed*Time.deltaTime);
            yield return null;  
        }
        ifmove = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Chess>() != null && !collision.CompareTag(controller.tag))
        {
            collision.GetComponent<Chess>().GetDamage(damage * Time.fixedDeltaTime,controller);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controller.GetComponent<Chess>() && !collision.CompareTag(controller.tag))
        {
            controller.GetComponent<Chess>().GetDamage(damage * Time.fixedDeltaTime,controller);
        }
    }
}
