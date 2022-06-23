using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    float baseAttack;//基础攻击力
    float baseAttackInterval;
    public float attack;//武器攻击力 应该是额外数值
    public int attackRange;//武器攻击距离
    public Chess master;//武器拥有者
    protected float timer = 0;//计时器
    public float attackInterval = 0.2f;
    protected bool ifAttack = false;
    public float ExtraMagic = 10f;
    AudioSource audio;
    private void Awake()
    {
        baseAttack = attack;
        baseAttackInterval = attackInterval;
    }
    protected virtual void Start()
    {
        if (!master) master = transform.parent.GetComponent<Chess>();
        audio = GetComponent<AudioSource>();
           
    }
    public virtual void Attack() {
        Chess chess = master;
        if (!chess.target) return;
        chess.equipWeapon.transform.right = chess.target.transform.position - chess.transform.position;
        if ((chess.FacingRight && chess.target.transform.position.x < chess.transform.position.x) || (!chess.FacingRight && chess.target.transform.position.x > chess.transform.position.x))
        {
            chess.equipWeapon.transform.localScale = new Vector3(1, -chess.equipWeapon.transform.localScale.y, 1);
        }
        if (ifAttack == false)
        {
            GetComponent<Animator>().Play("attack");
            ifAttack = true;
        }
    }
    public virtual void Stand() => GetComponent<Animator>().Play("stand");
    public virtual void TakeDamage(Chess target)
    {
        //if (audio) audio.Play();
        if (target == null) return;
        target.GetDamage(attack,master);
        master.property.mana += ExtraMagic;
        EventController.Instance.TriggerEvent(master.instanceID + EventName.WhenAttackTakeDamage.ToString());
        EventController.Instance.TriggerEvent<Chess>(target.instanceID + EventName.WhenBeAttack.ToString(),master);
    }
    public virtual void TakeDamages()
    {
        //if(audio)audio.Play();
        master.target.GetDamage(attack,master);
        master.property.mana += ExtraMagic;
        //EventMessage message;
        //message.Chess = master;
        EventController.Instance.TriggerEvent(master.instanceID + EventName.WhenAttackTakeDamage.ToString());
        EventController.Instance.TriggerEvent<Chess>(master.target.instanceID + EventName.WhenBeAttack.ToString(),master);
    }
    private void Update()
    {
        if (ifAttack)
        {
            timer += Time.deltaTime;
            if (timer > attackInterval)
            {
                ifAttack = false;
                timer = 0;
            }
        }
    }
    public void PlayAudio()
    {
        if (audio != null)
            audio.Play();
    }
    public void ResetWeapon()
    {
        attack = baseAttack;
        attackInterval = baseAttackInterval;
    }
}
