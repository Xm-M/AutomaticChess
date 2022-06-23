using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AIDefenceState", menuName = "State/AIDefenceState")]
public class AIDefenceState : AIState
{
    public static AIState Instance;
    private void OnEnable()
    {
        if (Instance == null) Instance = this;
    }
    public override int SelectChess(AIControler ai)
    {
        List<Chess> enemyChoose = ai.enemyChoose;
        float[] enemyCD = ai.enemyCD;
        FuzzyModule fuzzyModule = ai.fuzzyModule;
        float[] expect = ai.expect;
        int n = 0;
        for (int i = 0; i < enemyChoose.Count; i++)
        {
            Chess chess = enemyChoose[i];
            float realDefence = (chess.property.hpMax + 50 * chess.property.moveSpeed);
            if (enemyCD[i] < enemyChoose[i].property.CD)
            {
                expect[i] = 0;
            }
            else
            {
                fuzzyModule.Fuzzify("FVAttack", chess.equipWeapon.attack);
                fuzzyModule.Fuzzify("FVPrice", enemyChoose[i].property.Price);
                fuzzyModule.Fuzzify("FVDefence", chess.property.hpMax);
                expect[i] = fuzzyModule.Defuzzy("FVHope");
            }
        }
        float max = 0;
        for (int i = 0; i < expect.Length; i++)
        {
            if (expect[i] > max)
            {
                max = expect[i];
                n = i;
            }
        }
        //Debug.Log("期望值最高的是" + enemyChoose[n].name);
        return n;
    }
}

