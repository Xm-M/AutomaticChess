using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFuzzySet",menuName = "Fuzzy/FuzzySet")]
//��Ȼÿ����Ҫ�� ����ÿ���������Ӧ���Ƕ����İ�
//ģ�����ϻ��� ��Ӧ����һ��������
public class FuzztSet:ScriptableObject,IFuzzyTerm
{
    public string fuzzySetName;
    public float m_Dom;//��ǰ������
    public float m_Max;//���������ֵ Ҳ������ν�Ĵ���ֵ
    public Vector3 setRange;
    public Vector3 domRange;
    //����������
    public void SetRange(Vector3 setRange,Vector3 domRange)
    {
        this.setRange = setRange;
        this.domRange = domRange;
        m_Max = domRange.y;//һ�����ֵ������ֵ
    }
    //���������Ȳ�����������m_Dom��
    //���CalculateDom��ʵ���������Ⱥ������Ѵ����ֵ���������ȣ�ģ������
    //Ҫ��д�Ļ�һ�㶼������
    public virtual float CalculateDom(float val)
    {
        if (val > setRange.y && val < setRange.z)
        {
            float k = (domRange.y - domRange.z) / (setRange.y - setRange.z);
            m_Dom = k * (val - setRange.y) + domRange.y;
        }
        else if (val <= setRange.y && val > setRange.x)
        {
            float k = (domRange.y - domRange.x) / (setRange.y - setRange.x);
            m_Dom = k * (val - setRange.y) + domRange.y;
        }
        else m_Dom = 0;
        //Debug.Log(fuzzySetName+":"+ m_Dom);
        return m_Dom;
    }
    //�ϳɽ���
    //���ü���Ϊ������� ���ҷ���һ������ ��������������Ϊ���Ŷ�
    public virtual void OrWithDom(float val)
    {
        if (m_Dom < val) m_Dom = val;
    }
    public float GetRepresentVal()
    {
        if (domRange.x == domRange.y) return (setRange.x + setRange.y) / 2;
        if (domRange.z == domRange.y) return (setRange.z + setRange.y) / 2;
        return setRange.y;
    }
    public virtual float GetDom()
    {
        return m_Dom;
    }
    public virtual void ClearDom()
    {
        m_Dom = 0;
    }
}
