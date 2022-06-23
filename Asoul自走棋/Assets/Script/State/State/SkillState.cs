using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillState", menuName = "State/SkillState")]
public class SkillState : State
{
    //public float interval = 1f;
    //public float times = 0;
    Dictionary<Chess,float> timeList = new Dictionary<Chess,float>();
    public static State instance;
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }
    public override void Enter(Chess chess)
    {
        base.Enter(chess);
        if (!timeList.ContainsKey(chess)) timeList.Add(chess, 0);
        timeList[chess] = 0;
        Skill skill = ObjectPool.instance.Create(chess.skill.gameObject).GetComponent<Skill>();
        skill.controller = chess;
        skill.gameObject.SetActive(true);
        skill.OnSkillEnter();
        
    }
    public override void Excute(Chess chess)
    {
        base.Excute(chess);
        timeList[chess] += Time.deltaTime;
        if (timeList[chess] > chess.skill.interval)
        {
            chess.property.mana = 0;
        }
    }
    public override void Exit(Chess chess)
    {
        base.Exit(chess);        
    }
}
