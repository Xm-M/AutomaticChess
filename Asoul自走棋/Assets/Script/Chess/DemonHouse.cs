using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonHouse : House
{
    public float extraMoney=0.5f;
    public override void GetDamage(float damage, Chess chess)
    {
        if(chess!=null)
        MoneyManage.instance.teamM[chess.tag].currenMoney += extraMoney * damage * DamageDefence(chess.equipWeapon.attackRange); ;
        base.GetDamage(damage, chess);
    }
}
