using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetterManage
{
    public Dictionary<string, List<Fetter>> fetters;
    public List<Chess> team;

    public FetterManage()
    {
        fetters = new Dictionary<string, List<Fetter>>();
        team = new List<Chess>();
        EventController.Instance.AddListener(EventName.WhenSceneLoad.ToString(), Reset);
        EventController.Instance.AddListener(EventName.WhenSceneLoad.ToString(), Reset);
    }
    public void AddMember(Chess c)
    {
        team.Add(c);
        //for(int i = 0; i < c.fetters.Count; i++)
        //{
        //    if (!fetters.ContainsKey(c.fetters[i].fetterName))
        //        fetters.Add(c.fetters[i].fetterName, new List<Fetter>());
        //    fetters[c.fetters[i].fetterName].Add(c.fetters[i]);
        //    for (int j=0;j<fetters[c.fetters[i].fetterName].Count;j++)
        //    {
        //        fetters[c.fetters[i].fetterName][j].FetterEffect(fetters[c.fetters[i].fetterName].Count);
        //    }
        //}
    }
    public void RemoveMember(Chess c)
    {
        team.Remove(c);
        //for (int i = 0; i < c.fetters.Count; i++)
        //{
        //    fetters[c.fetters[i].fetterName].Remove(c.fetters[i]);
        //    for (int j = 0; j < fetters[c.fetters[i].fetterName].Count; j++)
        //        fetters[c.fetters[i].fetterName][j].FetterEffect(fetters[c.fetters[i].fetterName].Count);
        //    c.fetters[i].FetterReSet();
        //}
    }
    public void AddMember(Fetter f)
    {
        //场下没有装备的随从无法上场
    }
    public void Reset()
    {
        team.Clear();
    }
    public Chess RandomChess()
    {
        int i=Random.Range(0,team.Count);
        return team[i];
    }
}
