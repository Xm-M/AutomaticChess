using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    #region �ֶ�
    public LayerMask barrar;
    public static MapCreator instance;
    public Vector2Int xSizeRange;
    public Vector2Int ySizeRange;
    public int xMax;
    public int yMax;
    public int[,] maps;
    public GameObject[,] roads;
    public bool[,] ifVisited;
    public GameObject wall;
    public GameObject road;
    public int roomNum;
    public List<RoomMessege> rooms;
    public List<Sprite> mapSprite;
    public int tileSize = 2;
    public List<GameObject> Items;
    public delegate void WhenCreateRoom(RoomMessege roomMessege);
    public event WhenCreateRoom whenCreateRoom;
    #endregion
    #region ����
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        //whenCreateRoom += GameManager.instance.CreateBox;
        maps = new int[xMax, yMax];
        roads = new GameObject[xMax, yMax];
        ifVisited = new bool[xMax, yMax];
        rooms = new List<RoomMessege>();
        for (int i = 0; i < xMax; i++)
            for (int j = 0; j < yMax; j++)
                maps[i, j] = (i % 2 == 0 || j % 2 == 0) ? 1 : 0;//�����· 0· 
        RandomRoom();
        Maze();
        CreateRoomWay();
        UnCaver();
        CreateRoom();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Player");
        int n = Random.Range(0, rooms.Count);
        foreach (var a in gameObjects)
        {
            MapManage.instance.tileFather.transform.position = rooms[n].realPos + rooms[n].realSize / 2;
            a.transform.position = rooms[n].realPos;
        }
    }
    public void CreateRoom()
    {
        for (int i = 0; i < xMax; i++)
        {
            for (int j = 0; j < yMax; j++)
            {
                if (maps[i, j] == 1)
                {
                    GameObject walls = ObjectPool.instance.Create(wall);
                    if (walls.GetComponent<WallTile>())
                        walls.GetComponent<WallTile>().SetSprite(new Vector2Int(i, j));
                    walls.transform.position = new Vector2(i * tileSize, j * tileSize);
                }
                else
                {
                    GameObject roads = ObjectPool.instance.Create(road);
                    this.roads[i, j] = roads;
                    roads.transform.position = new Vector2(i * tileSize, j * tileSize);
                }
            }
        }
        for (int i = 0; i < rooms.Count; i++)
        {
            InitRoom(i);
        }
    }
    public void RandomRoom()
    {
        for (int i = 0; i < roomNum; i++)
        {
            int randomX = 2 * Random.Range(1, (xMax - 1) / 2) - 1;
            int randomY = 2 * Random.Range(1, (yMax - 1) / 2) - 1;

            int sizeX = Random.Range(xSizeRange.x, xSizeRange.y) * 2;
            int sizeY = Random.Range(ySizeRange.x, ySizeRange.y) * 2;
            if (randomX + sizeX > xMax - 1 || randomY + sizeY > yMax - 1) continue;
            //��������ֻ���ǵ��ĵ� ���ܻᵼ�±߽紩�� ������ȷ���ǿ����ı����е��Ƿ񱻷���
            else
            {
                bool ifRoom = false;
                for (int n = 0; n < rooms.Count; n++)
                {
                    if (Mathf.Abs(rooms[n].roomPos.x - randomX) < sizeX + rooms[n].roomSize.x - 2 &&
                        Mathf.Abs(rooms[n].roomPos.y - randomY) < sizeY + rooms[n].roomSize.y - 2)
                    {
                        ifRoom = true;
                        break;
                    }
                }
                if (!ifRoom)
                {
                    rooms.Add(new RoomMessege(new Vector2Int(randomX, randomY), new Vector2Int(sizeX, sizeY)));
                    for (int x = randomX; x <= randomX + sizeX; x++)
                    {
                        for (int y = randomY; y <= randomY + sizeY; y++)
                        {
                            maps[x, y] = -1;
                            ifVisited[x, y] = true;
                        }
                    }
                }
            }
        }
    }

    public void Maze()
    {
        for (int i = 1; i < xMax - 2; i += 2)
        {
            for (int j = 1; j < yMax - 2; j += 2)
            {
                if (!ifVisited[i, j])
                {
                    Prim(new Vector2Int(i, j));
                }
            }
        }
    }
    public void CreateRoomWay()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            int numOfDoor = Random.Range(1, 4);
            for (int j = 0; j < numOfDoor; j++)
            {
                Vector2Int pos = WayFromRoom(rooms[i].roomPos, rooms[i].roomSize);
                maps[pos.x, pos.y] = 0;
            }
        }
    }
    public Vector2Int WayFromRoom(Vector2Int pos, Vector2Int size)
    {
        int r;
        Vector2Int newPos;
        while (true)
        {
            r = Random.Range(0, 4);
            if (r == 0) newPos = new Vector2Int(pos.x + 2 * Random.Range(0, size.x / 2), pos.y - 1);
            else if (r == 1) newPos = new Vector2Int(pos.x + 2 * Random.Range(0, size.x / 2), pos.y + size.y + 1);
            else if (r == 2) newPos = new Vector2Int(pos.x - 1, pos.y + 2 * Random.Range(0, size.y / 2));
            else newPos = new Vector2Int(pos.x + size.x + 1, pos.y + 2 * Random.Range(0, size.y / 2));
            Vector2Int dis = FindNeighbor(newPos);
            if (newPos.x - dis.x >= 1 && (newPos.x + dis.x < xMax - 1) && newPos.y - dis.y >= 1 && newPos.y + dis.y < yMax - 1)
            {
                if ((maps[newPos.x + dis.x, newPos.y + dis.y] == 0 || maps[newPos.x - dis.x, newPos.y - dis.y] == 0)
                    && maps[newPos.x, newPos.y] != 0) return newPos;
            }
        }

    }
    public void UnCaver()
    {
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };
        for (int i = 0; i < xMax; i++)
        {
            for (int j = 0; j < yMax; j++)
            {
                if (maps[i, j] == 0)
                {
                    CaverRoad(new Vector2Int(i, j));
                }
            }
        }
    }
    public void CaverRoad(Vector2Int start)
    {
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };
        int wallnum = 0;
        Vector2Int next = Vector2Int.zero;
        for (int n = 0; n < dx.Length; n++)
        {
            if (start.x + dx[n] >= 0 && (start.x + dx[n] < xMax)
                && start.y + dy[n] >= 0 && start.y + dy[n] < yMax)
            {
                if (maps[start.x + dx[n], start.y + dy[n]] == 1) wallnum++;
                else if (maps[start.x + dx[n], start.y + dy[n]] == 0)
                    next = new Vector2Int(start.x + dx[n], start.y + dy[n]);
            }
            if (wallnum >= 3)
            {
                maps[start.x, start.y] = 1;
                CaverRoad(next);
            }
        }
    }
    public void Prim(Vector2Int start)
    {
        List<Vector2Int> A = new List<Vector2Int>();
        //maps[1, 1] = 1;//�ԣ�1��1��Ϊ���,��Ϊ·
        ifVisited[start.x, start.y] = true;//(1,1)Ϊ��� ���Ϊ�Ѿ�����
        //A.Add(new Vector2Int(2, 1));//����1��1������Χ��������б�
        //A.Add(new Vector2Int(1, 2));
        int[] dx = { 1, -1, 0, 0 };
        int[] dy = { 0, 0, 1, -1 };
        for (int i = 0; i < dx.Length; i++)
        {
            if (start.x + dx[i] >= 1 && (start.x + dx[i] < xMax - 1) && start.y + dy[i] >= 1 && start.y + dy[i] < yMax - 1
                && !ifVisited[start.x + dx[i], start.y + dy[i]])
            {
                A.Add(new Vector2Int(start.x + dx[i], start.y + dy[i]));
            }
        }

        while (A.Count != 0)//��ǽ�б�����Ϊ0
        {
            //�����A��ѡ��һ��ǽ
            Vector2Int pos = A[Random.Range(0, A.Count)];
            Vector2Int dxdy = FindNeighbor(pos);
            //���ӵ�����·
            Vector2Int road1 = pos + dxdy;
            Vector2Int road2 = pos - dxdy;
            A.Remove(pos);
            //������ӵ�����·��һ�������ʹ��� һ��û���ʹ� ������ǽ��Ϊ· ���Ұ����ǽ��·�����Ϊ�ѷ���
            //�������������ʹ�����ֱ�Ӱ����ǽ����Ϊ�Ѿ�����
            ifVisited[pos.x, pos.y] = true;
            //�������� Ϊʲô���г������ӵĽ����ж�
            if (!(ifVisited[road1.x, road1.y] && ifVisited[road2.x, road2.y]))
            {
                //��ǽ��Ϊ·
                maps[pos.x, pos.y] = 0;
                //��ǽ��·������Ϊ�Ѿ�����
                //��û�����ʹ����Ǹ�·�����Χ��ǽ ����A��
                Vector2Int unvisited = ifVisited[road1.x, road1.y] ? road2 : road1;
                ifVisited[road1.x, road1.y] = ifVisited[road2.x, road2.y] = true;
                for (int i = 0; i < dx.Length; i++)
                {
                    if (unvisited.x + dx[i] >= 1 && unvisited.x + dx[i] < xMax - 1 && unvisited.y + dy[i] >= 1 && unvisited.y + dy[i] < yMax - 1)
                    {
                        //��Χ��ĳһ�� ��������
                        Vector2Int newPos = new Vector2Int(unvisited.x + dx[i], unvisited.y + dy[i]);
                        if (!ifVisited[newPos.x, newPos.y])
                        {
                            A.Add(newPos);
                        }
                    }
                }
            }
        }
    }
    //��������ҵ�һ��ǽ���ӵ���������
    public Vector2Int FindNeighbor(Vector2Int wall)
    {
        if (wall.x % 2 == 0)
        {
            return new Vector2Int(1, 0);
        }
        else
        {
            return new Vector2Int(0, 1);
        }
    }
    public Sprite GetSprite(Vector2Int pos)
    {
        if (pos.y == 1) return mapSprite[pos.x + 1];
        else if (pos.y == 0) return mapSprite[pos.x + 4];
        else return mapSprite[pos.x + 7];
    }
    public void InitRoom(int roomNum)
    {
        //RoomType.Room1(rooms[roomNum]);
        //whenCreateRoom(rooms[roomNum]);
        //for (int i = 0; i < rooms[roomNum].realSize.x; i++)
        //    for (int j = 0; j < rooms[roomNum].realSize.y; j++)
        //    {
        //        if (rooms[roomNum].roomMap[i, j] == rooms[roomNum].rare)
        //        {
        //            GameObject a = GameObjectPool.instance.Create(Items[1]);
        //            a.transform.position = new Vector2(rooms[roomNum].realPos.x + i, rooms[roomNum].realPos.y + j);
        //        }
        //    }
    }
    #endregion
}
public class RoomMessege
{
    public Vector2Int roomPos;
    public Vector2Int roomSize;
    public Vector2 realPos;
    public Vector2 realSize;
    public int[,] roomMap;
    public int rare;//�������ϧ��
    public RoomMessege(Vector2Int roompos, Vector2Int roomsize, int rare = -2)
    {
        roomPos = roompos;
        roomSize = roomsize;
        this.rare = rare;
        realPos = roomPos * MapCreator.instance.tileSize;
        realSize = roomSize * MapCreator.instance.tileSize;
        roomMap = new int[(int)realSize.x, (int)realSize.y];
    }
    public RoomMessege(Vector2Int roompos, Vector2Int roomsize, Vector2 realpos, Vector2 realsize, int rare = 0)
    {
        roomPos = roompos;
        roomSize = new Vector2Int(roomsize.x + 1, roomsize.y + 1);
        this.rare = rare;
        realPos = realpos;
        realSize = realsize;
        roomMap = new int[(int)realSize.x, (int)realSize.y];
    }
    public void InitRoom()
    {

    }
}