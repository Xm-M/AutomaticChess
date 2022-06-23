using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManage : MonoBehaviour
{
    public static MapManage instance;
    public Chess Playerhouse;
    public Chess EnemyHouse;
    public Tile[,] tiles;
    public GameObject tile;
    public Transform tileFather;
    public Vector2Int mapSize;
    public float CantMoveNum;
    int[] dx = { 1, -1, 0, 0, -1, 1 };
    int[] dy = { 0, 0, 1, -1, 1, -1 };
    int[] dz = { -1, 1, -1, 1, 0, 0 };
    public Tile UnSelectableTile;
    private void Awake()
    {
        if (instance == null||instance!=this)
            instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        tiles = new Tile[mapSize.x, mapSize.y];
        UnSelectableTile = Instantiate(tile, tileFather).GetComponent<Tile>();
        UnSelectableTile.mapPos = new Vector2Int(100,100);
        UnSelectableTile.transform.position = new Vector2(100, 100);
        for (int i = 0; i < mapSize.x; i++)
        {
            for(int j = 0; j < mapSize.y; j++)
            {
                GameObject t = Instantiate(tile, tileFather);
                tiles[i, j] = t.GetComponent<Tile>();
                tiles[i, j].cubePos = ChangeMapToCubePos(new Vector2Int(i, j));
                tiles[i, j].mapPos = ChangeCubeToMapPos(tiles[i, j].cubePos);
                t.transform.position = new Vector2(i * 1.25f, j + i % 2 * 0.5f);
            }
        }
        CreatHouse(Playerhouse,0, (mapSize.y - 1) / 2,"Player");
        CreatHouse(Playerhouse,mapSize.x-1, (mapSize.y - 1) / 2, "Enemy");
    }
    public void CreatHouse(Chess house,int x, int y, string tag)
    {
        Chess chess = ObjectPool.instance.Create(house.gameObject).GetComponent<Chess>();
        tiles[x, y].ChessEnter(chess);
        chess.tag = tag;
        GameManage.instance.teams[chess.tag].AddMember(chess);
    }
    public static Vector3Int ChangeMapToCubePos(Vector2Int mapPos)
    {
        Vector3Int pos = new Vector3Int
        {
            x = mapPos.x,
            y = mapPos.y - (mapPos.x) / 2
        };
        pos.z = 0 - (pos.x + pos.y);
        return pos;
    }
    public static Vector2Int ChangeCubeToMapPos(Vector3Int cubePos)
    {
        Vector2Int pos = new Vector2Int
        {
            x = cubePos.x,
            y = cubePos.y + (cubePos.x) / 2
        };
        return pos;
    }
    public static int Distance(Tile tile1,Tile tile2)
    {
        if (tile1 == null || tile2 == null) return 1000;
        return (Mathf.Abs(tile1.cubePos.x - tile2.cubePos.x) + Mathf.Abs(tile2.cubePos.y - tile1.cubePos.y) + Mathf.Abs(tile1.cubePos.z - tile2.cubePos.z)) / 2;
    }
    public  int Distance(Vector2Int tile1, Vector2Int tile2)
    {
        if(IfInMapRange(tile1.x,tile1.y)&&IfInMapRange(tile1.x,tile2.y))
         return Distance(tiles[tile1.x,tile1.y],tiles[tile2.x,tile2.y]);
        return 100;
    }
    public float RealDis(Vector2Int tile1, Vector2Int tile2)
    {
        if (IfInMapRange(tile1.x, tile1.y) && IfInMapRange(tile1.x, tile2.y))
            return (tiles[tile1.x, tile1.y].transform.position - tiles[tile2.x, tile2.y].transform.position).magnitude;
        return 100;
    }
    public float CalculateF(Vector2Int tile1, Vector2Int tile2)
    {
        return Distance(tile1, tile2)+RealDis(tile1,tile2)/10;
    }
    public bool IfInMapRange(int x,int y)
    {
        return (x >= 0 && x < mapSize.x) && (y >= 0 && y < mapSize.y);
    }
    public void AwakeTile()
    {
        for (int i = 0; i < mapSize.x/2; i++)
            for (int j = 0; j < mapSize.y; j++)
                tiles[i, j].GetComponent<Collider2D>().enabled = true;
    }
    public void SleepTile()
    {
        for (int i = 0; i < mapSize.x/2; i++)
            for (int j = 0; j < mapSize.y; j++)
                tiles[i, j].GetComponent<Collider2D>().enabled = false;
    }   
    public void RoundTile(Tile tile,List<Tile> newlist)
    {
        newlist.Clear();
        Vector3Int tilepos = tile.cubePos;

        for (int i = 0; i < dx.Length; i++)
        {
            Vector2Int mapPos = ChangeCubeToMapPos(new Vector3Int(tilepos.x + dx[i], tilepos.y + dy[i], tilepos.z + dz[i]));
            if (IfInMapRange(mapPos.x, mapPos.y))
            {
                newlist.Add(tiles[mapPos.x, mapPos.y]);
            }
        }
    }
}
