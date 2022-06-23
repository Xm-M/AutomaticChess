using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrakCreate : MonoBehaviour
{
    public int a;
    public GameObject drakhole;
    public float posY;
    public float interval = 3;
    float times;
    Chess Chess;
    int max = 4;
    private void Start()
    {
        Chess = GetComponent<Chess>();  
    }
    public void Update()
    {
        if (GameManage.instance.ifGameStart)
        {
            times += Time.deltaTime;
            if (times > interval && max > 0)
            {
                dizziness.instance.AddChess(Chess, 1);
                GetComponent<StateController>().ChangeState(dizziness.instance);
                GetComponent<Animator>().Play("usingskill");
                times = 0;
                GameObject d = ObjectPool.instance.Create(drakhole);
                d.transform.position = new Vector2(transform.position.x-transform.right.x*1, posY);
                d.transform.right = transform.right;
                d.tag = tag;
                posY += 3;
                max--;
            }
        }
        else
        {
            times = 0;
            max = 4;
        }
    }
}
