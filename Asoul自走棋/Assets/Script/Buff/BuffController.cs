using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public Dictionary<string, Buff> buffDic;
    private void Update()
    {
        foreach(var a in buffDic)
        {
            a.Value.BuffEffect();
        }
    }
    public void AddBuff(Buff buff)
    {
        if (buffDic.ContainsKey(buff.buffName))
        {
            buffDic[buff.buffName].BuffReset();
        }
        else
        {
            buffDic.Add(buff.buffName, buff);
        }
    }
    public void RemoveBuff(string buffName)
    {
        if (buffDic.ContainsKey(buffName))
        {
            buffDic.Remove(buffName);
        }
        else
        {
            Debug.Log("没有这个buff");
        }
    }
}
