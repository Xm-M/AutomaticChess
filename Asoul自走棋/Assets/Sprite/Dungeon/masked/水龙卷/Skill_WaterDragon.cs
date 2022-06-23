using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_WaterDragon : Skill
{
    public float continueTime;
    float ctimes;
    public float speed;
    public override void OnSkillEnter()
    {
        base.OnSkillEnter();
        transform.right=controller.transform.right;
        transform.position=controller.transform.position;
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        while (ctimes < continueTime)
        {
            ctimes+=Time.deltaTime;
            transform.position=Vector2.MoveTowards(transform.position,transform.position+transform.right,speed*Time.deltaTime);
            yield return null;        
        }
        ctimes=0;
        OnSkillExit();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Chess>() != null && !collision.CompareTag(controller.tag))
        {
            collision.GetComponent<Chess>().GetDamage(GameManage.instance.BaseThunderDamage*Time.fixedDeltaTime,controller);
        }
    }
}
