using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OutRangeTransition", menuName = "Transition/OutRangeTransition")]
public class OutRangeTransition : Transition
{
    public override bool ifReach(Chess chess)
    {
        if (!chess.target||!chess.target.gameObject.activeSelf||MapManage.Distance(chess.standTile, chess.target.standTile) > chess.equipWeapon.attackRange)
        {
            return true;
        }
        return false;
    }
}
