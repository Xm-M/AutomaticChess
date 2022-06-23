using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControler : MonoBehaviour
{
    public int minX=6;
    public int maxX=11;

    float selectTime = 0.5f;
    float times = 0;
    public List<Chess> enemyChoose;
    public float[] enemyCD;
    public float[] expect;
    public FuzzyModule fuzzyModule;
    public AIState currentState;
    public float[] probability = { 1.1f, 0.75f, 0.5f, 0.25f, -0.1f };
    float PriceSum;

    private void Start()
    {
        foreach (Chess chess in enemyChoose) PriceSum += chess.property.Price;
        //foreach (Chess c in enemyChoose) CDsum += c.property.CD;
        //aveCost=PriceSum/enemyChoose.Count;
   
        enemyCD =new float[enemyChoose.Count];
        expect=new float[enemyChoose.Count];
        fuzzyModule=GetComponent<FuzzyModule>();
        if(currentState == null)
        currentState = AIAttackState.Instance;
    }
    void BuyCard(int n)
    {
        Chess chess=enemyChoose[n];
        if (MoneyManage.instance.BuySomething("Enemy",chess.property.Price))
        {
            //int x= Random.Range(6, MapManage.instance.mapSize.x), y= Random.Range(0, MapManage.instance.mapSize.y);
            Chess c = ObjectPool.instance.Create(chess.gameObject).GetComponent<Chess>();
            Vector2Int pos=SelectPos(c);
            c.ResetAll();
            MapManage.instance.tiles[pos.x,pos.y].ChessEnter(c);
            c.tag = "Enemy";
            GameManage.instance.teams["Enemy"].AddMember(c);
            enemyCD[n] = 0;
        }        
    }
    void LevelUp()
    {
        MoneyManage.instance.LevelUp("Enemy");
    }
    public bool IfLevelUp()
    {
        int sum=0;
        //Debug.Log(MoneyManage.instance.teamM["Enemy"].addSpeed * MoneyManage.instance.teamM["Enemy"].baseAdd);
        float random = Random.Range(0, 0.9f);
        if (probability[MoneyManage.instance.teamM["Enemy"].level]>random)
        {
            MoneyManage.instance.teamM["Enemy"].LevelUp();
            return true;
        }
        return false;
    }
    private void Update()
    {
        if (GameManage.instance.ifGameStart)
        {
            times += Time.deltaTime;
            for(int i=0;i<enemyCD.Length;i++) enemyCD[i] += Time.deltaTime;
            if (times > selectTime)
            {
                times = 0;
                if (!IfLevelUp())
                {
                    BuyCard(currentState.SelectChess(this));
                }
            }
        }
    }
    public void StateChange()
    {

    }
    //public virtual int SelectChess()
    //{
    //    int n = 0;
    //    for (int i = 0; i < enemyChoose.Count; i++)
    //    {
    //        if (enemyCD[i] < enemyChoose[i].property.CD)
    //        {
    //            expect[i] = 0;
    //        }
    //        else
    //        {
    //            fuzzyModule.Fuzzify("FVAttack", enemyChoose[i].equipWeapon.attack / enemyChoose[i].equipWeapon.attackInterval);
    //            fuzzyModule.Fuzzify("FVPrice", enemyChoose[i].property.Price);
    //            fuzzyModule.Fuzzify("FVDefence", enemyChoose[i].property.hpMax);
    //            expect[i] = fuzzyModule.Defuzzy("FVHope");
    //        }
    //    }
    //    float max = 0;
    //    for (int i = 0; i < expect.Length; i++)
    //    {
    //        if (expect[i] > max)
    //        {
    //            max = expect[i];
    //            n = i;
    //        }
    //    }
    //    Debug.Log("期望值最高的是" + enemyChoose[n].name);
    //    return n;
    //}
    public Vector2Int SelectPos(Chess chess)
    {
        int x = chess.equipWeapon.attackRange+5;
        if (x > maxX) x = maxX;
        if (x < minX) x = minX;
        int y=0;
        int[] dy = { 3, 4, 2, 5, 1, 6, 0, 7 };
        for(int i = 0; i < dy.Length; i++)
        {
            if (MapManage.instance.tiles[x, dy[i]].IfMoveable) { 
                y=dy[i];
                break;
            }
        }
        return new Vector2Int(x, y);
    }
}
