using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIce : MonoBehaviour
{
    public GameObject ice;
    public float interval;
    public float coldTime=2;
    float times;
    private void Update()
    {
        if (!GameManage.instance.ifGameStart) return;
        times += Time.deltaTime;
        if (times > interval)
        {
            times = 0;
            if (GameManage.instance.teams["Player"].team.Count > 0)
            {
                GameObject thunder = ObjectPool.instance.Create(ice);
                
                Chess c = GameManage.instance.teams["Player"].team[Random.Range(0, GameManage.instance.teams["Player"].team.Count)];
                thunder.transform.position = c.transform.position;
                dizziness.instance.AddChess(c,coldTime);
                c.stateController.ChangeState(dizziness.instance);
                thunder.transform.parent = c.transform;
            }
            if (GameManage.instance.teams["Enemy"].team.Count > 0)
            {
                GameObject thunder = ObjectPool.instance.Create(ice);
                Chess c = GameManage.instance.teams["Enemy"].team[Random.Range(0, GameManage.instance.teams["Enemy"].team.Count)];
                thunder.transform.position = c.transform.position;
                dizziness.instance.AddChess(c, coldTime);
                c.stateController.ChangeState(dizziness.instance);
                thunder.transform.parent = c.transform;
            }
        }
    }
}
