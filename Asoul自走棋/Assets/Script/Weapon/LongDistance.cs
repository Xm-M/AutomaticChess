using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDistance : Weapon
{
    public GameObject bullet;
    public Transform shootPos;
    public virtual void Shoot()
    {
        Bullet b= ObjectPool.instance.Create(bullet).GetComponent<Bullet>();
        b.target = master.target.transform;
        b.shooter = this;
        b.transform.position = shootPos.transform.position;
        b.transform.right = master.target.transform.position - transform.position;
    }
}
