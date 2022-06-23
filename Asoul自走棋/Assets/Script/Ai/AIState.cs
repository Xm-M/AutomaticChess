using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : ScriptableObject
{
    
    public virtual int SelectChess(AIControler ai)
    {
        int n = 0;
        for (int i = 0; i < ai.enemyChoose.Count; i++)
        {
            if (ai.enemyCD[i] < ai.enemyChoose[i].property.CD)
            {
                ai.expect[i] = 0;
            }
            else
            {
                ai.fuzzyModule.Fuzzify("FVAttack", ai.enemyChoose[i].equipWeapon.attack / ai.enemyChoose[i].equipWeapon.attackInterval);
                ai.fuzzyModule.Fuzzify("FVPrice", ai.enemyChoose[i].property.Price);
                ai.fuzzyModule.Fuzzify("FVDefence", ai.enemyChoose[i].property.hpMax);
                ai.expect[i] = ai.fuzzyModule.Defuzzy("FVHope");
            }
        }
        float max = 0;
        for (int i = 0; i < ai.expect.Length; i++)
        {
            if (ai.expect[i] > max)
            {
                max = ai.expect[i];
                n = i;
            }
        }
        //Debug.Log("期望值最高的是" + enemyChoose[n].name);
        return n;
    }
}
