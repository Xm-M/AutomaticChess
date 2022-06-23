using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public ShopIcon[] shopList;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AddSelection(Chess select)
    {
        for (int i = 0; i < shopList.Length; i++)
        {
            if (shopList[i].good == select) return;
        }
        for (int i = 0; i < shopList.Length; i++)
        {
            if (shopList[i].good == null)
            {
                shopList[i].good = select;
                shopList[i].CD = select.property.CD;
                shopList[i].ShowGood();
                return;
            }
        }
    }

}
