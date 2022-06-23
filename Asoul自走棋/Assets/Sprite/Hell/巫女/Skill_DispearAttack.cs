using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_DispearAttack : Skill
{
    public float disTime;
    public GameObject effect;
    public float damage;
    float dtimes;
    bool ifSkill;
    Tile before;
    public override void OnSkillEnter()
    {
        ifSkill = true;      
        transform.position=controller.transform.position;
        base.OnSkillEnter();
    }
    public override void SkillEffect()
    {        
        before = controller.standTile;
        base.SkillEffect();
        controller.standTile.ChessLeave();
        MapManage.instance.UnSelectableTile.ChessEnter(controller);
        before.CantMove();
    }
    public void Attack()
    {
        foreach(var chess in GameManage.instance.teams[controller.target.tag].team)
        {
            GameObject a= ObjectPool.instance.Create(effect);
            a.transform.position=chess.transform.position;
            a.GetComponent<MissAttack>().target = chess;
        }
    }
    public void Back()
    {
        before.ChessEnter(controller);
        MapManage.instance.UnSelectableTile.ChessLeave();
    }
    private void Update()
    {
        if (!ifSkill) return;
        dtimes+=Time.deltaTime;
        if (dtimes > disTime)
        {
            dtimes = 0;
            GetComponent<Animator>().Play("end");
        }
    }
}
