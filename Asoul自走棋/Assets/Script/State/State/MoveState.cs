using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveState",menuName ="State/MoveState")]
public class MoveState : State
{
    public static State instance;
    private void OnEnable()
    {
        if (instance == null)
            instance = this;
    }
    public override void Excute(Chess chess)
    {
        //chess.transform.position = new Vector3(chess.transform.position.x, chess.transform.position.y,chess.transform.position.y);
        base.Excute(chess);
        if (!chess.ifMove)
        {
            GameManage.instance.AddMoveChess(chess);
        }
    }

    public override void Enter(Chess chess)
    {
        chess.StartCoroutine(JustWait(chess));
        base.Enter(chess);
    }

    public override void Exit(Chess chess)
    {
        base.Exit(chess);
    }
    IEnumerator JustWait(Chess chess)
    {
        float i = 0;
        while (i < 0.3f)
        {
            i += Time.deltaTime;
            yield return null;
        }
        if (chess.stateController.currentState == this)
        {
            chess.SearchTile();
            GameManage.instance.AddMoveChess(chess);
        }
    }
}
