using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyRuleSet : ScriptableObject
{
    public IFuzzyTerm m_pAcendence;
    public IFuzzyTerm m_pConsedence;
    public void SetConfidenceOfConsequentToZero()
    {
        m_pConsedence.ClearDom();
    }
    //�÷���������ǰ�������������ȣ�ͬʱ���������������������
    public void Calculate()
    {
        m_pConsedence.OrWithDom(m_pAcendence.GetDom());
    }
}
