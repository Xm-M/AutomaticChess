using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff 
{
    public Chess target;//buff�����ö���
    public string buffName;
    public Buff(Chess target)
    {
        this.target = target;
    }
    public virtual void BuffEffect()
    {
        
    }
    public virtual void BuffReset()
    {

    }
}
