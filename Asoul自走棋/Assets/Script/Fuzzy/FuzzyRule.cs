using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFuzzyRule", menuName = "Fuzzy/FuzzyRule")]
public class FuzzyRule:ScriptableObject
{
    public FuzztSet m_pAntecedents;//ǰ��
    public FuzztSet m_pConsequence;//���
    //ģ�������������ǽ������ǰ����� ֮�������������Ŷ�
    
    public void ClearConsequence()
    {
        m_pConsequence.ClearDom();
    }
    public void Calculate()
    {
        //���º��������������
        m_pConsequence.OrWithDom(m_pAntecedents.GetDom());
    }
}
