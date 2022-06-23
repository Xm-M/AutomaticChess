using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManaEnoughTransition", menuName = "Transition/ManaEnoughTransition")]
public class ManaEnough : Transition
{
    public override bool ifReach(Chess chess)
    {
        if (chess.skill == null) return false;
        if (chess.property.mana > chess.skill.manaCost)
        {
            return true;
        }
        return false;
    }
}
