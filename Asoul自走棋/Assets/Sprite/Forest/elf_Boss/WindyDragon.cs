using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindyDragon : MonoBehaviour
{
    public float disapearTime;
    public float speed;
    float times;
    public float damage;
    private void Start()    
    {
        GetComponent<Animator>().SetBool("miss", false);
        StartCoroutine(RandomMove());
    }
    IEnumerator RandomMove()
    {
        Transform target = GameManage.instance.teams[tag].RandomChess().transform;
        while ((transform.position - target.transform.position).magnitude > 0.01)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(RandomMove());
    }
    // Update is called once per frame
    void Update()
    {
        times+=Time.deltaTime;
        if (times > disapearTime)
        {
            StopAllCoroutines();
            times=0;
            GetComponent<Animator>().SetBool("miss", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag(collision.tag) && collision.GetComponent<Chess>())
        {
            collision.GetComponent<Chess>().GetDamage(damage,null);
        }
    }
    public void Recycle()
    {
        ObjectPool.instance.Recycle(gameObject);
    }
}
