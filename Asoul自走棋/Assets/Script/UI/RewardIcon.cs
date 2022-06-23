using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RewardIcon : MonoBehaviour
{
    public GameObject select;

    private void OnEnable()
    {
        ShowReward();
    }
    public void SelectCard()
    {
        if (select != null)
            CharacterWindow.instance.AddCard(select);
    }
    public void ShowReward()
    {
        select = GameManage.instance.allChess[Random.Range(0, GameManage.instance.allChess.Count)];
        while (GameManage.instance.PlayerChess.Contains(select))
        {
            select=GameManage.instance.allChess[Random.Range(0, GameManage.instance.allChess.Count)];
        }
        Image image = transform.GetChild(0).GetComponent<Image>();
        image.sprite = select.GetComponent<SpriteRenderer>().sprite;
        image.SetNativeSize();
    }
}
