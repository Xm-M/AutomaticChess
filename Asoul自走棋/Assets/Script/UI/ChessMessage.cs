using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChessMessage : MonoBehaviour
{
    public Image chessPrite;
    public Text Atk;
    public Text HP;
    public Text ATKRange;
    public Text ATKSpeed;
    public Text Message;
    public void ShowChessMessage(Chess chess)
    {
        chessPrite.sprite=chess.GetComponent<SpriteRenderer>().sprite;
        chessPrite.SetNativeSize();
        if (chess.equipWeapon)
        {
            Atk.text = "¹¥»÷Á¦£º" + chess.equipWeapon.attack.ToString();
            ATKRange.text = "¹¥»÷¾àÀë£º" + chess.equipWeapon.attackRange.ToString();
            ATKSpeed.text = "¹¥»÷¼ä¸ô£º" + chess.equipWeapon.attackInterval.ToString();
        }
        HP.text = "ÉúÃüÖµ£º" + chess.property.hpMax.ToString();
        Message.text="¼ò½é£º"+chess.property.messege.ToString();
    }
}
