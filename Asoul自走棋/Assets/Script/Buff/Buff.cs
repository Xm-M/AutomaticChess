using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff 
{
    public Chess target;//buff的作用对象
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
