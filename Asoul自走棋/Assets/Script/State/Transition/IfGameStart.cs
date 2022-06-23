using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InGameStartTransition", menuName = "Transition/InGameStartTransition")]
public class IfGameStart : Transition
{
    public override bool ifReach(Chess chess)
    {
        if (GameManage.instance.ifGameStart && chess.gameObject.activeSelf&&GameManage.instance.HandChess!=chess)
            return true;
        return false;
    }
}
