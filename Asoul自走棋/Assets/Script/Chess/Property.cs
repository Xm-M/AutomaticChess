using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelsoftGames.PixelUI;
//[CreateAssetMenu(fileName ="NewProperty",menuName ="Property")]
public class Property:MonoBehaviour
{
    public float moveSpeed;
    public float hpMax;
    [HideInInspector]public float hpCurrent;
    public float armorBase;
    [HideInInspector] public float armorCurrent;
    public int Price;
    public float mana;
    public UIStatBar HpBar;
    public float CD=1;
    public string messege;
    private void Start()
    {
        hpCurrent = hpMax;
    }
    public void GetDamage(float damage)
    {
        float realDamage = damage * (armorCurrent/(100+armorCurrent));
        hpCurrent -= damage;
        if(hpCurrent>hpMax)hpCurrent = hpMax;
        if(HpBar)
        HpBar.SetValue(hpCurrent, hpMax);
    }
    public void ReSet()
    {
        hpCurrent = hpMax;
        mana = 0;
        armorCurrent = armorBase;
        //magicResistantCurrent = magicResistantBase;
        if (HpBar)
            HpBar.SetValue(hpCurrent, hpMax);
        
    }
}
