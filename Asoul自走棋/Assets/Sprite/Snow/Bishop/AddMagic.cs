using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMagic : MonoBehaviour
{
    public float interval=1;
    float times;
    void Update()
    {
        times+=Time.deltaTime;
        if (times > interval)
        {
            times=0;
            foreach(var chess in GameManage.instance.teams[tag].team)
            {
                chess.property.mana += 1;
            }
        }
    }
}
