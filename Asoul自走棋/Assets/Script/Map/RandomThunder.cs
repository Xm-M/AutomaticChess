using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomThunder : MonoBehaviour
{
    public GameObject Thunder;
    public Chess chess;
    public float interval;
    float times;
    private void Start()
    {
        EventController.Instance.AddListener(EventName.GameStart.ToString(), Qiumo);
    }
    private void Update()
    {
        if (!GameManage.instance.ifGameStart) return;
        times+=Time.deltaTime;
        if (times > interval)
        {
            times=0;
            if (GameManage.instance.teams["Player"].team.Count > 0)
            {
                GameObject thunder = ObjectPool.instance.Create(Thunder);
                Chess c = GameManage.instance.teams["Player"].team[Random.Range(0, GameManage.instance.teams["Player"].team.Count)];
                thunder.transform.position = c.transform.position;
                c.GetDamage(50f,null);
            }
            //if (GameManage.instance.teams["Enemy"].team.Count > 0)
            //{
            //    GameObject thunder = ObjectPool.instance.Create(Thunder);
            //    Chess c = GameManage.instance.teams["Enemy"].team[Random.Range(0, GameManage.instance.teams["Enemy"].team.Count)];
            //    thunder.transform.position = c.transform.position;
            //    c.GetDamage(50f,null);
            //}
        }
    }
    public void Qiumo()
    {
        Chess c = ObjectPool.instance.Create(chess.gameObject).GetComponent<Chess>();
        Vector2Int pos = new Vector2Int(11, 2);
        c.ResetAll();
        MapManage.instance.tiles[pos.x, pos.y].ChessEnter(c);
        c.tag = "Enemy";
        GameManage.instance.teams["Enemy"].AddMember(c);
    }
    private void OnDestroy()
    {
        EventController.Instance.RemoveListener(EventName.GameStart.ToString(), Qiumo);
    }
}
