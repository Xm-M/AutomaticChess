using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_HolySummon : Skill
{
    public static Dictionary<string,int> dic;
    public int maxTimes;
    public Chess xiaoji;
    public override void OnSkillEnter()
    {
        if(dic == null)dic = new Dictionary<string,int>();
        base.OnSkillEnter();
        SkillEffect();
        transform.position = controller.transform.position;
    }
    public override void SkillEffect()
    {
        base.SkillEffect();
        if(!dic.ContainsKey(controller.tag))dic.Add(controller.tag, 0);
        dic[controller.tag]++;
        if (dic[controller.tag] >= maxTimes)
        {
            dic[controller.tag] = 0;
            HolySummon();
        }
    }
    public void HolySummon()
    {
        int x = 6, 
            y = 4;
        Chess c = ObjectPool.instance.Create(xiaoji.gameObject).GetComponent<Chess>();
        if(MapManage.instance.tiles[x, y].Stander != null)
        {
            MapManage.instance.tiles[x, y].Stander.Death();
        }
        c.ResetAll();
        MapManage.instance.tiles[x, y].ChessEnter(c);
        c.tag = controller.tag;
        GameManage.instance.teams[c.tag].AddMember(c);
    }
}
