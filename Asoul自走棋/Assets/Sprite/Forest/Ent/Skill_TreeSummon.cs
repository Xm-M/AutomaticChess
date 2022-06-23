using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_TreeSummon : Skill
{
    public Chess tree;
    public override void OnSkillEnter()
    {
        TreeSummon();
        base.OnSkillEnter();
        OnSkillExit();
    }
    public void TreeSummon()
    {
        Chess c = ObjectPool.instance.Create(tree.gameObject).GetComponent<Chess>();
        Vector2Int pos = SelectPos(c);
        c.ResetAll();
        MapManage.instance.tiles[pos.x, pos.y].ChessEnter(c);
        c.tag = controller.tag;
        GameManage.instance.teams[c.tag].AddMember(c);
    }
    public Vector2Int SelectPos(Chess chess)
    {
        int x;
        if (controller.CompareTag("Enemy")) x = 7;
        else x = 4;
        int y = 0;
        int[] dy = { 3, 4, 2, 5, 1, 6, 0, 7 };
        for (int i = 0; i < dy.Length; i++)
        {
            if (MapManage.instance.tiles[x, dy[i]].IfMoveable)
            {
                y = dy[i];
                break;
            }
        }
        return new Vector2Int(x, y);
    }
}
