using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Chess
{
    public float bloodPercent=0.1f;
    public GameObject Heal;
    protected override void Start()
    {    
        base.Start();
        EventController.Instance.AddListener(instanceID + EventName.WhenAttackTakeDamage.ToString(), Suckingblood);
    }
    public void Suckingblood()
    {
        GetDamage(-equipWeapon.attack * bloodPercent,null);
        ObjectPool.instance.Create(Heal).transform.position=transform.position;
    }
}
