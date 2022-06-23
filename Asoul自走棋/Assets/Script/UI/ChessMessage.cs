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
            Atk.text = "��������" + chess.equipWeapon.attack.ToString();
            ATKRange.text = "�������룺" + chess.equipWeapon.attackRange.ToString();
            ATKSpeed.text = "���������" + chess.equipWeapon.attackInterval.ToString();
        }
        HP.text = "����ֵ��" + chess.property.hpMax.ToString();
        Message.text="��飺"+chess.property.messege.ToString();
    }
}
