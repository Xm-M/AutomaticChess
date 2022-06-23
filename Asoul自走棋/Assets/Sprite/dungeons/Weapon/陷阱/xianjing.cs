using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xianjing : MonoBehaviour
{
    public float damage;
    public float interval;
    float times;
    private void OnTriggerEnter2D(Collider2D collision)
    {              
        if (collision.GetComponent<Chess>())
        {
            collision.GetComponent<Chess>().GetDamage(damage,null);
        }
    }
    private void Update()
    {
        times+=Time.deltaTime;
        if(times > interval)
        {
            times=0;
            Attack();
        }
    }
    public void Attack()
    {
        GetComponent<Animator>().Play("attack");
        
    }
    public void Damge()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    public void Stand()
    {
        GetComponent<Animator>().Play("stand");
        GetComponent<Collider2D>().enabled = false;
    }
}
