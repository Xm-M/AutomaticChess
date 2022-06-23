using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public float interval = 5;
    public Chess chess;
    float times;
    public void Start()
    {
        EventController.Instance.AddListener(EventName.GameStart.ToString(), Qiumo);
    }
    private void Update()
    {
        if (!GameManage.instance.ifGameStart) return;
        times +=Time.deltaTime;
        if(times > interval)
        {
            times=0;
            GameObject item= ObjectPool.instance.Create(items[Random.Range(0, items.Count)]);
            item.transform.position=MapManage.instance.tiles[Random.Range(0,12),Random.Range(0,7)].transform.position;    
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
