using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManaLackingTransition", menuName = "Transition/ManaLackingTransition")]
public class ManaLacking : Transition
{
    public override bool ifReach(Chess chess)
    {
        if (chess.property.mana > chess.skill.manaCost) return false;
        return true;
    }
}
