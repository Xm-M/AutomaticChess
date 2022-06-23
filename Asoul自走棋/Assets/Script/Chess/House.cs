using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : Chess
{
    public static float[] damageArray = { 0.2f, 1, 0.8f, 0.6f, 0.45f, 0.3f, 0.2f };
    protected override void Start()
    {
        base.Start();
        EventController.Instance.AddListener(EventName.GameStart.ToString(), AddTeam);
    }
    public void AddTeam()
    {
        GameManage.instance.teams[tag].AddMember(this);
    }
    public override void Death()
    {
        property.ReSet();
        equipWeapon.ResetWeapon();
        if(GameManage.instance.ifGameStart)
        GameManage.instance.GameOver(tag);
    }

    private void OnDestroy()
    {
        EventController.Instance.RemoveListener(EventName.GameStart.ToString(), AddTeam);
    }
    public override void GetDamage(float damage,Chess chess)
    {
        float d=damage;
        
        if(chess != null)
        {
            int range = chess.equipWeapon.attackRange;
            if (range < damageArray.Length)
                d *= damageArray[range];
            else d = damageArray[0];
        }
        base.GetDamage(d,chess);        
    }
    public static float DamageDefence(int range)
    {
        if (range < damageArray.Length)
            return damageArray[range];
        else return damageArray[0];
    }
}
