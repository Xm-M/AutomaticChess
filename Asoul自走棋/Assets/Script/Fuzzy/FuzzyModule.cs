using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzzyModule : MonoBehaviour
{
    //ÿ��ai��ӵ��һ��fzmʵ��
    //ͨ�����ַ���ģ������
    public string moduleName;
    //ģ�����Ա�����list 
    public List<FuzzyVariable> VariableList; 
    //һ��������Ӧһ��ģ�����Ա��� ���硰���롱={�����У�Զ}����������={�ͣ��У���}��
    public Dictionary<string, FuzzyVariable> varMap;
    //�����˸�ģ���µ�����ģ������
    public List<FuzzyRule> fuzzyRules;
    //���ڴ���һ��ģ������
    private void Start()
    {
        varMap = new Dictionary<string, FuzzyVariable>();
        for(int i = 0; i < VariableList.Count; i++)
        {
            varMap.Add(VariableList[i].name, VariableList[i]);
        }
    }
    public void ClearAllConsequents()
    {
        for (int i = 0; i < fuzzyRules.Count; i++)
        {
            fuzzyRules[i].ClearConsequence();
        }
    }
    //����ָ��ģ�����Ա�����ģ��������,�����ģ������ĵ�һ��
    //����Fuzzify("����",5);Ȼ��
    public void Fuzzify(string flvName,float val)
    {
        if (!varMap.ContainsKey(flvName)) Debug.LogError("û�����ģ������");
        else varMap[flvName].Fuzzify(val);//���������� Ȼ������Ƿ��ڸ��Ե�dom��
    }
    //����ģ������ ��ȥģ��������ȥģ����
    //ȥģ����Ӧ�������һ���ˣ�һ���������ֵ��ʱ���õ���
    public float Defuzzy(string key)
    {
        if (!varMap.ContainsKey(key)) Debug.LogError("û�����ģ������2");
        //������к����������
        ClearAllConsequents();
        //����ģ������ĵڶ���
        for(int i = 0; i < fuzzyRules.Count; i++)
        {
            fuzzyRules[i].Calculate();
        }
        //����ģ������ĵ�����
        float defuzzy = varMap[key].Defuzzy();
        //Debug.Log("���õ�������ֵΪ��" + defuzzy);
        return defuzzy;
    }

}
