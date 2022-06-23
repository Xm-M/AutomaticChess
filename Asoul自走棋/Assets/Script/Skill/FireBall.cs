using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Skill
{
    Chess target;
    public float baseDamege = 100;
    public float damage;
    private void Start()
    {
        manaCost = 60;
    }
    public override void OnSkillEnter()
    {
        FindClosestEnemy();
        if(target)
        transform.position = target.transform.position;
        base.OnSkillEnter();
    }
    public void TakeDamage()
    {
        SkillEffect();
        if(target)
        target.property.GetDamage(damage);
    }
    public override void OnSkillExit()=>ObjectPool.instance.Recycle(gameObject);
    public void FindClosestEnemy()
    {
        float dis = 100;
        Chess c=null;
        foreach(var team in GameManage.instance.teams)
        {
            if (team.Key != controller.tag)
            {
                foreach(var enemy in team.Value.team)
                {
                    if (MapManage.Distance(controller.standTile, enemy.standTile) < dis)
                    {
                        dis = MapManage.Distance(controller.standTile, enemy.standTile);
                        c = enemy;
                    }
                }
            }
        }
        target = c;
    }
}
