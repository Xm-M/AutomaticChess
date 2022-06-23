using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWindow : MonoBehaviour
{
    [SerializeField]
    GameObject demoIconPrefab = null;
    [SerializeField]
    Transform contentPane = null;
    List<GameObject> iconList = new List<GameObject>();
    private void OnEnable()
    {
        LoadWeapon();
    }
    void LoadWeapon()
    {
        int n = 0;
        foreach (var weapon in GameManage.instance.allChess)
        {
            GameObject demoIcon;
            if (n < iconList.Count) demoIcon = iconList[n];
            else
            {
                demoIcon = Instantiate(demoIconPrefab, contentPane, false);
                iconList.Add(demoIcon);
            }
            foreach (Image image in demoIcon.transform.GetComponentsInChildren<Image>())
                if (image.gameObject != demoIcon)
                {
                    image.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
                    image.SetNativeSize();
                }
            n++;
        }
    }
    private void OnDisable()
    {
        
    }
}
