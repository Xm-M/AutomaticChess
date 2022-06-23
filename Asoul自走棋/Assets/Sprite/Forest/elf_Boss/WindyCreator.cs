using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindyCreator : MonoBehaviour
{
    public GameObject Windy;
    public float interval;
    float times;
    private void Update()
    {
        times += Time.deltaTime;
        if(times > interval)
        {
            times = 0;
            GameObject w= ObjectPool.instance.Create(Windy);
            w.transform.position = transform.position;
            w.tag = GetComponent<Chess>().target.tag;
        }
    }
}
