using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AStart 
{
    List<Vector2Int> open;
    List<Vector2Int> close;
    public List<Tile> tiles;
    Dictionary<Vector2Int, Vector2> fgDic;//具体的值就被保存在这里了 vector3对应着f g h;
    Dictionary<Vector2Int, Vector2Int> preTile;//保存了某一个位置的前一格的位置 
    public Tile nextTile;
    public AStart()
    {
        open = new List<Vector2Int>();
        close = new List<Vector2Int>();
        fgDic = new Dictionary<Vector2Int, Vector2>();
        preTile = new Dictionary<Vector2Int, Vector2Int>();
        tiles = new List<Tile>();
    }
    public virtual void Search(Vector2Int current,Vector2Int target,Tile[,] maps,int range,Chess chess)
    {
        ResetList();
        open.Add(current);
        float dis = MapManage.instance.CalculateF(current, target);
        fgDic.Add(current, new Vector2(dis, 0));
        while (open.Count != 1)
        {
            Vector2Int cPos = FindMinF();
            close.Add(cPos);
            MapManage.instance.RoundTile(MapManage.instance.tiles[cPos.x, cPos.y], tiles);
            foreach(var tile in tiles)
            {
                if(!MapManage.instance.IfInMapRange(target.x, target.y))
                {
                    nextTile = maps[current.x, current.y];
                    return;
                }
                if (MapManage.Distance(tile,MapManage.instance.tiles[target.x,target.y])<range)
                {
                    Vector2Int next = cPos;
                    while (preTile.ContainsKey(next) && preTile[next] != current)
                    {
                        next = preTile[next];
                    }
                    nextTile = maps[next.x, next.y];
                    return;
                }
                Vector2Int newPos = tile.mapPos;
                float ng = fgDic[cPos].y + 1;
                float nf = ng + MapManage.instance.CalculateF(target, newPos);
                if (tile.IfMoveable == false)
                {
                    continue;
                }
                if (open.Contains(newPos))
                {
                    if (fgDic[newPos].x < nf)
                    {
                        fgDic[newPos] = new Vector2(nf, ng);
                        preTile[newPos] = cPos;
                        open.Remove(newPos);
                        OpenAdd(newPos);
                    }
                }
                else if (!close.Contains(newPos))
                {
                    fgDic.Add(newPos, new Vector2(nf, ng));
                    preTile.Add(newPos, cPos);//可以通过pretile反向查找最短路径
                    OpenAdd(newPos);
                }
            }            
        }
        nextTile = maps[current.x, current.y];
    }
    public Vector2Int FindMinF()
    {
        Vector2Int vector2Int = open[1];
        Vector2Int t = open[1];
        open[1] = open[open.Count - 1];
        open[open.Count - 1] = t;
        open.RemoveAt(open.Count-1);
        SifiDown();
        return vector2Int;
    }
    public void OpenAdd(Vector2Int vector2Int)
    {
        open.Add(vector2Int);
        SifiUp();
    }
    public void SifiDown()
    {
        int i=1;
        while (i*2 < open.Count)
        {
            i *= 2;
            if (i + 1 < open.Count && fgDic[open[i]].x > fgDic[open[i + 1]].x) i++;
            if (fgDic[open[i]].x < fgDic[open[i / 2]].x)//i是下方，i/2是上方
            {
                Vector2Int n = open[i];
                open[i] = open[i / 2];
                open[i / 2] = n;
            }
            else return;
        }
    }
    public void SifiUp()
    {
        int i = open.Count-1;
        if (i == 1) return;
        while (i/2 >= 1)
        {
            if (fgDic[open[i]].x < fgDic[open[i / 2]].x)//i是下方，i/2是上方，应满足F(i/2)<F(i)，不满足则交换;
            {
                Vector2Int n = open[i];
                open[i]=open[i/2];
                open[i / 2] = n;
            }
            else return;
            i /= 2;
        }
    }
    public void ResetList()
    {
        open.Clear();
        open.Add(new Vector2Int(100,100));  
        close.Clear();
        fgDic.Clear();
        preTile.Clear();
    }
}

