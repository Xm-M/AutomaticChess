using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour
{
    public Vector3Int cubePos;
    public Vector2Int mapPos;
    public bool IfMoveable { get; protected set; }
    public Chess Stander { get;private set; }
    private void Awake()
    {
        GetComponent<Collider2D>().enabled = false;
        IfMoveable = true;
    }
    private void Update()
    {
        //if (IfMoveable == false) GetComponent<SpriteRenderer>().color = Color.red;
        //else GetComponent<SpriteRenderer>().color = Color.white;
        if (Stander&&!Stander.gameObject.activeSelf) ChessLeave();
        if (Stander && Stander.standTile != this)
        {
            Stander = null;IfMoveable = true;
        }
    }
    public void ChessEnter(Chess chess)
    {
        if (Stander != null) Debug.LogError("此处有人");
        chess.transform.position = transform.position;
        IfMoveable = false;
        Stander = chess;
        chess.standTile=this;
    }
    public void ChessEnter(Chess chess,bool ifmove)
    {
        if (Stander != null) Debug.LogError("此处有人");
        IfMoveable = false;
        Stander = chess;
        chess.standTile = this;
    }
    public void ChessLeave()
    {
        IfMoveable = true;
        Stander = null;
    }
    public void CantMove()
    {
        IfMoveable=false;
        SetColor(Color.black);
    }
    public void SetColor()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }
    public void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
