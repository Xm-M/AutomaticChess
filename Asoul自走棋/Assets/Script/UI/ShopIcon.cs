using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PixelsoftGames.PixelUI;
public class ShopIcon : MonoBehaviour
{
    public Sprite baseSprite;
    public Chess good;
    public Image image;
    bool ifCanBuy;
    float times = 0;
    public float CD = 5;
    public UIStatBar bar;
    public Text price;
    private void Update()
    {
        if (GameManage.instance.ifGameStart)
        {
            bar.SetValue(CD - times, CD);
            if (times < CD)
                times += Time.deltaTime;
            if (times > CD)
            {
                ifCanBuy = true;
            }
        }
        else times = 0;
    }
    public void ShowGood()
    {
        if (!good) return;
        image.sprite = good.GetComponent<SpriteRenderer>().sprite;
        image.SetNativeSize();
        price.text=good.property.Price.ToString();
    }
    public void BuyCard()
    {
        if (good&&GameManage.instance.ifGameStart&&GameManage.instance.HandChess==null&&ifCanBuy)
        {
            if (MoneyManage.instance.BuySomething("Player", good.property.Price))
            {              
                Chess c = ObjectPool.instance.Create(good.gameObject).GetComponent<Chess>();
                c.unAttackable = true;
                c.ResetAll();
                GameManage.instance.HandChess = c;
                MapManage.instance.AwakeTile();
                c.tag = "Player";
                ifCanBuy = false;
                times = 0;
            }        
        }else if (!GameManage.instance.ifGameStart)
        {
            good = null;
            image.sprite = baseSprite;
        }
    }
}
