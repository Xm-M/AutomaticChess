using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFuzzyVariable", menuName = "Fuzzy/FuzzyVariable")]
//ģ�����Ա����������롱�����������ģ�����ϣ���{�����У�Զ}����
public class FuzzyVariable : ScriptableObject
{
    public string variableName;
    public List<FuzztSet> memberSets;
    public void Fuzzify(float val)
    {
        //�ֱ��������ģ�����ϵ������� �������Ǳ����ڸ��Ե�m_Dom��
        for(int i = 0; i < memberSets.Count; i++)
        {
            memberSets[i].CalculateDom(val);
        }
    }
    //ƽ��ֵ��ȥģ��������
    public float Defuzzy()
    {
        float confidenceSum = 0; ;
        float valSum = 0;
        for(int i = 0; i < memberSets.Count; i++)
        {
            confidenceSum += memberSets[i].GetDom();
            //Debug.Log("confidence:" + confidenceSum);
            valSum += memberSets[i].GetDom() * memberSets[i].GetRepresentVal();
            //Debug.Log("valsum:" + valSum);
        }
        //Debug.Log("ans:" + valSum / confidenceSum);
        return valSum / confidenceSum;
    }
}
