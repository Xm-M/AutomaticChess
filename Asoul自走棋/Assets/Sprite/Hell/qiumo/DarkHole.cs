using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkHole : MonoBehaviour
{
    public float speed;
    public LayerMask layerMask;
    Vector2 size = new Vector2(20, 2.5f);
    Collider2D[] hits=new Collider2D[10];
    List<Chess> chesses=new List<Chess>();
    private void FixedUpdate()
    {
        Physics2D.OverlapBoxNonAlloc(transform.position,size,0,hits,layerMask);
        foreach(var hit in hits)
        {
            if (hit && !hit.transform.CompareTag(tag))
            {
                Chess c = hit.transform.GetComponent<Chess>();
                if (c == null) continue;
                if (c.GetComponent<House>()) continue;
                if (!chesses.Contains(c))chesses.Add(c);
                dizziness.instance.AddChess(c, 100);
                c.stateController.ChangeState(dizziness.instance);
               
            }
        }
        foreach(Chess c in chesses)
        {
            c.transform.position = Vector2.MoveTowards(c.transform.position, transform.position, speed * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(tag)&&collision.GetComponent<Chess>())
        {
            if (collision.GetComponent<House>()) return;
            collision.GetComponent<Chess>().Death();
        }
    }
}
