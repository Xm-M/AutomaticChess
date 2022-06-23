using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chess : MonoBehaviour
{
    public static int numOfChess;
    public int instanceID;
    public Tile standTile;
    public Chess target;
    public Weapon equipWeapon;
    public StateController stateController;
    public Property property;
    public Skill skill;
    public Animator animator;
    public bool unAttackable;
    Fetter fetter;
    public bool ifMove = false;
    public bool FacingRight=true;
    AStart aStart; 
    protected virtual void Start()
    {
        aStart = new AStart();
        if (!equipWeapon) equipWeapon = transform.GetChild(0).GetComponent<Weapon>();
        fetter=GetComponent<Fetter>();
        stateController = GetComponent<StateController>();
        animator = GetComponent<Animator>();    
        EventController.Instance.AddListener(EventName.RestartGame.ToString(), Death);  
        //equipWeapon.master = this;
    }
    public void FindTarget( )
    {
        Chess chess=null;
        int minDistance;
        List<Chess> enemyTeam;
        if (CompareTag("Player")) enemyTeam = GameManage.instance.teams["Enemy"].team;
        else enemyTeam = GameManage.instance.teams["Player"].team;
        if (enemyTeam.Count > 0)
        {
            chess = enemyTeam[0];
            minDistance = MapManage.Distance(standTile, chess.standTile);
            for (int i = 0; i < enemyTeam.Count; i++)
                if (MapManage.Distance(standTile,enemyTeam[i].standTile) < minDistance)
                {
                    minDistance = MapManage.Distance(standTile, enemyTeam[i].standTile);
                    chess = enemyTeam[i];
                }
        }
        target = chess;
    }

    public void Update()
    {
        Flap();
        WhenMouseMove();
    }
    public void Flap()
    {
        if (target) 
        if ((FacingRight && target.transform.position.x < transform.position.x) || (!FacingRight && target.transform.position.x > transform.position.x))
        {
            transform.Rotate(0, 180, 0);
            FacingRight = !FacingRight;
        }
    }
    private void WhenMouseMove()
    {
        if (GameManage.instance.HandChess == this)
        {
            if (Input.GetMouseButtonDown(1))
            {
                GameManage.instance.HandChess = null;
                MapManage.instance.SleepTile();
                MoneyManage.instance.teamM[tag].currenMoney += property.Price;
                Death();                
            }
            Vector2 mousePos = GameManage.instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;           
        }
        
    }
    private void OnMouseUp()
    {
        if (CompareTag("Player")&&GameManage.instance.HandChess==this)
        {
            unAttackable = false;
            if (!standTile || standTile.IfMoveable == false)
            {
                return;
            }
            MapManage.instance.SleepTile();
            transform.position = standTile.transform.position;
            standTile.ChessEnter(this);
            GameManage.instance.teams[tag].AddMember(this);
            GameManage.instance.HandChess = null;
            if (GameManage.instance.ifGameStart) stateController.ChangeState(MoveState.instance);
            MapManage.instance.SleepTile();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManage.instance.HandChess==this&&stateController.currentState==PrepareState.instance&&collision.GetComponent<Tile>()&& collision.GetComponent<Tile>().IfMoveable) standTile = collision.GetComponent<Tile>();
    }
    public void SearchTile()
    {    
        FindTarget();
        if (target == null)
        {
            stateController.ChangeState(PrepareState.instance);
            return;
        }
        aStart.Search(standTile.mapPos, target.standTile.mapPos, MapManage.instance.tiles,equipWeapon.attackRange,this);
    }
    public virtual void MoveToNextTile()
    {
        if (ifMove == true) return;
        SearchTile();
        if (aStart.nextTile == standTile || property.moveSpeed == 0)
        {
            GameManage.instance.AddMoveChess(this);
            animator.Play("idle");
            return;
        }
        standTile.ChessLeave();
        aStart.nextTile.ChessEnter(this, true);
        if (ifMove == false)
        {
            ifMove = true;
            StartCoroutine(Moving());
        }
    }
    IEnumerator Moving()
    {
        animator.Play("run");
        while (Vector2.Distance(transform.position, standTile.transform.position) > 0.0001)
        {            
            transform.position = Vector2.MoveTowards(transform.position, standTile.transform.position, property.moveSpeed * Time.deltaTime);
            yield return null;
        }
        GameManage.instance.AddMoveChess(this);
        animator.Play("idle");
        ifMove = false;
    }
    public virtual void GetDamage(float damage,Chess chess)
    {
        if (unAttackable) return;
        property.GetDamage(damage);
        if (property.hpCurrent <= 0)
        {
            Death();
            return;
        }
        if(gameObject.activeSelf)
        StartCoroutine(ColorChange(0.3f));
    }

    public void ResetAll()
    {
        target = null;
        if (standTile)
            standTile.ChessLeave();
        property.ReSet();
        equipWeapon.ResetWeapon();
        GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
        if (fetter) fetter.UseFetter();
        ifMove = false;
    }
    public virtual void Death()
    {
        EventController.Instance.TriggerEvent(instanceID + EventName.WhenDeath.ToString());
        stateController.ChangeState(PrepareState.instance);
        if(standTile)
        standTile.ChessLeave();
        GameManage.instance.teams[tag].RemoveMember(this);
        property.ReSet();
        equipWeapon.ResetWeapon();
        ObjectPool.instance.Recycle(gameObject);      
    }
    private void OnDestroy()
    {
        EventController.Instance.RemoveListener(EventName.RestartGame.ToString(), Death);
    }
    IEnumerator ColorChange(float time)
    {
        float times = 0;
        GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 1);
        while (times < time)
        {
            times += Time.deltaTime;
            yield return null;
        }
        GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", 0);
    }
}
