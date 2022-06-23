using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSelectIcon : MonoBehaviour
{
    public Chess select;
    public void SelectCard()
    {
        if(select!=null)
        Shop.instance.AddSelection(select);
        UIManage.instance.ShowChess(select);
    }
}
