using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCreator : MonoBehaviour
{
    public float interval;
    public GameObject fire;
    Tile select;
    float times;
    private void Update()
    {
        times += Time.deltaTime;
        if (times > interval)
        {
            times = 0;
            RandomBoom();
            CreateBoom();
        }
    }
    public void RandomBoom()
    {
        int posX = Random.Range(0, 12);
        int posY = Random.Range(0, 8);
        select=MapManage.instance.tiles[posX,posY];
        transform.position=select.transform.position;
    }
    public void CreateBoom()
    {
        ObjectPool.instance.Create(fire).transform.position=select.transform.position;
    }
}
