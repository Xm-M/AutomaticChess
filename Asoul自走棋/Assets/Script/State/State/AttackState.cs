using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "State/AttackState")]
public class AttackState : State
{
    public static State instance;
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }
    public override void Enter(Chess chess)
    {
        base.Enter(chess);
        if (chess.equipWeapon)
            chess.equipWeapon.Attack();
    }
    public override void Excute(Chess chess)
    {
        if(chess.equipWeapon)chess.equipWeapon.Attack();
        base.Excute(chess);
    }
    public override void Exit(Chess chess)
    {
        if (chess.equipWeapon)
            chess.equipWeapon.Stand();
        base.Exit(chess);
    }
    //protected virtual void WeaponForward(Chess chess)
    //{
    //    chess.equipWeapon.transform.right = chess.target.transform.position - chess.transform.position;
    //    if((chess.FacingRight &&chess.target.transform.position.x < chess.transform.position.x) || (!chess.FacingRight && chess.target.transform.position.x > chess.transform.position.x)){
    //        chess.equipWeapon.transform.localScale = new Vector3(1, -chess.equipWeapon.transform.localScale.y, 1);
    //    }
        
    //}
}
