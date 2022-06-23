using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissAttack : MonoBehaviour
{
    public Chess target;
    public float damage;
    public AudioSource source;
    private void Start()
    {
         source = GetComponent<AudioSource>();
    }
    public void TakeDamage()
    {       
        if(target != null)
        {
            transform.position = target.transform.position;
            target.GetDamage(damage,null);
            source.Play();
        }
    }
    public void Miss()
    {
        ObjectPool.instance.Recycle(gameObject);
    }
}
