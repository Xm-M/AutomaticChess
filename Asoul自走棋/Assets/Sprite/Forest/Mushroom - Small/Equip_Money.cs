using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip_Money : Weapon
{
    public float money;
    public GameObject effect;
    public override void Attack()
    {
        base.Attack();      
    }
    public override void TakeDamages()
    {
        //GameObject m= ObjectPool.instance.Create(effect);
        //m.transform.parent = master.transform;
        //m.transform.position=master.transform.position;
        MoneyManage.instance.teamM[master.tag].currenMoney += money;
    }
}
