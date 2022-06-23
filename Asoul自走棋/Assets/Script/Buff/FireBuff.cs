using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBuff : Buff
{
    protected float damage;
    protected float continueTimes;
    protected float interval;
    protected float times;
    FireBuff(Chess target):base(target)
    {
        damage = 10;
        continueTimes = 10;
        interval = 0.5f;
        buffName = "FireBuff";
    }
    public override void BuffEffect()
    {
        base.BuffEffect();
        times += Time.deltaTime;
        continueTimes -= Time.deltaTime;
        if (continueTimes < 0)
        {
            return;
        }
        else if (times > interval)
        {
            target.property.GetDamage(damage);
            times = 0;
        }
    }
    public void BuffFinish()
    {
        //target.buffController.RemoveBuff(this.buffName);
    }
}
