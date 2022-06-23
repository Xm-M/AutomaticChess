using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterWindow : MonoBehaviour
{
    public static CharacterWindow instance;
    [SerializeField]
    GameObject demoIconPrefab = null;
    [SerializeField]
    Transform contentPane = null;
    List<GameObject> iconList = new List<GameObject>();
    private void Awake()
    {
        if(instance == null)instance = this;
    }
    private void OnEnable()
    {
        LoadCharacter();
    }
    void LoadCharacter()
    {
        int n = 0;      
        foreach (var chess in GameManage.instance.PlayerChess)
        {
            GameObject demoIcon;
            if (n < iconList.Count) demoIcon = iconList[n];
            else
            {
                demoIcon = Instantiate(demoIconPrefab, contentPane, false);
                demoIcon.GetComponent<ShopSelectIcon>().select = chess.GetComponent<Chess>();
                iconList.Add(demoIcon);
            }
            foreach (Image image in demoIcon.transform.GetComponentsInChildren<Image>())
                if (image.gameObject != demoIcon)
                {
                    image.sprite = chess.GetComponent<SpriteRenderer>().sprite;
                    image.SetNativeSize();
                }
            n++;
        }        
    }
    public void AddCard(GameObject chess)
    {
        GameObject demoIcon;
        demoIcon = Instantiate(demoIconPrefab, contentPane, false);
        demoIcon.GetComponent<ShopSelectIcon>().select = chess.GetComponent<Chess>();
        iconList.Add(demoIcon);
        Image image = demoIcon.transform.transform.GetChild(0).GetComponent<Image>();
        image.sprite = chess.GetComponent<SpriteRenderer>().sprite;
        image.SetNativeSize();
    }
}
