using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAtack : Weapon
{
    public override void Attack()
    {
        base.Attack();
        master.animator.Play("attack");
    }
}
