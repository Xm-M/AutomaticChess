using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject effect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Chess>())
        {
            Effect(collision.GetComponent<Chess>());
            ObjectPool.instance.Recycle(gameObject);
        }
    }
    protected virtual void Effect(Chess chess)
    {
        GameObject e= ObjectPool.instance.Create(effect);
        e.transform.position=chess.transform.position;
    }
}
