using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LongDistance shooter;
    public Transform target;
    public float moveSpeed = 10;
    public GameObject effect;

    private void Start()
    {
        //EventController.Instance.AddListener(EventName.GameOver.ToString(),()=>ObjectPool.instance.Recycle(gameObject));
    }
    private void Update()
    {
        if (GameManage.instance.ifGameStart)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            if (!target.gameObject.activeSelf)
            {
                ObjectPool.instance.Recycle(gameObject);
            }
        }
        else
        {
            ObjectPool.instance.Recycle(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            shooter.TakeDamage(shooter.master.target);
            ObjectPool.instance.Recycle(gameObject);
            GameObject effcets = ObjectPool.instance.Create(effect);
            effcets.transform.position = target.transform.position;
            //effcets.transform.right = target.transform.right;
        }
    }
}
