using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ThousandBird : Skill
{
    public GameObject effect;
    public float speed;
    public float flyTime;
    float stimes;
    public override void OnSkillEnter()
    {
        GetComponent<Animator>().SetBool("miss", false);
        base.OnSkillEnter();
        transform.position=new Vector2(controller.transform.position.x,controller.target.transform.position.y);
        transform.right = controller.transform.right;
    }
    public void Fly()
    {
        StartCoroutine(Flying());
    }
    IEnumerator Flying()
    {
        while (stimes<flyTime)
        {
            stimes+=Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, transform.position+transform.right, speed * Time.deltaTime);
            yield return null;
        }
        stimes = 0;
        GetComponent<Animator>().SetBool("miss", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Chess>() && !collision.CompareTag(controller.tag))
        {
            collision.GetComponent<Chess>().GetDamage(GameManage.instance.BaseThunderDamage, controller);
            ObjectPool.instance.Create(effect).transform.position=collision.transform.position;
        }
    }
}
